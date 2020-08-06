using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Security.Security_Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Courses
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int CourseId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ICourseAccess _courseAccess;
            private readonly ICourseVerifier _courseVerifier;

            public Handler(ICourseAccess courseAccess, ICourseVerifier courseVerifier)
            {
                _courseAccess = courseAccess;
                _courseVerifier = courseVerifier;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var courseToDelete = await _courseAccess.CourseDetails(request.CourseId);

                _courseVerifier.Verify(courseToDelete);

                var success = await _courseAccess.DeleteCourse(request.CourseId);

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes.");
            }
        }
    }
}
