using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Application.Validators;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using ComparativeGraderAPI.Security.Security_Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.User
{
    public class Register
    {
        public class Command: IRequest<User>
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly GradingDataContext _context;
            private readonly UserManager<ProfessorUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(GradingDataContext context, UserManager<ProfessorUser> userManager, IJwtGenerator jwtGenerator)
            {
                this._context = context;
                this._userManager = userManager;
                this._jwtGenerator = jwtGenerator;
            }
            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.AnyAsync(x => x.Email == request.Email))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists." });
                }

                if (await _context.Users.AnyAsync(x => x.UserName == request.Username))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists." });
                }

                var user = new ProfessorUser
                {
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new User
                    {
                        Token = _jwtGenerator.CreateToken(user),
                        Username = user.UserName,
                    };
                }

                throw new Exception("Problem creating user.");
            }
        }
    }
}
