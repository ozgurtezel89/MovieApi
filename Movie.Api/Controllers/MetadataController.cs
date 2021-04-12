using Microsoft.AspNetCore.Mvc;
using Movie.Api.BusinessLogic;
using Movie.Api.Models;
using System;
using System.Linq;

namespace Movie.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IMetadataService _metadataService;

        public MetaDataController(IMetadataService metadataService)
        {
            _metadataService = metadataService ?? throw new ArgumentNullException(nameof(metadataService));
        }


        [HttpGet]
        [Route("/{movieId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult Get([FromRoute]int movieId)
        {
            var results = _metadataService.GetMetadatas(movieId);

            if (results.Count() <= 0)
            {
                return NotFound();
            }

            var jsonResults = Newtonsoft.Json.JsonConvert.SerializeObject(results);

            return Ok(jsonResults);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]MetadataViewModel model)
        {
            _metadataService.SaveMetadata(model);

            return Created("/", model);
        }
    }
}
