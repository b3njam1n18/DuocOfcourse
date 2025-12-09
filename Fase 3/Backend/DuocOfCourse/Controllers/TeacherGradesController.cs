using Api.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/teachers/{teacherId}/courses")]
    public class TeacherGradesController : ControllerBase
    {
        private readonly IGradesReportService _gradesReportService;

        public TeacherGradesController(IGradesReportService gradesReportService)
        {
            _gradesReportService = gradesReportService;
        }

        // GET: /api/teachers/{teacherId}/courses/{courseId}/grades/excel
        [HttpGet("{courseId}/grades/excel")]
        public async Task<IActionResult> DownloadCourseGradesExcel(long teacherId, long courseId)
        {
            var fileBytes = await _gradesReportService
                .GenerateTeacherCourseGradesExcelAsync(teacherId, courseId);

            var fileName = $"notas_curso_{courseId}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }
    }
}
