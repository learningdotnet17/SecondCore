using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecondCore.Models;
using Microsoft.Extensions.Logging;

namespace SecondCore.Controllers.Api
{
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private ICoreRepository _repository;

        public StopsController(ICoreRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                return Ok(trip.Stops.OrderBy(s => s.Order).ToList());

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get stops", ex);
                throw;
            }
            return BadRequest("Unable to return stops");
        }
    }
}
