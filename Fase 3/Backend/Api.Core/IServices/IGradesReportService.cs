using System.Threading.Tasks;

namespace Api.Core.IServices
{
    public interface IGradesReportService
    {
        Task<byte[]> GenerateTeacherCourseGradesExcelAsync(long teacherId, long courseId);
    }
}
