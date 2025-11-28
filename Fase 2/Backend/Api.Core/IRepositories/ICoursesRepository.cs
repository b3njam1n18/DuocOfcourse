using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IRepositories
{
    public interface ICoursesRepository
    {
        Task<Courses> AddAsync(Courses course);
        Task<Courses?> GetByIdAsync(long courseId);
        Task<Courses> UpdateAsync(Courses course);

        Task<Users?> GetTeacherAsync(long teacherId);
        Task<Course_Categories?> GetCategoryAsync(long categoryId);
        Task<Schools?> GetSchoolAsync(long schoolId);

        Task DeleteAsync(Courses course);
        Task<bool> HasEnrollmentsAsync(long courseId);

        Task<List<Courses>> GetByTeacherIdAsync(long teacherId);
        Task<bool> TeacherExistsAsync(long teacherId);
        Task<bool> CourseExistsAsync(long courseId);
        Task<int> GetNextPositionAsync(long courseId);
        Task<Course_Modules> AddAsync(Course_Modules module);
        Task<bool> ModuleExistsAsync(long moduleId);
        Task<int> GetNextLessonPositionAsync(long moduleId);
        Task<Lessons> AddLessonAsync(Lessons lesson);
        Task<Lessons?> GetLessonByIdAsync(long lessonId);
        Task<Lessons> UpdateLessonAsync(Lessons lesson);
        Task DeleteLessonAsync(Lessons lesson);

        Task<Media> AddMediaAsync(Media media);
        Task<Media?> GetMediaByIdAsync(long mediaId);
        Task DeleteMediaAsync(Media media);

 
    }
}
