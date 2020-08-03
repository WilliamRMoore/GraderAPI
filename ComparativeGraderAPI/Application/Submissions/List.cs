using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Submissions
{
    public class List
    {
        public class Query : IRequest<List<Submission>> { }

        public class Handler : IRequestHandler<Query, List<Submission>>
        {
            private readonly ISubmissionsAccess _submissionsAccess;

            public Handler(ISubmissionsAccess submissionsAccess)
            {
                _submissionsAccess = submissionsAccess;
            }

            public async Task<List<Submission>> Handle(Query request, CancellationToken cancellationToken)
            {
                var submissions = await _submissionsAccess.ListSubmissions();

                return submissions.ToList();
            }
        }
    }
}
