using CRUD.Models;
using CRUD.Services.Pacientai;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class PacientaiController : Controller
    {

        private readonly IPacientuService _servisas;
        public PacientaiController(IPacientuService servisas)
        {
            _servisas = servisas;
        }

        public IActionResult Pradzia(string rusiavimoTipas)
        {
            ViewBag.VardoRusiavimoParam = rusiavimoTipas == "vardas_asc" ? "vardas_desc" : "vardas_asc";
            ViewBag.PavardesRusiavimoParam = rusiavimoTipas == "pavarde_asc" ? "pavarde_desc" : "pavarde_asc";
            ViewBag.DatosRusiavimoParam = rusiavimoTipas == "data_asc" ? "data_desc" : "data_asc";

            var duomenys = _servisas.RastiVisus();

            if (rusiavimoTipas != null)
            {
                duomenys = _servisas.Rusiuoti(duomenys, rusiavimoTipas);
            }
            
            return View(duomenys);
        }

        public IActionResult Sukurti()
        {
            ViewBag.Gydytojai = _servisas.RastiGydytojus();
            return View();
        }

        [HttpPost]
        public IActionResult Sukurti(Pacientas obj, int[] Daktarai)
        {
            if (ModelState.IsValid)
            {
                if (Daktarai == null || Daktarai.Length == 0)
                {
                }
                _servisas.Sukurti(obj, Daktarai);
                return RedirectToAction("Pradzia");
            }
            ViewBag.Gydytojai = _servisas.RastiGydytojus();
            return View();
        }
        public IActionResult Redaguoti(int id)
        {
            var koreguojamasDarbuotojas = _servisas.RastiPagalId(id);
            ViewBag.Gydytojai = _servisas.RastiGydytojus();
            return View(koreguojamasDarbuotojas);
        }

        [HttpPost]
        public IActionResult Redaguoti(Pacientas obj, int[] Pareigos)
        {
            if (ModelState.IsValid)
            {
                _servisas.Redaguoti(obj, Pareigos);
                return RedirectToAction("Pradzia");
            }
            ViewBag.Gydytojai = _servisas.RastiGydytojus();
            return View();
        }
    }
}
