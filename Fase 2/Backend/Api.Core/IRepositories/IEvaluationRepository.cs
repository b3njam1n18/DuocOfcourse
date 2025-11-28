using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IRepositories
{
    public interface IEvaluationRepository
    {

        //ver si curso existe
        Task<bool> CourseExistsAsync(long courseId);
        //agregar evaluacion
        Task<Evaluations> AddEvaluationAsync(Evaluations evaluation);
        Task<Evaluations?> GetEvaluationFullAsync(long evaluationId);
        Task<Evaluations> CreateEvaluationAsync(Evaluations evaluation);

        //conseguir evaluadcion por id
        Task<Evaluations?> GetByIdAsync(long evaluationId);
        //MOdificar evaluacion
        Task<Evaluations> UpdateAsync(Evaluations evaluation);
        //borrar evaluacion
        Task DeleteAsync(Evaluations evaluation);
        
        //crear pregunta examen
        Task<Questions> CreateQuestionAsync(Questions question);
        //crear opcion examen
        Task<Options> CreateOptionAsync(Options option);

        

        //conocer pregunta por ID
        Task<Questions?> GetByIdAsyncQuestion(long id);
        //actualizar pregunta
        Task UpdateAsync(Questions question);
        //actualizar opcion
        Task UpdateAsync(Options option);

        //conocer opcion por id
        Task<Options?> GetByIdAsyncOption(long id);
        //borrar pregunta
        Task DeleteAsync(Questions question);

        //borraropcion
        Task DeleteAsync(Options option);

        //registrar intento aulmno
        Task<Attempts> CreateAttemptAsync(Attempts attempt);

        Task<Answers> CreateAsyncAnswer(Answers answer);

        Task<Attempts?> GetAttemptWithEvaluationAsync(long attemptId);
        Task UpdateAsyncAttempt(Attempts attempt);
        Task<Attempts?> GetAttemptFullAsync(long attemptId);

        Task<List<Evaluations>> GetEvaluationsByCourseAsync(long courseId, bool? published);
    }
}
