using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ComparativeGraderAPI.Security
{
    public class DomainVerifier : IDomainVerifier
    {
        private readonly IUserAccessor _userAccessor;

        public DomainVerifier(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public void Verify<T>(object ob)
        {
            if(ob == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { activity = "NOT FOUND" });
            }

            if(ob is T)
            {
                var typeName = typeof(T).Name;

                switch (typeName)
                {
                    case "Course":
                        VerifyClassParams((Course)ob);
                        return;
                    case "Assignment":
                        VerifyClassParams((Assignment)ob);
                        return;
                    case "Submission":
                        VerifyClassParams((Submission)ob);
                        return;
                    default:
                        throw new RestException(HttpStatusCode.InternalServerError, new { activity = "SYSTEM ERROR" });
                }
            }

            throw new RestException(HttpStatusCode.InternalServerError, new { activity = "SYSTEM ERROR" });
            
        }

        private void VerifyClassParams(Course course)
        {
            if (course.ProfessorUserId != _userAccessor.GetCurrentUserId())//Make sure the current user is authorized to delete this entry.
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }
        }

        private void VerifyClassParams(Assignment assignment)
        {
            if (assignment.ProfessorUserId != _userAccessor.GetCurrentUserId())//Make sure the current user is authorized to delete this entry.
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }
        }

        private void VerifyClassParams(Submission submission)
        {
            if (submission.ProfessorUserId != _userAccessor.GetCurrentUserId())//Make sure the current user is authorized to delete this entry.
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }
        }
    }
}
