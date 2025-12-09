using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.Models;

namespace Api.Core.IServices
{
    public interface ICourseService
    {
        Task<CourseResponse> CreateCourseAsync(CreateCourseRequest request);
        Task<CourseResponse> UpdateCourseAsync(long courseId, UpdateCourseRequest request);
        Task DeleteCourseAsync(long courseId);

        Task<TeacherCoursesListResponse> GetTeacherCoursesAsync(long teacherId);
        Task<CourseDetailResponse> GetCourseByIdAsync(long courseId);
        Task<PublishCourseResponse> PublishCourseAsync(long courseId);
        Task<ModuleResponse> CreateModuleAsync(long courseId, CreateModuleRequest request);
        Task<LessonResponse> CreateLessonAsync(long moduleId, CreateLessonRequest request);
        Task<LessonResponse> UpdateLessonAsync(long lessonId, CreateLessonRequest request);

        Task<bool> DeleteLessonAsync(long lessonId);
        Task<MediaResponse> AddMediaToLessonAsync(long lessonId, UploadMediaRequest request);

        Task<bool> DeleteMediaAsync(long mediaId);

        
        Task<List<LessonResponse>> GetLessonsByCourseAsync(long courseId);

        Task<List<CourseExploreItemResponse>> GetAllCoursesAsync();

        Task EnrollStudentAsync(long courseId, long studentId);

        Task<List<long>> GetEnrolledCourseIdsByStudentAsync(long studentId);
    }
}
