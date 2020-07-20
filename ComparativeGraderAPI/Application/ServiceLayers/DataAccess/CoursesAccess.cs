using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using ComparativeGraderAPI.Security.Security_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
