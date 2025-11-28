using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.Models;

namespace Api.Core.IServices
{
    public interface IEvaluationService
    {
        Task<EvaluationResponse> CreateEvaluationAsync(long courseId, CreateEvaluationRequest request);

        //este es el que permite que el profe cree la evaluacion asociada a un cursos 
        Task<Evaluations> CreateEvaluationAsync(CreateEvaluation dto);

        Task<Questions> CreateQuestionAsync(long evaluationId, CreateQuestion dto);

        Task<Options> CreateOptionAsync(long questionId, CreateOption dto);

        Task<EvaluationFullResponse> GetEvaluationFullAsync(long evaluationId);

        Task<Evaluations> UpdateEvaluationAsync(long evaluationId, UpdateEvaluationDto dto);

        Task<Questions> UpdateQuestionAsync(long questionId, CreateQuestion dto);

        Task<Options> UpdateOptionAsync(long optionId, UpdateOption dto);

        Task DeleteQuestionAsync(long questionId);

        Task DeleteOptionAsync(long optionId);

        Task<Attempts> CreateAttemptAsync(long evaluationId, CreateAttempt dto);

        Task<List<Answers>> SubmitAnswersAsync(long attemptId, SubmitAnswersRequest request);

        Task<FinishAttemptResponse> FinishAttemptAsync(long attemptId);

        Task<AttemptResultResponse> GetAttemptResultAsync(long attemptId);

        Task<Evaluations> PublishEvaluationAsync(long evaluationId, bool publish);

        Task<List<EvaluationListItemDto>> GetEvaluationsByCourseAsync(long courseId, bool? published);

    }

}
