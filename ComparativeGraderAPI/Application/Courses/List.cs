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
    public class List
    {
        public class Query: IRequest<List<Course>> { }

        public class Handler : IRequestHandler<Query, List<Course>>
        {
            private readonly ICourseAccess _courseAccess;

            public Handler(ICourseAccess courseAccess)
            {
                _courseAccess = courseAccess;
            }

            public async Task<List<Course>> Handle(Query request, CancellationToken cancellationToken)
            {
                var courses = await _courseAccess.ListCourses();

                return courses.ToList();
            }
        }
    }
}
