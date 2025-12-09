using Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.IServices
{
    public interface IAttemptService
    {
        Task<SubmitLessonAttemptResponse> SubmitLessonAttemptAsync(
            long courseId,
            long lessonId,
            SubmitLessonAttemptRequest dto);

        Task<List<LessonAttemptSummary>> GetLessonAttemptsByStudentAsync(
            long courseId,
            long lessonId,
            long studentId);
    }
}
