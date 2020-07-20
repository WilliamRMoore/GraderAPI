using ComparativeGraderAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.ServiceLayers.Interfaces
{
    public interface ICourseAccess
    {
        Task<bool> AddCourse(Course course);
        Task<IEnumerable<Course>> ListCourses();
    }
}
