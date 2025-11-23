using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.IRepositories;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Repositories
{
    public class EvaluationsRepository : IEvaluationRepository
    {
        private readonly AppDbContext _context;

        public EvaluationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> CourseExistsAsync(long courseId)
        {
            return _context.Courses
                .AnyAsync(c => c.Id == courseId);
        }

        public async Task<Evaluations> AddEvaluationAsync(Evaluations evaluation)
        {
            _context.Evaluations
                .Add(evaluation);
            await _context.SaveChangesAsync();
            return evaluation;
        }

        public async Task<Evaluations?> GetByIdAsync(long evaluationId)
        {
            return await _context.Evaluations
                .FirstOrDefaultAsync(e => e.Id == evaluationId);
        }

        public async Task<Evaluations> UpdateAsync(Evaluations evaluation)
        {
            _context.Evaluations.Update(evaluation);
            await _context
                .SaveChangesAsync();
            return evaluation;
        }

        public async Task DeleteAsync(Evaluations evaluation)
        {
            _context.Evaluations
                .Remove(evaluation);
            await _context.SaveChangesAsync();
        }

        public async Task<Evaluations> CreateEvaluationAsync(Evaluations evaluation)
        {
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();
            return evaluation;
        }

        public async Task<Questions> CreateQuestionAsync(Questions question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<Options> CreateOptionAsync(Options option)
        {
            _context.Options.Add(option);
            await _context.SaveChangesAsync();
            return option;
        }

        public async Task<Evaluations?> GetEvaluationFullAsync(long evaluationId)
        {
            return await _context.Evaluations
                .Include(e => e.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(e => e.Id == evaluationId);
        }
        //preguntas examen
        public async Task<Questions?> GetByIdAsyncQuestion(long id)
        {
            return await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
        }
        
        public async Task UpdateAsync(Questions question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }
        //opciones examen
        public async Task<Options?> GetByIdAsyncOption(long id)
        {
            return await _context.Options.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateAsync(Options option)
        {
            _context.Options.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Questions question)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Options option)
        {
            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
        }

        public async Task<Attempts> CreateAttemptAsync(Attempts attempt)
        {
            _context.Attempts.Add(attempt);
            await _context.SaveChangesAsync();
            return attempt;
        }

        public async Task<Answers> CreateAsyncAnswer(Answers answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<Attempts?> GetAttemptWithEvaluationAsync(long attemptId)
        {
            return await _context.Attempts
                .Include(a => a.Evaluation)
                .Include(a => a.Answers)
                .ThenInclude(ans => ans.Question)
                .FirstOrDefaultAsync(a => a.Id == attemptId);
        }

        public async Task UpdateAsyncAttempt(Attempts attempt)
        {
            _context.Attempts.Update(attempt);
            await _context.SaveChangesAsync();
        }

        public async Task<Attempts?> GetAttemptFullAsync(long attemptId)
        {
            return await _context.Attempts
                .Include(a => a.Evaluation)
                .Include(a => a.Answers)
                    .ThenInclude(ans => ans.Question)
                        .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(a => a.Id == attemptId);
        }

        public async Task<List<Evaluations>> GetEvaluationsByCourseAsync(long courseId, bool? published)
        {
            var query = _context.Evaluations
                .Where(e => e.CourseId == courseId);

            if (published.HasValue)
                query = query.Where(e => e.IsPublished == published.Value);

            return await query
                .OrderBy(e => e.DueAt ?? DateTime.MaxValue)
                .ToListAsync();
        }

    }

}
