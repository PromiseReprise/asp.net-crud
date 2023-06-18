using CRUD.Models;
using CRUD.Services.Darbuotojai;
using CRUD.Services.Pacientai;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD.Controllers
{
    public class PacientaiController : Controller
    {

        private readonly IPacientuService _servisas;
        public PacientaiController(IPacientuService servisas)
        {
            _servisas = servisas;
        }

        public IActionResult Pradzia(string rusiavimoTipas, string paieskosKategorija, string paieskosUzklausa)
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
            var duomenys = _servisas.RastiVisus();

            return View(duomenys);
        }

        public IActionResult Sukurti()
        {
            ViewBag.VisiDaktarai = _servisas.RastiDaktarus();
            return View();
        }

        [HttpPost]
        public IActionResult Sukurti(Pacientas obj, int[] Daktarai)
        {
            if (ModelState.IsValid)
            {
                _servisas.Sukurti(obj, Daktarai);
                return RedirectToAction("Pradzia");
            }
            ViewBag.VisosPareigos = _servisas.RastiDaktarus();
            return View();
        }
    }
}
