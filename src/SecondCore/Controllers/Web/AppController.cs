using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecondCore.Models;
using Microsoft.Extensions.Logging;

namespace SecondCore.Controllers.Web
{
    public class AppController : Controller
    {
        private IConfigurationRoot _config;
        private ICoreRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IConfigurationRoot config, ICoreRepository repository, ILogger<AppController> logger)
        {
            _config = config;
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                var data = _repository.GetAllTrips();
                return View(data);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
                return Redirect("/error");
                throw;
            }
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
