using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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

            public Handler(IAssignmentAccess assignmentAccess)
            {
                _assignmentAccess = assignmentAccess;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _assignmentAccess.EditAssignment(request);

                return Unit.Value;
            }
        }
    }
}
