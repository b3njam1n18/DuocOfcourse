using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;

namespace Api.Service.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICoursesRepository _repository;

        public CourseService(ICoursesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CourseResponse> CreateCourseAsync(CreateCourseRequest request)
        {
            // Validaciones de entidades relacionadas
            if (await _repository.GetTeacherAsync(request.TeacherId) == null)
                throw new Exception("No se encontro profesor");

            if (await _repository.GetCategoryAsync(request.CategoryId) == null)
                throw new Exception("No se encontro categoria asociada");

            if (await _repository.GetSchoolAsync(request.SchoolId) == null)
                throw new Exception("No se encontro escuela asocidad");

            var course = new Courses
            {
                Title = request.Title,
                Description = request.Description,
                CoverImagePath = request.CoverImagePath, 

                TeacherId = request.TeacherId,
                CategoryId = request.CategoryId,
                SchoolId = request.SchoolId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsPublished = false
            };


            await _repository.AddAsync(course);

            return new CourseResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreatedAt = course.CreatedAt
            };
        }

        public async Task<CourseResponse> UpdateCourseAsync(long courseId, UpdateCourseRequest request)
        {
            var course = await _repository.GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course not found.");

            if (await _repository.GetCategoryAsync(request.CategoryId) == null)
                throw new Exception("Category not found.");

            if (await _repository.GetSchoolAsync(request.SchoolId) == null)
                throw new Exception("School not found.");

            // Actualizar entidad
            course.Title = request.Title;
            course.Description = request.Description;
            course.CategoryId = request.CategoryId;
            course.SchoolId = request.SchoolId;
            course.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(course);

            return new CourseResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreatedAt = course.CreatedAt
            };
        }

        public async Task DeleteCourseAsync(long courseId)
        {
            var course = await _repository.GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course not found.");

            // Validación: no borrar si tiene estudiantes
            if (await _repository.HasEnrollmentsAsync(courseId))
                throw new Exception("Cannot delete: course has enrolled students.");



            await _repository.DeleteAsync(course);
        }

        public async Task<TeacherCoursesListResponse> GetTeacherCoursesAsync(long teacherId)
        {
            if (!await _repository.TeacherExistsAsync(teacherId))
                throw new Exception("Teacher not found.");

            var courses = await _repository.GetByTeacherIdAsync(teacherId);

            var result = new TeacherCoursesListResponse
            {
                TeacherId = teacherId,
                Courses = courses.Select(c => new TeacherCourseItemResponse
                {
                    Id = c.Id,
                    Title = c.Title,
                    IsPublished = c.IsPublished,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return result;
        }

        public async Task<CourseDetailResponse> GetCourseByIdAsync(long courseId)
        {
            var course = await _repository.GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course not found.");

            var teacher = await _repository.GetTeacherAsync(course.TeacherId);

            string? teacherFullName = null;
            if (teacher != null)
                {
                    var parts = new[]
                    {
                teacher.FirstName,
                teacher.MiddleName,
                teacher.LastName,
                teacher.SecondLastName
            }.Where(p => !string.IsNullOrWhiteSpace(p));

            teacherFullName = string.Join(" ", parts);
            }

            return new CourseDetailResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                IsPublished = course.IsPublished,
                TeacherId = course.TeacherId,
                CategoryId = course.CategoryId,
                SchoolId = course.SchoolId,
                TeacherFullName = teacherFullName,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt
            };
        }

        public async Task<PublishCourseResponse> PublishCourseAsync(long courseId)
        {
            var course = await _repository.GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course not found.");

            if (course.IsPublished)
                throw new Exception("Course is already published.");

            course.IsPublished = true;
            course.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(course);

            return new PublishCourseResponse
            {
                CourseId = course.Id,
                IsPublished = true,
                PublishedAt = course.UpdatedAt ?? DateTime.UtcNow,
                Message = "Curso publicado con exito."
            };
        }

        public async Task<ModuleResponse> CreateModuleAsync(long courseId, CreateModuleRequest request)
        {
            if (!await _repository.CourseExistsAsync(courseId))
                throw new Exception("Course not found.");

            var nextPosition = await _repository.GetNextPositionAsync(courseId);

            var module = new Course_Modules
            {
                CourseId = courseId,
                Title = request.Title,
                Position = nextPosition
            };

            module = await _repository.AddAsync(module);

            return new ModuleResponse
            {
                Id = module.Id,
                CourseId = module.CourseId,
                Title = module.Title,
                Position = module.Position
            };

        }

        public async Task<LessonResponse> CreateLessonAsync(long moduleId, CreateLessonRequest request)
        {
            if (!await _repository.ModuleExistsAsync(moduleId))
                throw new Exception("No se encontro");

            var nextPosition = await _repository.GetNextLessonPositionAsync(moduleId);

            var lesson = new Lessons
            {
                ModuleId = moduleId,
                Title = request.Title,
                ContentUrl = request.ContentUrl,
                DurationMinutes = request.DurationMinutes,
                Position = nextPosition
            };

            lesson = await _repository.AddLessonAsync(lesson);

            return new LessonResponse
            {
                Id = lesson.Id,
                ModuleId = lesson.ModuleId,
                Title = lesson.Title,
                ContentUrl = lesson.ContentUrl,
                DurationMinutes = lesson.DurationMinutes,
                Position = lesson.Position
            };
        }

        public async Task<LessonResponse> UpdateLessonAsync(long lessonId, CreateLessonRequest request)
        {
            var lesson = await _repository.GetLessonByIdAsync(lessonId);

            if (lesson == null)
                throw new Exception("No encontrado.");

            lesson.Title = request.Title;
            lesson.ContentUrl = request.ContentUrl;
            lesson.DurationMinutes = request.DurationMinutes;

            lesson = await _repository.UpdateLessonAsync(lesson);

            return new LessonResponse
            {
                Id = lesson.Id,
                ModuleId = lesson.ModuleId,
                Title = lesson.Title,
                ContentUrl = lesson.ContentUrl,
                DurationMinutes = lesson.DurationMinutes,
                Position = lesson.Position
            };
        }

        public async Task<bool> DeleteLessonAsync(long lessonId)
        {
            var lesson = await _repository.GetLessonByIdAsync(lessonId);

            if (lesson == null)
                throw new Exception("no encontrado.");

            await _repository.DeleteLessonAsync(lesson);

            return true;
        }

        public async Task<MediaResponse> AddMediaToLessonAsync(long lessonId, UploadMediaRequest request)
        {
            var lesson = await _repository.GetLessonByIdAsync(lessonId);

            if (lesson == null)
                throw new Exception("No encontrado");

            // Obtener courseId desde el módulo / curso
            var courseId = lesson.Module.CourseId;

            var media = new Media
            {
                CourseId = courseId,
                Title = request.Title,
                MimeType = request.MimeType,
                StoragePath = request.StoragePath,
                SizeBytes = request.SizeBytes,
                UploadedBy = request.UploadedBy
            };

            media = await _repository.AddMediaAsync(media);

            return new MediaResponse
            {
                Id = media.Id,
                CourseId = media.CourseId,
                Title = media.Title,
                MimeType = media.MimeType,
                StoragePath = media.StoragePath,
                SizeBytes = media.SizeBytes,
                UploadedBy = media.UploadedBy
            };
        }

        public async Task<bool> DeleteMediaAsync(long mediaId)
        {
            var media = await _repository.GetMediaByIdAsync(mediaId);

            if (media == null)
                throw new Exception("no encontrado.");

            await _repository.DeleteMediaAsync(media);
            return true;
        }

     

        public async Task<List<LessonResponse>> GetLessonsByCourseAsync(long courseId)
        {
            if (!await _repository.CourseExistsAsync(courseId))
                throw new Exception("Course not found.");

            var lessons = await _repository.GetLessonsByCourseIdAsync(courseId);

            return lessons.Select(l => new LessonResponse
            {
                Id = l.Id,
                ModuleId = l.ModuleId,
                Title = l.Title,
                ContentUrl = l.ContentUrl,
                DurationMinutes = l.DurationMinutes,
                Position = l.Position
            }).ToList();
        }

        public async Task<List<CourseExploreItemResponse>> GetAllCoursesAsync()
        {
            // Aquí sí usamos _repository, porque estamos en el servicio
            var courses = await _repository.GetAllWithDetailsAsync();

            var result = new List<CourseExploreItemResponse>();

            foreach (var c in courses)
            {
                string? teacherFullName = null;
                if (c.Teacher != null)
                {
                    var parts = new[]
                    {
                        c.Teacher.FirstName,
                        c.Teacher.MiddleName,
                        c.Teacher.LastName,
                        c.Teacher.SecondLastName
                    }.Where(p => !string.IsNullOrWhiteSpace(p));

                    teacherFullName = string.Join(" ", parts);
                }

                var lessonsCount = 0;
                if (c.Modules != null)
                {
                    lessonsCount = c.Modules
                        .Where(m => m.Lessons != null)
                        .SelectMany(m => m.Lessons)
                        .Count();
                }

                result.Add(new CourseExploreItemResponse
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    SchoolName = c.School?.Name,
                    TeacherName = teacherFullName,
                    LessonsCount = lessonsCount,
                    IsPublished = c.IsPublished
                });
            }

            return result;
        }

        public async Task EnrollStudentAsync(long courseId, long studentId)
        {
            // Validar que el curso exista
            if (!await _repository.CourseExistsAsync(courseId))
                throw new Exception("Course not found.");

            // (Opcional) validar que el alumno exista si tienes algo como GetStudentAsync
            // if (await _repository.GetStudentAsync(studentId) == null)
            //     throw new Exception("Student not found.");

            // Validar que no esté ya inscrito
            if (await _repository.IsStudentEnrolledAsync(courseId, studentId))
                throw new Exception("Student already enrolled in this course.");

            await _repository.EnrollStudentAsync(courseId, studentId);
        }

        public async Task<List<long>> GetEnrolledCourseIdsByStudentAsync(long studentId)
        {
            return await _repository.GetEnrolledCourseIdsByStudentAsync(studentId);
        }




    } // ← cierre de la clase CourseService
}     // ← cierre del namespace Api.Service.Service


