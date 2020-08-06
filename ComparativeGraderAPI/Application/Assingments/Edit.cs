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
            private readonly IUserAccessor _userAccessor;

            public Handler(IAssignmentAccess assignmentAccess, IUserAccessor userAccessor)
            {
                _assignmentAccess = assignmentAccess;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentAssignment = await _assignmentAccess.AssignmentDetails(request.Id);

                if (currentAssignment == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Assignment = "NOT FOUND" });
                }

                if (currentAssignment.ProfessorUserId != _userAccessor.GetCurrentUserId())
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new { Assignment = "UNAUTHORIZED" });
                }

                var success = await _assignmentAccess.EditAssignment(request, currentAssignment);

                return Unit.Value;
            }
        }
    }
}
