using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecondCore.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using SecondCore.ViewModels;

namespace SecondCore.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private ICoreRepository _repository;

        public StopsController(ICoreRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get stops", ex);
                throw;
            }
            return BadRequest("Unable to return stops");
        }
        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    _repository.AddStop(tripName, newStop);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                            Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception)
            {
                _logger.LogError("Failed to save new stop");
                throw;
            }
            return BadRequest("Failed to save new stop");
        }
    }
}
