using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using ComparativeGraderAPI.Security.Security_Interfaces;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static ComparativeGraderAPI.Application.Courses.Edit;

namespace ComparativeGraderAPI.Application.ServiceLayers.DataAccess
{
    public class CoursesAccess : ICourseAccess
    {
        private readonly GradingDataContext _context;
        private readonly IUserAccessor _userAccessor;

        public CoursesAccess(GradingDataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<bool> AddCourse(Course course)
        {
            course.ProfessorUserId = _userAccessor.GetCurrentUserId();

            _context.Courses.Add(course);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return success;

            throw new Exception("Problem saving changes");
        }

        public async Task<IEnumerable<Course>> ListCourses()
        {
            var profId = _userAccessor.GetCurrentUserId();

            var courses = _context.Courses.Where(c => c.ProfessorUserId == profId);

            return courses;
        }

        public async Task<bool> EditCourse(Course courseToEdit, Command request)//This probably shouldnt handle the command, fix this later.
        {
            //var courseToEdit = await _context.Courses.FindAsync(courseId);

            courseToEdit.SemesterId = request.SemesterId > 0 && courseToEdit.SemesterId != request.SemesterId ? request.SemesterId : courseToEdit.SemesterId;
            courseToEdit.CourseName = request.CourseName ?? courseToEdit.CourseName;
            courseToEdit.CourseDescription = request.CourseDescription ?? courseToEdit.CourseDescription;
            courseToEdit.Year = request.Year > 0 && courseToEdit.Year != request.Year ? request.Year : courseToEdit.Year;

            var success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            //TODO: Implement some kind of cascade delete to get rid of related assignments.

            var course = await _context.Courses.FindAsync(id);

            _context.Remove(course);

            var success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<Course> CourseDetails(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            return course;
        }
    }
}
