using ComparativeGraderAPI.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Semesters
{
    public class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StartDate).NotEmpty();
                RuleFor(x => x.EndDate).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly GradingDataContext _context;

            public Handler(GradingDataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var semester = await _context.Semesters.FindAsync(request.Id);

                if(semester == null)
                {
                    throw new Exception("Not found");//change to RestException later.
                }

                semester.StartDate = request.StartDate;
                semester.EndDate = request.EndDate;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
