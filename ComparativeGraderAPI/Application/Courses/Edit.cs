using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Courses
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int CourseId { get; set; }
            public int SemesterId { get; set; }
            public int Year { get; set; }
            public string CourseName { get; set; }
            public string CourseDescription { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SemesterId).InclusiveBetween(1, 3);//extend a rule to check to see if the semester id is valid
                RuleFor(x => x.Year).NotEmpty();
                RuleFor(x => x.CourseName).NotEmpty();
                RuleFor(x => x.CourseDescription).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ICourseAccess _courseAccess;

            public Handler(ICourseAccess courseAccess)
            {
                _courseAccess = courseAccess;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _courseAccess.EditCourse(request.CourseId, request);

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes.");
            }
        }
    }
}
