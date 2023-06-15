using CRUD.Data;
using CRUD.Models;
using CRUD.Services.Darbuotojai;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class DarbuotojaiController : Controller
    {
        private readonly IDarbuotojuService _servisas;

        public DarbuotojaiController(IDarbuotojuService servisas)
        {
            _servisas = servisas;
        }

        public IActionResult Pradzia(bool? aktyvus, bool? neaktyvus)
        {
            var duomenys = _servisas.RastiVisus(aktyvus, neaktyvus);
            return View(duomenys);
        }

        public IActionResult Sukurti()
        {
            // Naudojamas ViewBag, kad pasiekti visas Pareigas kurios yra duomenų lentelėje
            ViewBag.VisosPareigos = _servisas.RastiPareigas();
            return View();
        }

        [HttpPost]
        public IActionResult Sukurti(Darbuotojas obj, int[] Pareigos)
        {
            if (ModelState.IsValid)
            {
                _servisas.Sukurti(obj, Pareigos);
                return RedirectToAction("Pradzia");
            }
            ViewBag.VisosPareigos = _servisas.RastiPareigas();
            return View();
        }

        public IActionResult Redaguoti(int id)
        {
            var koreguojamasDarbuotojas = _servisas.RastiPagalId(id);
            ViewBag.VisosPareigos = _servisas.RastiPareigas();
            return View(koreguojamasDarbuotojas);
        }

        [HttpPost]
        public IActionResult Redaguoti(Darbuotojas obj, int[] Pareigos)
        {
            if (ModelState.IsValid)
            {
                _servisas.Redaguoti(obj, Pareigos);
                return RedirectToAction("Pradzia");
            }
            ViewBag.VisosPareigos = _servisas.RastiPareigas();
            return View();
        }

        public IActionResult Panaikinti(int id)
        {
            var koreguojamasDarbuotojas = _servisas.RastiPagalId(id);
            ViewBag.VisosPareigos = _servisas.RastiPareigas();
            return View(koreguojamasDarbuotojas);
        }

        [HttpPost]
        public IActionResult PanaikintiPOST(int id)
        {
            _servisas.Panaikinti(id);
            return RedirectToAction("Pradzia");
        }
    }
}
