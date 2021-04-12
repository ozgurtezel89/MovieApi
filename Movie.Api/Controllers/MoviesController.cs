using Microsoft.AspNetCore.Mvc;
using Movie.Api.BusinessLogic;
using System;

namespace Movie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IStatusService _statusService;

        public MoviesController(IStatusService statusService)
        {
            _statusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
        }

        [HttpGet]
        [Route("/stats")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public IActionResult Get()
        {
            var results = _statusService.GetViewingStatistics();

            if (results is null)
            {
                return NoContent();
            }

            var jsonResults = Newtonsoft.Json.JsonConvert.SerializeObject(results);

            return Ok(jsonResults);
        }
    }
}
