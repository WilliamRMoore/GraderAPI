using ComparativeGraderAPI.Application.Semesters;
using ComparativeGraderAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ComparativeGraderAPI.Controllers
{
    public class SemestersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Semester>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        //[HttpPost]
        //public async Task<ActionResult<Unit>> Create(Create.Command command)
        //{
        //    return await Mediator.Send(command);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        //{
        //    command.Id = id;
        //    return await Mediator.Send(command);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Semester>> Details(int id)
        //{
        //    return await Mediator.Send(new Details.Query { Id = id });
        //}
    }
}
