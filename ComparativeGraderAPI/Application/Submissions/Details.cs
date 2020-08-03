using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Submissions
{
    public class Details
    {
        public class Query : IRequest<Submission>
        {
            public int SubmissionId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Submission>
        {
            private readonly ISubmissionsAccess _submissionsAccess;

            public Handler(ISubmissionsAccess submissionsAccess)
            {
                _submissionsAccess = submissionsAccess;
            }

            public async Task<Submission> Handle(Query request, CancellationToken cancellationToken)
            {
                var submission = await _submissionsAccess.SubmissionDetails(request.SubmissionId);

                return submission;
            }
        }
    }
}
