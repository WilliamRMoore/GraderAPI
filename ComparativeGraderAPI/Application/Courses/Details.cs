using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Courses
{
    public class Details
    {
        public class Query : IRequest<Course>
        {
            public int CourseId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Course>
        {
            private readonly ICourseAccess _courseAccess;

            public Handler(ICourseAccess courseAccess)
            {
                _courseAccess = courseAccess;
            }

            public Task<Course> Handle(Query request, CancellationToken cancellationToken)
            {
                var course = _courseAccess.CourseDetails(request.CourseId);

                return course;
            }
        }
    }
}
