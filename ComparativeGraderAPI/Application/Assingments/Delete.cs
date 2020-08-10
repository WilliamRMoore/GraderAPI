using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Security.Security_Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Assingments
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IAssignmentAccess _assignmentAccess;
            private readonly IDomainVerifier _domainVerifier;
            private readonly IAssignmentVerifier _assignmentVerifier;

            public Handler(IAssignmentAccess assignmentAccess, IDomainVerifier domainVerifier)
            {
                _assignmentAccess = assignmentAccess;
                _domainVerifier = domainVerifier;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var assignmentToDelete = await _assignmentAccess.AssignmentDetails(request.Id);

                _assignmentVerifier.Verify(assignmentToDelete);

                await _assignmentAccess.DeleteAssignment(request.Id);

                return Unit.Value;
            }
        }
    }
}
