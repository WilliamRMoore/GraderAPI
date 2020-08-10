using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Assingments
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string AssignmentName { get; set; }
            public string AssignmentDescription { get; set; }
            public DateTime? DueDate { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AssignmentDescription).NotEmpty();
                RuleFor(x => x.AssignmentName).NotEmpty();
                RuleFor(x => x.DueDate).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IAssignmentAccess _assignmentAccess;
            private readonly IDomainVerifier _domainVerifier;

            public Handler(IAssignmentAccess assignmentAccess, IDomainVerifier domainVerifier)
            {
                _assignmentAccess = assignmentAccess;
                _domainVerifier = domainVerifier;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentAssignment = await _assignmentAccess.AssignmentDetails(request.Id);

                _domainVerifier.Verify<Assignment>(currentAssignment);

                await _assignmentAccess.EditAssignment(request, currentAssignment);

                return Unit.Value;
            }
        }
    }
}
