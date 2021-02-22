﻿using CourseCatalog.Application.Contracts;
using CourseCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(CourseDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Course> GetCourseByCourseNumber(string courseNumber)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseNumber == courseNumber);

            return course;
        }

        public async Task<Course> GetCourseWithDetails(int courseId)
        {
            var course = await _dbContext.Courses
                .Include(c => c.CourseLevel)
                .Include(c => c.Subject)
                .Include(c => c.HighGrade)
                .Include(c => c.LowGrade)
                .Include(c => c.GradeScale)
                .Include(c => c.ScedCategory)
                .FirstOrDefaultAsync(x => x.CourseId == courseId);

            return course;
        }
    }
}
