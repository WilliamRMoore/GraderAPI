using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Assingments
{
    public class Details
    {
        public class Query : IRequest<Assignment>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Assignment>
        {
            private readonly IAssignmentAccess _assignmentAccess;

            public Handler(IAssignmentAccess assignmentAccess)
            {
                _assignmentAccess = assignmentAccess;
            }

            public async Task<Assignment> Handle(Query request, CancellationToken cancellationToken)
            {
                var assignment = await _assignmentAccess.AssignmentDetails(request.Id);

                return assignment;
            }
        }
    }
}
