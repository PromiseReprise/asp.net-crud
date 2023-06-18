using CRUD.Models;
using CRUD.Services.Darbuotojai;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD.Controllers
{
    public class DarbuotojaiController : Controller
    {
        private readonly IDarbuotojuService _servisas;

        public DarbuotojaiController(IDarbuotojuService servisas)
        {
            _servisas = servisas;
        }

        public IActionResult Pradzia(bool? aktyvus, bool? neaktyvus, string rusiavimoTipas, string paieskosKategorija, string paieskosUzklausa)
        {
            var paieskosPavadinimai = new List<string>
            {
                "Vardas",
                "Pavardė",
                "Gimimo Data",
                "Adresas",
                "Pareigos"
            };
            ViewBag.PaieskosPavadinimai = new SelectList(paieskosPavadinimai);
            ViewBag.VardoRusiavimoParam = rusiavimoTipas == "vardas_asc" ? "vardas_desc" : "vardas_asc";
            ViewBag.PavardesRusiavimoParam = rusiavimoTipas == "pavarde_asc" ? "pavarde_desc" : "pavarde_asc";
            ViewBag.DatosRusiavimoParam = rusiavimoTipas == "data_asc" ? "data_desc" : "data_asc";
            ViewBag.AdresoRusiavimoParam = rusiavimoTipas == "adresas_asc" ? "adresas_desc" : "adresas_asc";
            ViewBag.StatusoRusiavimoParam = rusiavimoTipas == "statusas_akt" ? "statusas_neakt" : "statusas_akt";

            var duomenys = _servisas.RastiVisus();

            if (rusiavimoTipas != null)
            {
                duomenys = _servisas.Rusiuoti(duomenys, rusiavimoTipas);
            }

            if (paieskosUzklausa != null || aktyvus.HasValue || neaktyvus.HasValue)
            {
                duomenys = _servisas.Filtruoti(duomenys, paieskosKategorija, paieskosUzklausa, aktyvus, neaktyvus);
            }

            return View(duomenys);
        }

        public IActionResult Sukurti()
        {
            // Naudojamas ViewBag, kad pasiekti visas Pareigas kurios yra duomenų lentelėje
            ViewBag.VisosPareigos = _servisas.RastiPareigas();
            // PARTIAL VIEW?
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
        public IActionResult PanaikinkDarbuotoja(int id)
        {
            _servisas.Panaikinti(id);
            return RedirectToAction("Pradzia");
        }
    }
}
