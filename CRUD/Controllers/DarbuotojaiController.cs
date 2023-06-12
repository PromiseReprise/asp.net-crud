using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class DarbuotojaiController : Controller
    {
        private readonly DuomenuKontekstas _db;
        public DarbuotojaiController(DuomenuKontekstas db)
        {
            _db = db;
        }

        public IActionResult Pradzia(bool? aktyvus, bool? neaktyvus)
        {
            // Pasirinkti visus Darbuotojus
            var darbuotojai = _db.Darbuotojai.Include(s => s.Pareigos).ToList();
            // Patikrinti kurie žymimi langeliai turi vertę ir kokią, pasirenkamas tinkamas Darbuotojas
            if (aktyvus.HasValue && !aktyvus.Value)
            {
                darbuotojai = darbuotojai.Where(e => e.Statusas != 1).ToList();
            }
            if (neaktyvus.HasValue && !neaktyvus.Value)
            {
                darbuotojai = darbuotojai.Where(e => e.Statusas != 0).ToList();
            }
            return View(darbuotojai);
        }

        public IActionResult Sukurti()
        {
            // Naudojamas ViewBag, kad pasiekti visas Pareigas kurios yra duomenų lentelėje
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Sukurti(Darbuotojas obj, int[] Pareigos)
        {
            if (ModelState.IsValid)
            {
                if (Pareigos != null)
                {
                    // Priskirti pasirinktas Pareigas Darbuotojui obj
                    foreach (var pareigosId in Pareigos)
                    {
                        var pareiga = _db.Pareigos.Find(pareigosId);
                        if (pareiga != null)
                        {
                            obj.Pareigos.Add(pareiga);
                        }
                    }
                }
                obj.Statusas = 1;
                _db.Darbuotojai.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Pradzia");
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View();
        }

        public IActionResult Redaguoti(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Rasti Darbuotoja
            var koreguojamasDarbuotojas = _db.Darbuotojai
                .Include(x => x.Pareigos)
                .FirstOrDefault(i => i.Id == id);
            if (koreguojamasDarbuotojas == null)
            {
                return NotFound();
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View(koreguojamasDarbuotojas);
        }

        [HttpPost]
        public IActionResult Redaguoti(Darbuotojas obj, int[] Pareigos)
        {
            if (ModelState.IsValid)
            {
                // Rasti Darbuotoja ir jo Pareigas
                var koreguojamasDarbuotojas = _db.Darbuotojai
                        .Include(x => x.Pareigos)
                        .FirstOrDefault(i => i.Id == obj.Id);
                // Pašalinti visas Darbuotojo Pareigas ir priskirti naujas Pareigas
                koreguojamasDarbuotojas.Pareigos.Clear();
                if (Pareigos != null)
                {
                    foreach (var pareigosId in Pareigos)
                    {
                        var pareiga = _db.Pareigos.Find(pareigosId);
                        koreguojamasDarbuotojas.Pareigos.Add(pareiga);
                    }
                }
                // Šią dalį pakeisti
                koreguojamasDarbuotojas.Vardas = obj.Vardas;
                koreguojamasDarbuotojas.Pavarde = obj.Pavarde;
                koreguojamasDarbuotojas.GimimoData = obj.GimimoData;
                koreguojamasDarbuotojas.Adresas = obj.Adresas;
                koreguojamasDarbuotojas.Statusas = 1;

                // Metodas Update atvaizduoja klaidą, kai yra perduodamas obj kintamasis
                _db.Darbuotojai.Update(koreguojamasDarbuotojas);
                _db.SaveChanges();
                return RedirectToAction("Pradzia");
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View();
        }

        public IActionResult Panaikinti(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Rasti Darbuotoja ir jo Pareigas
            var koreguojamasDarbuotojas = _db.Darbuotojai
                .Include(x => x.Pareigos)
                .FirstOrDefault(i => i.Id == id);
            if (koreguojamasDarbuotojas == null)
            {
                return NotFound();
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View(koreguojamasDarbuotojas);
        }

        [HttpPost]
        public IActionResult PanaikintiPOST(int? id)
        {
            var obj = _db.Darbuotojai.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.Statusas = 0;
            _db.SaveChanges();
            return RedirectToAction("Pradzia");
        }
    }
}
