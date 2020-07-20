using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComparativeGraderAPI.Application.Courses;
using ComparativeGraderAPI.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComparativeGraderAPI.Controllers
{
    public class CoursesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}