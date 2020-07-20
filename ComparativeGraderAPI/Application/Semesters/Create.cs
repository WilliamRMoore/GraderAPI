using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Semesters
{
    public class Create
    {
        public class Command : IRequest
        {
            //public int ProfessorId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //TODO: Add validator for ProfID when Identity is implimented.
                RuleFor(x => x.StartDate).NotEmpty();
                RuleFor(x => x.EndDate).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IGradingDataAccess _dataAccess;

            public Handler(IGradingDataAccess dataAccess)
            {
                _dataAccess = dataAccess;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var semester = new Semester
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };

                var success = await _dataAccess.AddSemester(semester);

                return Unit.Value;
            }
        }
    }
}
