using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Security
{
    public class CourseVerifier : ICourseVerifier
    {
        private readonly IUserAccessor _userAccessor;

        public CourseVerifier(IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;
        }

        public void Verify(Course course)
        {
            if(course == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { activity = "NOT FOUND" });
            }

            if(course.ProfessorUserId != _userAccessor.GetCurrentUserId())
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }
        }
    }
}
