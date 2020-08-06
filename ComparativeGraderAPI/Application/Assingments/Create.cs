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
using System.Security.Cryptography.X509Certificates;
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
                RuleFor(x => x.CourseId).NotEmpty();
                RuleFor(x => x.AssignmentName).NotEmpty();   
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IAssignmentAccess _assignmentAccess;
            private readonly ICourseVerifier _courseVerifier;
            private readonly ICourseAccess _courseAccess;

            public Handler(IAssignmentAccess assignmentAccess, ICourseVerifier courseVerifier, ICourseAccess courseAccess)
            {
                _assignmentAccess = assignmentAccess;
                _courseVerifier = courseVerifier;
                _courseAccess = courseAccess;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var courseToAddAssignmentTo = await _courseAccess.CourseDetails(request.CourseId);

                _courseVerifier.Verify(courseToAddAssignmentTo);
                
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
