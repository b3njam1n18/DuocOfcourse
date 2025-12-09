using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IRepositories
{
    public interface IEvaluationRepository
    {
        Task<bool> CourseExistsAsync(long courseId);

        Task<Evaluations> AddEvaluationAsync(Evaluations evaluation);
        Task<Evaluations?> GetByIdAsync(long evaluationId);
        Task<Evaluations> UpdateAsync(Evaluations evaluation);
        Task DeleteAsync(Evaluations evaluation);

        Task<Evaluations> CreateEvaluationAsync(Evaluations evaluation);
        Task<Questions> CreateQuestionAsync(Questions question);
        Task<Options> CreateOptionAsync(Options option);

        Task<Evaluations?> GetEvaluationFullAsync(long evaluationId);

        // Preguntas
        Task<Questions?> GetByIdAsyncQuestion(long id);
        Task UpdateAsync(Questions question);
        Task DeleteAsync(Questions question);

        // Opciones
        Task<Options?> GetByIdAsyncOption(long id);
        Task UpdateAsync(Options option);
        Task DeleteAsync(Options option);

        // Intentos
        Task<Attempts> CreateAttemptAsync(Attempts attempt);
        Task<Answers> CreateAsyncAnswer(Answers answer);
        Task<Attempts?> GetAttemptWithEvaluationAsync(long attemptId);
        Task UpdateAsyncAttempt(Attempts attempt);
        Task<Attempts?> GetAttemptFullAsync(long attemptId);

        Task<List<Evaluations>> GetEvaluationsByCourseAsync(long courseId, bool? published);

        
        Task<List<Evaluations>> GetEvaluationsByLessonAsync(long courseId, long lessonId, bool? published);

        Task<Attempts?> GetLastAttemptByStudentAsync(long evaluationId, long studentId);

    }
}
