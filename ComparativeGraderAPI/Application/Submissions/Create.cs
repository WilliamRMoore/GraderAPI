using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Submissions
{
    public class Create
    {
        public class Command: IRequest
        {
            public int AssignmentId { get; set; }

            //public int SubmittedArtifactId { get; set; }

            public string StudentName { get; set; }
            //public int Rank { get; set; }
            //public double Grade { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StudentName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ISubmissionsAccess _submissionsAccess;
            private readonly IAssignmentAccess _assignmentAccess;
            private readonly IDomainVerifier _domainVerifier;

            public Handler(ISubmissionsAccess submissionsAccess, IAssignmentAccess assignmentAccess, IDomainVerifier domainVerifier)
            {
                _submissionsAccess = submissionsAccess;
                _assignmentAccess = assignmentAccess;
                _domainVerifier = domainVerifier;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var assignmentToAddSubmissionTo = await _assignmentAccess.AssignmentDetails(request.AssignmentId);

                _domainVerifier.Verify<Assignment>(assignmentToAddSubmissionTo);

                var submission = new Submission
                {
                    AssignmentId = request.AssignmentId,
                    StudentName = request.StudentName
                };

                var result = await _submissionsAccess.AddSubmission(submission);

                return Unit.Value;
            }
        }
    }
}
