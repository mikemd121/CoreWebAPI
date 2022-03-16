using CoreWebAPI.Business;
using CoreWebAPI.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationController : ControllerBase
    {
        /// <summary>
        /// The population data service
        /// </summary>
        private readonly IPopulationDataService _populationDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationController"/> class.
        /// </summary>
        /// <param name="populationDataService">The population data service.</param>
        public PopulationController(IPopulationDataService populationDataService )
        {
            this._populationDataService = populationDataService;
        }

        /// <summary>
        /// Gets the current location by event hub asynchronous.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Response>), (int)HttpStatusCode.OK)]
        public ActionResult<Response> GetPopulationByState([FromQuery] int[] state)
        {
            if (state == null)
                return BadRequest(CoreWebAPIResource.MessagesStateId);

            var model = _populationDataService.GetCurrentLocationAsync(state);
            return Ok(model);
        }
    }
}
