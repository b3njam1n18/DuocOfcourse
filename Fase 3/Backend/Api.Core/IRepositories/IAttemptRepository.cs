using Api.Core.DTO;
using Api.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.IRepositories
{
    public interface IAttemptRepository
    {
        Task<Attempts> CreateLessonAttemptAsync(long courseId, long lessonId, SubmitLessonAttemptRequest dto);

        Task<List<LessonAttemptSummary>> GetLessonAttemptsByStudentAsync(
            long courseId,
            long lessonId,
            long studentId);
    }
}
