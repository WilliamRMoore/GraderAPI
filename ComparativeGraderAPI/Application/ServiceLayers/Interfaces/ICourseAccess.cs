using ComparativeGraderAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ComparativeGraderAPI.Application.Courses.Edit;

namespace ComparativeGraderAPI.Application.ServiceLayers.Interfaces
{
    public interface ICourseAccess
    {
        Task<bool> AddCourse(Course course);
        Task<IEnumerable<Course>> ListCourses();
        Task<bool> EditCourse(int courseId, Command request);
        Task<bool> DeleteCourse(int id);
        Task<Course> CourseDetails(int id);
    }
}
