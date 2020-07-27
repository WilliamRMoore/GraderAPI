using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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

            public Handler(ICourseAccess courseAccess)
            {
                _courseAccess = courseAccess;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _courseAccess.DeleteCourse(request.CourseId);

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes.");
            }
        }
    }
}
