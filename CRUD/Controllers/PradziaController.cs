using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRUD.Controllers
{
    public class PradziaController : Controller
    {
        private readonly ILogger<PradziaController> _logger;
        public PradziaController(ILogger<PradziaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Pradzia()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Klaida()
        {
            return View(new KlaiduModelis { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}