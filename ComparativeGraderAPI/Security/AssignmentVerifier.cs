using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Security
{
    public class AssignmentVerifier : IAssignmentVerifier
    {
        private readonly IUserAccessor _userAccessor;

        public AssignmentVerifier(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
        public void Verify(Assignment assignment)
        {
            if (assignment == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { activity = "NOT FOUND" });
            }

            if (assignment.ProfessorUserId != _userAccessor.GetCurrentUserId())//Make sure the current user is authorized to delete this entry.
            {
                throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            }
        }
    }
}
