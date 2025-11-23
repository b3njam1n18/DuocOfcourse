using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.IRepositories;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly AppDbContext _context;

        public CoursesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Courses?> GetByIdAsync(long courseId)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public Task<bool> HasEnrollmentsAsync(long courseId)
        {
            return _context.Enrollments.AnyAsync(e => e.CourseId == courseId);
        }

        public async Task DeleteAsync(Courses course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Courses> AddAsync(Courses course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Courses> UpdateAsync(Courses course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public Task<Users?> GetTeacherAsync(long teacherId)
        {
            return _context.Users
                .FirstOrDefaultAsync(u => u.Id == teacherId);
        }

        public Task<Course_Categories?> GetCategoryAsync(long categoryId)
        {
            return _context.Course_Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public Task<Schools?> GetSchoolAsync(long schoolId)
        {
            return _context.Schools
                .FirstOrDefaultAsync(s => s.Id == schoolId);
        }

        public async Task<List<Courses>> GetByTeacherIdAsync(long teacherId)
        {
            return await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public Task<bool> TeacherExistsAsync(long teacherId)
        {
            return _context.Users.AnyAsync(u => u.Id == teacherId);
        }

        public Task<bool> CourseExistsAsync(long courseId)
        {
            return _context.Courses.AnyAsync(c => c.Id == courseId);
        }

        public async Task<int> GetNextPositionAsync(long courseId)
        {
            var lastModule = await _context.Course_Modules
                .Where(m => m.CourseId == courseId)
                .OrderByDescending(m => m.Position)
                .FirstOrDefaultAsync();

            return lastModule == null ? 1 : lastModule.Position + 1;
        }

        public async Task<Course_Modules> AddAsync(Course_Modules module)
        {
            _context.Course_Modules.Add(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<bool> ModuleExistsAsync(long moduleId)
        {
            return await _context.Course_Modules.AnyAsync(m => m.Id == moduleId);
        }

        public async Task<int> GetNextLessonPositionAsync(long moduleId)
        {
            var lastLesson = await _context.Lessons
                .Where(l => l.ModuleId == moduleId)
                .OrderByDescending(l => l.Position)
                .FirstOrDefaultAsync();

            return lastLesson == null ? 1 : lastLesson.Position + 1;
        }

        public async Task<Lessons> AddLessonAsync(Lessons lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }
        public async Task<Lessons?> GetLessonByIdAsync(long lessonId)
        {
            return await _context.Lessons
                .FirstOrDefaultAsync(l => l.Id == lessonId);
        }

        public async Task<Lessons> UpdateLessonAsync(Lessons lesson)
        {
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task DeleteLessonAsync(Lessons lesson)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<Media> AddMediaAsync(Media media)
        {
            _context.Media.Add(media);
            await _context.SaveChangesAsync();
            return media;
        }

        public async Task<Media?> GetMediaByIdAsync(long mediaId)
        {
            return await _context.Media
                .FirstOrDefaultAsync(m => m.Id == mediaId);
        }

        public async Task DeleteMediaAsync(Media media)
        {
            _context.Media
                .Remove(media);
            await _context.SaveChangesAsync();
        }

       

    }
}

