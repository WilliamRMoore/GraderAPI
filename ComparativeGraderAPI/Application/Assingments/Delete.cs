using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
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

            public Handler(IAssignmentAccess assignmentAccess)
            {
                _assignmentAccess = assignmentAccess;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _assignmentAccess.DeleteAssignment(request.Id);

                return Unit.Value;
            }
        }
    }
}
