using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
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
            private readonly IDomainVerifier _domainVerifier;

            public Handler(ISubmissionsAccess submissionsAccess, IDomainVerifier domainVerifier)
            {
                _submissionsAccess = submissionsAccess;
                _domainVerifier = domainVerifier;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var submissionToDelete = await _submissionsAccess.SubmissionDetails(request.SubmissionId);

                _domainVerifier.Verify<Submission>(submissionToDelete);

                await _submissionsAccess.DeleteSubmission(submissionToDelete);

                return Unit.Value;
            }
        }
    }
}
