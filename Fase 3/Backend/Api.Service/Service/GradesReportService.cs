using Api.Core.DTO;
using Api.Core.IServices;
using Api.Repository;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace Api.Service.Service
{
    public class GradesReportService : IGradesReportService
    {
        private readonly AppDbContext _db;

        public GradesReportService(AppDbContext db)
        {
            _db = db;
        }

        // helper: pasa porcentaje (0–1 o 0–100) a nota 1.0–7.0
        private decimal? PercentageToGrade(decimal? raw)
        {
            if (raw == null)
                return null;

            var p = raw.Value;

            // si viene en 0–100, lo normalizamos
            if (p > 1m)
                p = p / 100m;

            if (p < 0m) p = 0m;
            if (p > 1m) p = 1m;

            return 1m + 6m * p;
        }

        public async Task<byte[]> GenerateTeacherCourseGradesExcelAsync(long teacherId, long courseId)
        {
            // 1) Traer curso + validar que sea del profe
            var course = await _db.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == courseId && c.TeacherId == teacherId);

            if (course == null)
                throw new Exception("Curso no encontrado o no pertenece a este profesor.");

            // Nombre “seguro” del curso (solo Title)
            var courseTitle = string.IsNullOrWhiteSpace(course.Title)
                ? $"Curso {courseId}"
                : course.Title;

            // 2) Traer las clases del curso SIN usar CourseId en Lessons
            //    Tomamos las lecciones que tengan al menos una evaluación de este curso (no final)
            var lessons = await _db.Lessons
                .Where(l => _db.Evaluations.Any(e =>
                    e.CourseId == courseId &&
                    e.LessonId == l.Id &&
                    !e.IsFinalExam))
                .OrderBy(l => l.Position)
                .ThenBy(l => l.Id)
                .ToListAsync();

            var lessonIds = lessons.Select(l => l.Id).ToList();

            // 3) Traer evaluación final del curso (IsFinalExam = true)
            var finalExam = await _db.Evaluations
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.IsFinalExam);

            // 4) Traer todos los estudiantes inscritos
            var enrollments = await _db.Enrollments
                .Include(e => e.Student)
                .Where(e => e.CourseId == courseId)
                .ToListAsync();

            var rows = new List<StudentCourseGradeDto>();

            foreach (var enr in enrollments)
            {
                var student = enr.Student;

                // Nombre alumno
                var nameParts = new[]
                {
                    student.FirstName,
                    student.MiddleName,
                    student.LastName,
                    student.SecondLastName
                }
                .Where(p => !string.IsNullOrWhiteSpace(p));

                var fullName = string.Join(" ", nameParts);
                if (string.IsNullOrWhiteSpace(fullName))
                    fullName = $"Alumno {student.Id}";

                // --- Notas por clase (detalladas) ---
                var lessonGradesDict = new Dictionary<long, decimal?>();
                var lessonGradesForAverage = new List<decimal>();

                foreach (var lesson in lessons)
                {
                    // Tomamos el último intento (Attempts) para alguna evaluación de esa clase
                    var lessonAttempt = await _db.Attempts
                        .Include(a => a.Evaluation)
                        .Where(a =>
                            a.Evaluation.CourseId == courseId &&
                            a.Evaluation.LessonId == lesson.Id &&
                            a.StudentId == student.Id)
                        .OrderByDescending(a => a.SubmittedAt)
                        .FirstOrDefaultAsync();

                    if (lessonAttempt == null)
                    {
                        // No tiene intento en esta clase → nota null
                        lessonGradesDict[lesson.Id] = null;
                        continue;
                    }

                    var grade = PercentageToGrade(lessonAttempt.Score);
                    lessonGradesDict[lesson.Id] = grade;

                    if (grade.HasValue)
                        lessonGradesForAverage.Add(grade.Value);
                }

                decimal? lessonsAverage = null;
                if (lessonGradesForAverage.Count > 0)
                    lessonsAverage = lessonGradesForAverage.Average();

                // --- Nota examen final ---
                decimal? finalExamGrade = null;

                if (finalExam != null)
                {
                    var lastExamAttempt = await _db.Attempts
                        .Where(a => a.EvaluationId == finalExam.Id && a.StudentId == student.Id)
                        .OrderByDescending(a => a.SubmittedAt)
                        .FirstOrDefaultAsync();

                    if (lastExamAttempt != null)
                    {
                        finalExamGrade = PercentageToGrade(lastExamAttempt.Score);
                    }
                }

                // --- Nota final 60/40 ---
                decimal? finalCourseGrade = null;

                if (lessonsAverage != null && finalExamGrade != null)
                    finalCourseGrade = lessonsAverage * 0.6m + finalExamGrade * 0.4m;
                else if (lessonsAverage != null)
                    finalCourseGrade = lessonsAverage;
                else if (finalExamGrade != null)
                    finalCourseGrade = finalExamGrade;

                rows.Add(new StudentCourseGradeDto
                {
                    StudentId = student.Id,
                    StudentFullName = fullName,
                    StudentEmail = student.Email ?? string.Empty,
                    LessonsAverage = lessonsAverage,
                    FinalExamGrade = finalExamGrade,
                    FinalCourseGrade = finalCourseGrade,
                    LessonGrades = lessonGradesDict
                });
            }

            // 5) Construir el Excel con ClosedXML
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Notas");

            // ===== ENCABEZADOS DINÁMICOS =====
            int headerRow = 1;
            int col = 1;

            ws.Cell(headerRow, col++).Value = "Curso";
            ws.Cell(headerRow, col++).Value = "ID Alumno";
            ws.Cell(headerRow, col++).Value = "Nombre Alumno";
            ws.Cell(headerRow, col++).Value = "Correo";

            // Una columna por cada clase
            foreach (var lesson in lessons)
            {
                ws.Cell(headerRow, col++).Value = $"Clase {lesson.Position}";
            }

            ws.Cell(headerRow, col++).Value = "Prom. Clases (60%)";
            ws.Cell(headerRow, col++).Value = "Examen (40%)";
            ws.Cell(headerRow, col).Value = "Nota Final";

            ws.Row(headerRow).Style.Font.Bold = true;

            // ===== FILAS POR ALUMNO =====
            int rowIndex = 2;
            foreach (var r in rows)
            {
                col = 1;
                ws.Cell(rowIndex, col++).Value = courseTitle;
                ws.Cell(rowIndex, col++).Value = r.StudentId;
                ws.Cell(rowIndex, col++).Value = r.StudentFullName;
                ws.Cell(rowIndex, col++).Value = r.StudentEmail;

                // Notas por clase
                foreach (var lesson in lessons)
                {
                    if (r.LessonGrades != null &&
                        r.LessonGrades.TryGetValue(lesson.Id, out var grade) &&
                        grade.HasValue)
                    {
                        ws.Cell(rowIndex, col++).Value = grade.Value.ToString("0.0");
                    }
                    else
                    {
                        ws.Cell(rowIndex, col++).Value = "-";
                    }
                }

                ws.Cell(rowIndex, col++).Value = r.LessonsAverage?.ToString("0.0");
                ws.Cell(rowIndex, col++).Value = r.FinalExamGrade?.ToString("0.0");
                ws.Cell(rowIndex, col).Value = r.FinalCourseGrade?.ToString("0.0");

                rowIndex++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
