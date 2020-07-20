using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Security.Security_Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.User
{
    public class CurrentUser
    {
        public class Query: IRequest<User> { }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<ProfessorUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<ProfessorUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
            {
                this._userManager = userManager;
                this._jwtGenerator = jwtGenerator;
                this._userAccessor = userAccessor;
            }
            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());

                return new User
                {
                    Username = user.UserName,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }
        }
    }
}
