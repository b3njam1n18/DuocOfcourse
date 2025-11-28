using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class TeacherCoursesListResponse
    {
        public long TeacherId { get; set; }
        public List<TeacherCourseItemResponse> Courses { get; set; } = new();
    }

}
