using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataContext _db;

        public EmployeeController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index(bool? aktyvus, bool? neaktyvus)
        {
            // Select all Darbuotojai
            var employees = _db.Darbuotojai.Include(s => s.Pareigos).ToList();

            // Check whether checkboxes are null or have value, Select the right Darbuotojai
            if (aktyvus.HasValue && !aktyvus.Value)
            {
                employees = employees.Where(e => e.Statusas != 1).ToList();
            }

            if (neaktyvus.HasValue && !neaktyvus.Value)
            {
                employees = employees.Where(e => e.Statusas != 0).ToList();
            }

            return View(employees);
        }

        public IActionResult Create()
        {
            // ViewBag to access all Pareigos that are in Datatable
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Darbuotojas obj, int[] Pareigos)
        {
            if (ModelState.IsValid)
            {
                if (Pareigos != null)
                {
                    // Add selected Pareigos to obj
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
                return RedirectToAction("Index");
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Find Darbuotojas
            var darbuotojasToEdit = _db.Darbuotojai
                .Include(x => x.Pareigos)
                .FirstOrDefault(i => i.Id == id);

            if (darbuotojasToEdit == null)
            {
                return NotFound();
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View(darbuotojasToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Darbuotojas obj, int[] Pareigos)
        {
            if (ModelState.IsValid)
            {
                // find Darbuotojas and include Pareigos
                var darbuotojasToEdit = _db.Darbuotojai
                        .Include(x => x.Pareigos)
                        .FirstOrDefault(i => i.Id == obj.Id);

                // Clear all Pareigos from Darbuotojas to then add selected Pareigos
                darbuotojasToEdit.Pareigos.Clear();

                if (Pareigos != null)
                {
                    foreach (var pareigosId in Pareigos)
                    {
                        var pareiga = _db.Pareigos.Find(pareigosId);
                        darbuotojasToEdit.Pareigos.Add(pareiga);
                    }
                }

                // Need to change this part
                darbuotojasToEdit.Vardas = obj.Vardas;
                darbuotojasToEdit.Pavarde = obj.Pavarde;
                darbuotojasToEdit.GimimoData = obj.GimimoData;
                darbuotojasToEdit.Adresas = obj.Adresas;

                darbuotojasToEdit.Statusas = 1;

                // Update gives an error if trying to push obj
                _db.Darbuotojai.Update(darbuotojasToEdit);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Find Darbuotojas
            var darbuotojasToEdit = _db.Darbuotojai
                .Include(x => x.Pareigos)
                .FirstOrDefault(i => i.Id == id);

            if (darbuotojasToEdit == null)
            {
                return NotFound();
            }
            ViewBag.VisosPareigos = _db.Pareigos.ToList();
            return View(darbuotojasToEdit);
        }

        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Darbuotojai.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            obj.Statusas = 0;

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
