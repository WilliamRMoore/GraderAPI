using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Semesters
{
    public class List
    {
        public class Query : IRequest<List<Semester>> { }

        public class Handler : IRequestHandler<Query, List<Semester>>
        {
            private readonly GradingDataContext _context;

            public Handler(GradingDataContext context)
            {
                _context = context;
            }

            public async Task<List<Semester>> Handle(Query request, CancellationToken cancelationToken)
            {
                var semesters = await _context.Semesters.ToListAsync();

                return semesters;
            }
        }
    }
}
