using System.Collections.Generic;

namespace Api.Core.DTO
{
    public class StudentCourseGradeDto
    {
        public long StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;

        // Promedio de notas de las clases (1.0 a 7.0)
        public decimal? LessonsAverage { get; set; }

        // Nota del examen final (1.0 a 7.0)
        public decimal? FinalExamGrade { get; set; }

        // Nota final del curso (1.0 a 7.0)
        public decimal? FinalCourseGrade { get; set; }

        // 🔹 Detalle por clase: key = lessonId, value = nota de la clase (1.0 a 7.0) o null si no tiene intento
        public Dictionary<long, decimal?> LessonGrades { get; set; } = new();
    }
}
