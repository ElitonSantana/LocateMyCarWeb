using LocateMyCarWeb.Models;
using LocateMyCarWeb.Services;
using LocateMyCarWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LocateMyCarWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;
        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            var response = _vehicleService.GetTopVehiclesOlders();
            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
