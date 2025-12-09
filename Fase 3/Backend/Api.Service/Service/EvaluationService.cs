using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Service.Service
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _repository;

        public EvaluationService(IEvaluationRepository repository)
        {
            _repository = repository;
        }

        public async Task<EvaluationResponse> CreateEvaluationAsync(long courseId, CreateEvaluationRequest request)
        {
            if (!await _repository.CourseExistsAsync(courseId))
                throw new Exception("Course not found.");

            var evaluation = new Evaluations
            {
                CourseId = courseId,
                LessonId = request.LessonId,
                Title = request.Title,
                Description = request.Description,
                DueAt = request.DueAt,
                Type = request.Type,
                PassThreshold = request.PassThreshold,

                // ✔ AQUÍ EL CAMBIO
                IsFinalExam = request.IsFinalExam,

                CreatedAt = DateTime.UtcNow
            };

            evaluation = await _repository.AddEvaluationAsync(evaluation);

            return new EvaluationResponse
            {
                Id = evaluation.Id,
                CourseId = evaluation.CourseId,
                Title = evaluation.Title,
                Description = evaluation.Description,
                DueAt = evaluation.DueAt,
                Type = evaluation.Type,
                PassThreshold = evaluation.PassThreshold,
                IsFinalExam = evaluation.IsFinalExam,
                CreatedAt = evaluation.CreatedAt
            };
        }


        public async Task<Evaluations> CreateEvaluationAsync(CreateEvaluation dto)
        {
            var evaluation = new Evaluations
            {
                CourseId = dto.CourseId,
                LessonId = dto.LessonId,
                Title = dto.Title,
                Description = dto.Description,
                DueAt = dto.DueAt,
                Type = dto.Type,
                PassThreshold = dto.PassThreshold,
                IsFinalExam = dto.IsFinalExam,
                CreatedAt = DateTime.UtcNow,
            };

            return await _repository.CreateEvaluationAsync(evaluation);
        }


        public async Task<Questions> CreateQuestionAsync(long evaluationId, CreateQuestion dto)
        {
            var question = new Questions
            {
                EvaluationId = evaluationId,
                Prompt = dto.Prompt,
                Type = dto.Type,
                Points = dto.Points,
                Position = dto.Position
            };

            return await _repository.CreateQuestionAsync(question);
        }

        public async Task<Options> CreateOptionAsync(long questionId, CreateOption dto)
        {
            var option = new Options
            {
                QuestionId = questionId,
                Text = dto.Text,
                IsCorrect = dto.IsCorrect
            };

            return await _repository.CreateOptionAsync(option);
        }

        public async Task<EvaluationFullResponse> GetEvaluationFullAsync(long evaluationId)
        {
            var evaluation = await _repository.GetEvaluationFullAsync(evaluationId);

            if (evaluation == null)
                throw new Exception("Evaluation not found.");

            return new EvaluationFullResponse
            {
                Id = evaluation.Id,
                CourseId = evaluation.CourseId,
                LessonId = evaluation.LessonId,
                Title = evaluation.Title,
                Description = evaluation.Description,
                DueAt = evaluation.DueAt,
                Type = evaluation.Type,
                PassThreshold = evaluation.PassThreshold,
                IsFinalExam = evaluation.IsFinalExam,
                CreatedAt = evaluation.CreatedAt,

                Questions = evaluation.Questions
                    .OrderBy(q => q.Position)
                    .Select(q => new QuestionResponse
                    {
                        Id = q.Id,
                        Prompt = q.Prompt,
                        Type = q.Type,
                        Points = q.Points,
                        Position = q.Position,

                        Options = q.Options
                            .Select(o => new OptionResponse
                            {
                                Id = o.Id,
                                Text = o.Text,
                                IsCorrect = o.IsCorrect
                            })
                            .ToList()
                    })
                    .ToList()
            };
        }

        public async Task<Evaluations> UpdateEvaluationAsync(long evaluationId, UpdateEvaluationDto dto)
        {
            var eval = await _repository.GetByIdAsync(evaluationId);

            if (eval == null)
                throw new Exception("Evaluation not found.");

            eval.Title = dto.Title;
            eval.Description = dto.Description;
            eval.DueAt = dto.DueAt;
            eval.Type = dto.Type;
            eval.PassThreshold = dto.PassThreshold;
            eval.IsFinalExam = dto.IsFinalExam;
            eval.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(eval);

            return eval;
        }

        public async Task<Questions> UpdateQuestionAsync(long questionId, CreateQuestion dto)
        {
            var question = await _repository.GetByIdAsyncQuestion(questionId);

            if (question == null)
                throw new Exception("Question not found.");

            question.Prompt = dto.Prompt;
            question.Type = dto.Type;
            question.Points = dto.Points;
            question.Position = dto.Position;

            await _repository.UpdateAsync(question);

            return question;
        }

        public async Task<Options> UpdateOptionAsync(long optionId, UpdateOption dto)
        {
            var option = await _repository.GetByIdAsyncOption(optionId);

            if (option == null)
                throw new Exception("Option not found.");

            option.Text = dto.Text;
            option.IsCorrect = dto.IsCorrect;

            await _repository.UpdateAsync(option);

            return option;
        }

        public async Task DeleteQuestionAsync(long questionId)
        {
            var question = await _repository.GetByIdAsync(questionId);

            if (question == null)
                throw new Exception("Question not found.");

            await _repository.DeleteAsync(question);
        }

        public async Task DeleteOptionAsync(long optionId)
        {
            var option = await _repository.GetByIdAsync(optionId);

            if (option == null)
                throw new Exception("Option not found.");

            await _repository.DeleteAsync(option);
        }


        public async Task<Attempts> CreateAttemptAsync(long evaluationId, CreateAttempt dto)
        {
            var attempt = new Attempts
            {
                EvaluationId = evaluationId,
                StudentId = dto.StudentId,
                StartedAt = DateTime.UtcNow,
                Score = null
            };

            return await _repository.CreateAttemptAsync(attempt);
        }

        public async Task<List<Answers>> SubmitAnswersAsync(long attemptId, SubmitAnswersRequest request)
        {
            var results = new List<Answers>();

            foreach (var dto in request.Answers)
            {
                // Detectar si es OPEN o OPTION
                bool? isCorrect = null;
                decimal? awarded = null;

                // Buscar info de la pregunta y opción
                var question = await _repository.GetByIdAsyncQuestion(dto.QuestionId);

                if (question == null)
                    throw new Exception("Question not found.");

                //si encontro la pregunta
                if (dto.OptionId.HasValue)
                {
                    var option = await _repository.GetByIdAsyncOption(dto.OptionId.Value);
                    if (option == null)
                        throw new Exception("Option not found.");

                    isCorrect = option.IsCorrect;
                    awarded = option.IsCorrect ? question.Points : 0;
                }

                // Construir respuesta
                var ans = new Answers
                {
                    AttemptId = attemptId,
                    QuestionId = dto.QuestionId,
                    OptionId = dto.OptionId,
                    OpenText = dto.OpenText,
                    IsCorrect = isCorrect,
                    PointsAwarded = awarded
                };

                // Guardar respuesta
                var saved = await _repository.CreateAsyncAnswer(ans);
                results.Add(saved);
            }

            return results;
        }

        public async Task<FinishAttemptResponse> FinishAttemptAsync(long attemptId)
        {
            var attempt = await _repository.GetAttemptWithEvaluationAsync(attemptId);

            if (attempt == null)
                throw new Exception("Attempt not found.");

            if (attempt.SubmittedAt != null)
                throw new Exception("Attempt already finished.");

            decimal totalPoints = attempt.Answers.Sum(a => a.Question.Points);
            decimal earnedPoints = attempt.Answers.Sum(a => a.PointsAwarded ?? 0);

            decimal percentage = totalPoints > 0 ? earnedPoints / totalPoints : 0;

            bool passed = percentage >= attempt.Evaluation.PassThreshold;

            attempt.Score = earnedPoints;
            attempt.SubmittedAt = DateTime.UtcNow;

            await _repository.UpdateAsyncAttempt(attempt);

            return new FinishAttemptResponse
            {
                AttemptId = attempt.Id,
                Score = earnedPoints,
                TotalPoints = totalPoints,
                Percentage = percentage,
                Passed = passed
            };
        }

        public async Task<AttemptResultResponse> GetAttemptResultAsync(long attemptId)
        {
            var attempt = await _repository.GetAttemptFullAsync(attemptId);

            if (attempt == null)
                throw new Exception("Attempt not found.");

            decimal totalPoints = attempt.Answers.Sum(a => a.Question.Points);
            decimal earnedPoints = attempt.Answers.Sum(a => a.PointsAwarded ?? 0);
            decimal percentage = totalPoints > 0 ? earnedPoints / totalPoints : 0;

            var response = new AttemptResultResponse
            {
                AttemptId = attempt.Id,
                Score = earnedPoints,
                TotalPoints = totalPoints,
                Percentage = percentage,
                Passed = percentage >= attempt.Evaluation.PassThreshold,
            };

            // Build question results
            foreach (var ans in attempt.Answers)
            {
                var q = ans.Question;

                var qResult = new QuestionResult
                {
                    QuestionId = q.Id,
                    Prompt = q.Prompt,
                    Points = q.Points,
                    SelectedOptionId = ans.OptionId,
                    IsCorrect = ans.IsCorrect,
                    OpenText = ans.OpenText,
                    Options = q.Options.Select(o => new OptionResult
                    {
                        Id = o.Id,
                        Text = o.Text,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                };

                response.Questions.Add(qResult);
            }

            return response;
        }

        public async Task<Evaluations> PublishEvaluationAsync(long evaluationId, bool publish)
        {
            var eval = await _repository.GetByIdAsync(evaluationId);

            if (eval == null)
                throw new Exception("Evaluation not found.");

            eval.IsPublished = publish;
            eval.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(eval);

            return eval;
        }

        public async Task<List<EvaluationListItemDto>> GetEvaluationsByCourseAsync(long courseId, bool? published)
        {
            var list = await _repository.GetEvaluationsByCourseAsync(courseId, published);

            return list.Select(e => new EvaluationListItemDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                IsPublished = e.IsPublished,
                IsFinalExam = e.IsFinalExam,
                DueAt = e.DueAt
            }).ToList();
        }

        public async Task<List<EvaluationFullResponse>> GetEvaluationsByLessonAsync(long courseId, long lessonId, bool? published)
        {
            var list = await _repository.GetEvaluationsByLessonAsync(courseId, lessonId, published);

            return list.Select(evaluation => new EvaluationFullResponse
            {
                Id = evaluation.Id,
                CourseId = evaluation.CourseId,
                LessonId = evaluation.LessonId,
                Title = evaluation.Title,
                Description = evaluation.Description,
                DueAt = evaluation.DueAt,
                Type = evaluation.Type,
                PassThreshold = evaluation.PassThreshold,
                IsFinalExam = evaluation.IsFinalExam,
                CreatedAt = evaluation.CreatedAt,
                Questions = evaluation.Questions
                    .OrderBy(q => q.Position)
                    .Select(q => new QuestionResponse
                    {
                        Id = q.Id,
                        Prompt = q.Prompt,
                        Type = q.Type,
                        Points = q.Points,
                        Position = q.Position,
                        Options = q.Options
                            .Select(o => new OptionResponse
                            {
                                Id = o.Id,
                                Text = o.Text,
                                IsCorrect = o.IsCorrect
                            })
                            .ToList()
                    })
                    .ToList()
            }).ToList();
        }

        public async Task<AttemptResultResponse?> GetLastAttemptByStudentAsync(long evaluationId, long studentId)
        {
            // Usamos el repo para buscar el último intento del alumno
            var attempt = await _repository.GetLastAttemptByStudentAsync(evaluationId, studentId);

            if (attempt == null)
                return null;

            // Reutilizamos la lógica que ya existe en GetAttemptResultAsync
            return await GetAttemptResultAsync(attempt.Id);
        }




    }
}
