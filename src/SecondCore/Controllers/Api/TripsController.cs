using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCore.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private ICoreRepository _repository;

        public TripsController(ICoreRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var trips = _repository.GetAllTrips();
                var tripViewModels = Mapper.Map<IEnumerable<TripViewModel>>(trips);
                return Ok(tripViewModels);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occurred");
                throw;
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {

                var newTrip = Mapper.Map<Trip>(trip);

                _repository.AddTrip(newTrip);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Failed to save changes to database");
                }
            }
            return BadRequest("Failed to save the trip");
        }
    }
}
