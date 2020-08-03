using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Submissions
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int SubmissionId { get; set; }
            //public string ProfessorUserId { get; set; }
            //public int AssignmentId { get; set; }

            //public int SubmittedArtifactId { get; set; }

            public string StudentName { get; set; }
            //public int Rank { get; set; } = 0;
            public double Grade { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StudentName).NotEmpty();
                //RuleFor(x => x.Rank).NotEmpty();
                RuleFor(x => x.Grade).NotEmpty();
            }
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
                var success = await _submissionsAccess.EditSubmission(request);

                return Unit.Value;
            }
        }
    }
}
