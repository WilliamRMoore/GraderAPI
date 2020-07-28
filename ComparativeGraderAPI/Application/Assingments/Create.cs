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
    public class Create
    {
        public class Command : IRequest
        {
            public int CourseId { get; set; }
            public string AssignmentName { get; set; }
            public string AssignmentDescription { get; set; }
            public DateTime DueDate { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CourseId).NotEmpty();//extend validator to verify the courseId
                RuleFor(x => x.AssignmentName).NotEmpty();
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
                var assingment = new Assignment
                {
                    CourseId = request.CourseId,
                    AssignmentName = request.AssignmentName,
                    AssignmentDescription = request.AssignmentDescription,
                    DueDate = request.DueDate,
                };

                var success = await _assignmentAccess.AddAssignment(assingment);

                return Unit.Value;
            }
        }
    }
}
