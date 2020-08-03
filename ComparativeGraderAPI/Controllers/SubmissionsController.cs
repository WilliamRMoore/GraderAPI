using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComparativeGraderAPI.Application.Submissions;
using ComparativeGraderAPI.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComparativeGraderAPI.Controllers
{
    public class SubmissionsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<List<Submission>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Submission>> Details(int id)
        {
            return await Mediator.Send(new Details.Query { SubmissionId = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.Command command)
        {
            command.SubmissionId = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await Mediator.Send(new Delete.Command { SubmissionId = id });
        }
    }
}