using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Submissions
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int SubmissionId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ISubmissionsAccess _submissionsAccess;

            public Handler(ISubmissionsAccess submissionsAccess)
            {
                _submissionsAccess = submissionsAccess;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _submissionsAccess.DeleteSubmission(request.SubmissionId);

                return Unit.Value;
            }
        }
    }
}
