using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.IServices;

namespace Api.Service.Service
{
    public class AttemptService : IAttemptService
    {
        private readonly IAttemptRepository _repo;

        public AttemptService(IAttemptRepository repo)
        {
            _repo = repo;
        }

        public async Task<SubmitLessonAttemptResponse> SubmitLessonAttemptAsync(
            long courseId,
            long lessonId,
            SubmitLessonAttemptRequest dto)
        {
            if (dto.StudentId <= 0)
                throw new Exception("StudentId inválido.");

            if (dto.Quizzes == null || dto.Quizzes.Count == 0)
                throw new Exception("No se enviaron respuestas.");

            var attempt = await _repo.CreateLessonAttemptAsync(courseId, lessonId, dto);

            var score = attempt.ScoreObtained ?? 0m;
            var passed = attempt.Passed ?? false;

            return new SubmitLessonAttemptResponse
            {
                AttemptId = attempt.Id,
                StudentId = attempt.StudentId,
                CourseId = courseId,
                LessonId = lessonId,
                Score = score,
                Passed = passed,
                CreatedAt = attempt.StartedAt
            };
        }

        public async Task<List<LessonAttemptSummary>> GetLessonAttemptsByStudentAsync(
            long courseId,
            long lessonId,
            long studentId)
        {
            return await _repo.GetLessonAttemptsByStudentAsync(courseId, lessonId, studentId);
        }
    }
}
