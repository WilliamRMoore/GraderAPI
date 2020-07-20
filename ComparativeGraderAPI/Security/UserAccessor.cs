using ComparativeGraderAPI.Security.Security_Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()//PUT THE BEARER TOKEN IN POSTMAN YOU DUMB DUMB
        {
            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim =>
            claim.Type == ClaimTypes.NameIdentifier)?.Value;


            return userId;
        }

        public string GetCurrentUsername()
        {
            var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim =>
            claim.Type == ClaimTypes.Name)?.Value;

            return username;
        }
    }
}
