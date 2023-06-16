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

        public IEnumerable<Darbuotojas> RastiVisus()
        {
            // Pasirinkti visus Darbuotojus
            var darbuotojai = _db.Darbuotojai.Include(s => s.Pareigos).ToList();
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

        public IEnumerable<Darbuotojas> Rusiuoti(IEnumerable<Darbuotojas> darbuotojai, string rusiavimoTipas)
        {
            switch (rusiavimoTipas)
            {
                case "vardas_asc":
                    darbuotojai = darbuotojai.OrderBy(x => x.Vardas).ToList();
                    break;
                case "vardas_desc":
                    darbuotojai = darbuotojai.OrderByDescending(x => x.Vardas).ToList();
                    break;

                case "pavarde_asc":
                    darbuotojai = darbuotojai.OrderBy(x => x.Pavarde).ToList();
                    break;
                case "pavarde_desc":
                    darbuotojai = darbuotojai.OrderByDescending(x => x.Pavarde).ToList();
                    break;

                case "data_asc":
                    darbuotojai = darbuotojai.OrderBy(x => x.GimimoData).ToList();
                    break;
                case "data_desc":
                    darbuotojai = darbuotojai.OrderByDescending(x => x.GimimoData).ToList();
                    break;

                case "adresas_asc":
                    darbuotojai = darbuotojai.OrderBy(x => x.Adresas).ToList();
                    break;
                case "adresas_desc":
                    darbuotojai = darbuotojai.OrderByDescending(x => x.Adresas).ToList();
                    break;

                case "statusas_akt":
                    darbuotojai = darbuotojai.OrderByDescending(x => x.Statusas).ToList();
                    break;
                case "statusas_neakt":
                    darbuotojai = darbuotojai.OrderBy(x => x.Statusas).ToList();
                    break;
            }
            return darbuotojai;
        }

        public IEnumerable<Darbuotojas> Filtruoti(IEnumerable<Darbuotojas> darbuotojai, string paieskosKategorija, string paieskosUzklausa, bool? aktyvus, bool? neaktyvus)
        {
            // Patikrinti kurie žymimi langeliai turi vertę ir kokią, pasirenkamas tinkamas Darbuotojas
            if (aktyvus.HasValue && !aktyvus.Value)
            {
                darbuotojai = darbuotojai.Where(m => m.Statusas != 1).ToList();
            }

            if (neaktyvus.HasValue && !neaktyvus.Value)
            {
                darbuotojai = darbuotojai.Where(m => m.Statusas != 0).ToList();
            }

            if (paieskosUzklausa != null)
            {
                switch (paieskosKategorija)
                {
                    case "Vardas":
                        darbuotojai = darbuotojai.Where(m => m.Vardas == paieskosUzklausa).ToList();
                        break;
                    case "Pavardė":
                        darbuotojai = darbuotojai.Where(m => m.Pavarde == paieskosUzklausa).ToList();
                        break;
                    case "Gimimo Data":
                        if (DateTime.TryParse(paieskosUzklausa, out DateTime paieskosUzklausaDateTime))
                        {
                            darbuotojai = darbuotojai.Where(m => m.GimimoData.Year == paieskosUzklausaDateTime.Year &&
                                m.GimimoData.Month == paieskosUzklausaDateTime.Month &&
                                m.GimimoData.Day == paieskosUzklausaDateTime.Day).ToList();
                        }
                        else if (int.TryParse(paieskosUzklausa, out int paieskosUzklausaInt))
                        {
                            darbuotojai = darbuotojai.Where(m => m.GimimoData.Year == paieskosUzklausaInt).ToList();
                        }
                        break;
                    case "Adresas":
                        darbuotojai = darbuotojai.Where(m => m.Adresas.IndexOf(paieskosUzklausa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                        break;
                    case "Pareigos":
                        darbuotojai = darbuotojai.Where(m => m.Pareigos.Any(pareiga => pareiga.Pareigos.IndexOf(paieskosUzklausa, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
                        break;
                }
            }
            return darbuotojai;
        }
    }
}
