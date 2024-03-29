﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Dtos.Extensions;
using Tacx.Activities.Core.Queries;

namespace Tacx.Activities.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
            try
            {
                var activity = await JsonSerializer.DeserializeAsync<ActivityDto>(
                    file.OpenReadStream(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (activity == null || activity.IsInvalid())
                {
                    return BadRequest("Uploaded Activity is not valid");
                }

                var response = await _mediator.Send(new CreateActivityCommand(activity!));

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                // Exceptions messages should not be used like this in production environments
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var activity = await _mediator.Send(new GetActivityQuery(id));
            return activity != null
                ? Ok(activity)
                : BadRequest("Activity could not be found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _mediator.Send(new DeleteActivityCommand(id));
            return isDeleted
                ? Ok()
                : BadRequest("Activity not deleted. Something went wrong");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ActivityDto activity)
        {
            var isUpdated = await _mediator.Send(new UpdateActivityCommand(activity));

            return isUpdated
                ? Ok()
                : BadRequest("update failed");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id,
            [FromBody] JsonPatchDocument<ActivityDto> patchDoc)
        {

            var activity = await _mediator.Send(new GetActivityQuery(id));
            if (activity == null)
            {
                return BadRequest("Activity not found");
            }

            patchDoc.ApplyTo(activity, ModelState);
            
            var isPatched = await _mediator.Send(new UpdateActivityCommand(activity));

            return isPatched
                ? Ok()
                : BadRequest("patch failed");
        }
    }
}