using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Dtos.Extensions;

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

                if (activity == null || activity.IsEmpty())
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
        public async Task<IActionResult> GetActivityById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }
    }
}