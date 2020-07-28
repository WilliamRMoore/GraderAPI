using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Assingments
{
    public class List
    {
        public class Query : IRequest<List<Assignment>> { }

        public class Handler : IRequestHandler<Query, List<Assignment>>
        {
            private readonly IAssignmentAccess _assignmentAccess;

            public Handler(IAssignmentAccess assignmentAccess)
            {
                _assignmentAccess = assignmentAccess;
            }

            public async Task<List<Assignment>> Handle(Query request, CancellationToken cancellationToken)
            {
                //TODO: Impliment pageination

                var assignments = await _assignmentAccess.ListAssignments();

                return assignments.ToList();
            }
        }
    }
}
