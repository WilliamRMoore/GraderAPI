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

        public async Task<bool> EditCourse(int courseId, Command request)
        {
            var currentCourse = await _context.Courses.FindAsync(courseId);
            
            if(currentCourse == null)//Check to see if Course exists.
            {
                throw new RestException(HttpStatusCode.NotFound, new { activity = "NOT FOUND" });
            }

            if(currentCourse.ProfessorUserId != _userAccessor.GetCurrentUserId())//Make sure the current user is authorized to edit this entry.
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }

            currentCourse.SemesterId = request.SemesterId > 0 && currentCourse.SemesterId != request.SemesterId ? request.SemesterId : currentCourse.SemesterId;
            currentCourse.CourseName = request.CourseName ?? currentCourse.CourseName;
            currentCourse.CourseDescription = request.CourseDescription ?? currentCourse.CourseDescription;
            currentCourse.Year = request.Year > 0 && currentCourse.Year != request.Year ? request.Year : currentCourse.Year;
            var success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            //TODO: Implement some kind of cascade delete to get rid of related assignments.

            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { activity = "NOT FOUND" });
            }

            if (course.ProfessorUserId != _userAccessor.GetCurrentUserId())//Make sure the current user is authorized to delete this entry.
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }

            _context.Remove(course);

            var success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<Course> CourseDetails(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { activity = "NOT FOUND" });
            }

            if (course.ProfessorUserId != _userAccessor.GetCurrentUserId())//Make sure the current user is authorized to see this entry.
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }

            return course;
        }
    }
}
