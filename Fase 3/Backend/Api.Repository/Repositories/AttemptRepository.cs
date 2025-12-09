using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Repositories
{
    public class AttemptRepository : IAttemptRepository
    {
        private readonly AppDbContext _context;

        public AttemptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Attempts> CreateLessonAttemptAsync(
            long courseId,
            long lessonId,
            SubmitLessonAttemptRequest dto)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var now = DateTime.UtcNow;

                // Tomamos la primera evaluación del payload
                var firstQuiz = dto.Quizzes.First();

                var eval = await _context.Evaluations
                    .Include(e => e.Questions)
                        .ThenInclude(q => q.Options)
                    .FirstOrDefaultAsync(e =>
                        e.Id == firstQuiz.EvaluationId &&
                        e.CourseId == courseId &&
                        e.LessonId == lessonId);

                if (eval == null)
                    throw new Exception("Evaluación no encontrada.");

                decimal totalPoints = 0m;
                decimal obtainedPoints = 0m;

                var attempt = new Attempts
                {
                    EvaluationId = eval.Id,
                    StudentId = dto.StudentId,
                    StartedAt = now,
                    FinishedAt = now,     // alias de SubmittedAt
                    AttemptNumber = 1,
                    ScoreObtained = 0m,
                    Passed = false
                };

                _context.Attempts.Add(attempt);
                await _context.SaveChangesAsync();

                foreach (var quiz in dto.Quizzes)
                {
                    foreach (var ans in quiz.Answers)
                    {
                        var question = await _context.Questions
                            .Include(q => q.Options)
                            .FirstOrDefaultAsync(q => q.Id == ans.QuestionId);

                        if (question == null)
                            continue;

                        totalPoints += question.Points;
                        bool correct = false;

                        if (ans.OptionId.HasValue)
                        {
                            var option = question.Options.FirstOrDefault(o => o.Id == ans.OptionId);
                            if (option != null && option.IsCorrect)
                            {
                                obtainedPoints += question.Points;
                                correct = true;
                            }
                        }

                        var answer = new Answers
                        {
                            AttemptId = attempt.Id,
                            QuestionId = question.Id,
                            OptionId = ans.OptionId,
                            OpenText = ans.OpenText
                        };

                        _context.Answers.Add(answer);
                    }
                }

                // Nota 0–100
                attempt.ScoreObtained = totalPoints > 0
                    ? (obtainedPoints / totalPoints) * 100m
                    : 0m;

                // Aprobado según pass_threshold de la evaluación
                var passThreshold = eval.PassThreshold; // ej. 0.6
                attempt.Passed = (attempt.ScoreObtained / 100m) >= passThreshold;

                _context.Attempts.Update(attempt);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();
                return attempt;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<List<LessonAttemptSummary>> GetLessonAttemptsByStudentAsync(
            long courseId,
            long lessonId,
            long studentId)
        {
            return await _context.Attempts
                .Include(a => a.Evaluation)
                .Where(a =>
                    a.Evaluation.CourseId == courseId &&
                    a.Evaluation.LessonId == lessonId &&
                    a.StudentId == studentId)
                .Select(a => new LessonAttemptSummary
                {
                    AttemptId = a.Id,
                    // ScoreObtained y Passed pueden ser null ⇒ aseguramos valores por defecto
                    Score = a.ScoreObtained ?? 0m,
                    Passed = a.Passed ?? false,
                    CreatedAt = a.StartedAt
                })
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}
