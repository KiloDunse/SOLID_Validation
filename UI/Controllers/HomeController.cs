using Core;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IValidationAggregator<Person> _validator;

        public HomeController(ILogger<HomeController> logger, IValidationAggregator<Person> validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public IActionResult Index()
        {
            var p = new Person
            {
                Age = -1,
                FirstName = "Test",
                LastName = "Tester"
            };

            var res = _validator.Validate(p);

            var valid = res.IsValid;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}