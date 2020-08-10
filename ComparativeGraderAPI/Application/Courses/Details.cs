using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
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
            private readonly ICourseVerifier _courseVerifier;

            public Handler(ICourseAccess courseAccess, ICourseVerifier courseVerifier)
            {
                _courseAccess = courseAccess;
                _courseVerifier = courseVerifier;
            }

            public async Task<Course> Handle(Query request, CancellationToken cancellationToken)
            {
                var course = await _courseAccess.CourseDetails(request.CourseId);

                _courseVerifier.Verify(course);

                return course;
            }
        }
    }
}
