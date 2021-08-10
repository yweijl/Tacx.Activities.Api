using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Infrastructure.AzureStorage.Interfaces;

namespace Tacx.Activities.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivitiesStorageController : ControllerBase
    {
        private readonly IBlobContainer _blobContainer;

        public ActivitiesStorageController(IBlobContainer blobContainer)
        {
            _blobContainer = blobContainer;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var activity = await _blobContainer.GetAsync<Activity>(id);
            return activity != null
                ? Ok(activity)
                : BadRequest("Activity could not be found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _blobContainer.DeleteAsync<Activity>(id);
            return isDeleted
                ? Ok()
                : BadRequest("Activity not deleted. Something went wrong");
        }
    }
}