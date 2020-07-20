using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Semesters
{
    public class Details
    {
        public class Query : IRequest<Semester>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Semester>
        {
            private readonly GradingDataContext _context;

            public Handler(GradingDataContext context)
            {
                _context = context;
            }

            public async Task<Semester> Handle(Query request, CancellationToken cancellationToken)
            {
                var semester = await _context.Semesters.FindAsync(request.Id);

                if(semester == null)
                {
                    throw new Exception("semester not found");//change to RestException later
                }

                return semester;
            }
        }
    }
}
