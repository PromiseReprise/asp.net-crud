using CRUD.Data;
using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Services.Darbuotojai
{
    public class DarboutojuService : IDarbuotojuService
    {
        private readonly DuomenuKontekstas _db;
        public DarboutojuService(DuomenuKontekstas db)
        {
            _db = db;
        }

        Darbuotojas IDarbuotojuService.RastiPagalId(int id)
        {
            // Rasti Darbuotoja ir jo Pareigas
            var rastasDarbuotojas = _db.Darbuotojai
                .Include(x => x.Pareigos)
                .FirstOrDefault(i => i.Id == id);

            return rastasDarbuotojas;
        }

        public IEnumerable<Darbuotojas> RastiVisus(bool? aktyvus, bool? neaktyvus)
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
            return darbuotojai;
        }

        void IDarbuotojuService.Sukurti(Darbuotojas darbuotojas, int[] Pareigos)
        {
            if (Pareigos != null)
            {
                // Priskirti pasirinktas Pareigas Darbuotojui
                foreach (var pareigosId in Pareigos)
                {
                    var pareiga = _db.Pareigos.Find(pareigosId);
                    if (pareiga != null)
                    {
                        darbuotojas.Pareigos.Add(pareiga);
                    }
                }
            }
            darbuotojas.Statusas = 1;
            _db.Darbuotojai.Add(darbuotojas);
            _db.SaveChanges();
        }

        void IDarbuotojuService.Redaguoti(Darbuotojas darbuotojas, int[] Pareigos)
        {
            // Rasti Darbuotoja ir jo Pareigas
            var koreguojamasDarbuotojas = _db.Darbuotojai
                    .Include(x => x.Pareigos)
                    .FirstOrDefault(i => i.Id == darbuotojas.Id);
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
            koreguojamasDarbuotojas.Vardas = darbuotojas.Vardas;
            koreguojamasDarbuotojas.Pavarde = darbuotojas.Pavarde;
            koreguojamasDarbuotojas.GimimoData = darbuotojas.GimimoData;
            koreguojamasDarbuotojas.Adresas = darbuotojas.Adresas;
            koreguojamasDarbuotojas.Statusas = 1;

            // Metodas Update atvaizduoja klaidą, kai yra perduodamas obj kintamasis
            _db.Darbuotojai.Update(koreguojamasDarbuotojas);
            _db.SaveChanges();
        }

        void IDarbuotojuService.Panaikinti(int id)
        {
            var naikinamasDarbuotojas = _db.Darbuotojai.FirstOrDefault(x => x.Id == id);
            naikinamasDarbuotojas.Statusas = 0;
            _db.SaveChanges();
        }

        public ICollection<Pareiga> RastiPareigas()
        {
            var visosPareigos = _db.Pareigos.ToList();
            return visosPareigos;
        }
    }
}
