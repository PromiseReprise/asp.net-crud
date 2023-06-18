using CRUD.Data;
using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Services.Pacientai
{
    public class PacientuService : IPacientuService
    {
        private readonly DuomenuKontekstas _db;
        public PacientuService(DuomenuKontekstas db)
        {
            _db = db;
        }

        public IEnumerable<Pacientas> RastiVisus()
        {
            var pacientai = _db.Pacientai.Include(s => s.Darbuotojai).ToList();
            return pacientai;
        }

        public Pacientas RastiPagalId(int id)
        {
            var rastasPacientas = _db.Pacientai
                .Include(x => x.Darbuotojai)
                .FirstOrDefault(i => i.Id == id);

            return rastasPacientas;
        }

        public ICollection<Darbuotojas> RastiGydytojus()
        {
            var visiGydytojai = _db.Darbuotojai.Where(m => m.Pareigos.Any(pareiga => pareiga.Pareigos.Contains("Daktaras"))).ToList();
            return visiGydytojai;
        }

        public void Sukurti(Pacientas pacientas, int[] Gydytojai)
        {
            if (Gydytojai != null && Gydytojai.Any())
            {
                if (pacientas.Darbuotojai == null)
                {
                    pacientas.Darbuotojai = new List<Darbuotojas>();
                } 

                foreach (var gydytojoId in Gydytojai)
                {
                    var gydytojas = _db.Darbuotojai.Find(gydytojoId);
                    if (gydytojas != null)
                    {
                        pacientas.Darbuotojai.Add(gydytojas);
                    }
                }
            }
            _db.Pacientai.Add(pacientas);
            _db.SaveChanges();
        }

        public void Redaguoti(Pacientas pacientas, int[] Gydytojai)
        {
            // Rasti Darbuotoja ir jo Pareigas
            var koreguojamasPacientas = _db.Pacientai
                    .Include(x => x.Darbuotojai)
                    .FirstOrDefault(i => i.Id == pacientas.Id);
            // Pašalinti visas Darbuotojo Pareigas ir priskirti naujas Pareigas
            koreguojamasPacientas.Darbuotojai.Clear();
            if (Gydytojai != null)
            {
                foreach (var gydytojoId in Gydytojai)
                {
                    var gydytojas = _db.Darbuotojai.Find(gydytojoId);
                    koreguojamasPacientas.Darbuotojai.Add(gydytojas);
                }
            }
            koreguojamasPacientas.Vardas = pacientas.Vardas;
            koreguojamasPacientas.Pavarde = pacientas.Pavarde;
            koreguojamasPacientas.GimimoData = pacientas.GimimoData;

            _db.Pacientai.Update(koreguojamasPacientas);
            _db.SaveChanges();
        }

        public IEnumerable<Pacientas> Rusiuoti(IEnumerable<Pacientas> pacientai, string rusiavimoTipas)
        {
            switch (rusiavimoTipas)
            {
                case "vardas_asc":
                    pacientai = pacientai.OrderBy(x => x.Vardas).ToList();
                    break;
                case "vardas_desc":
                    pacientai = pacientai.OrderByDescending(x => x.Vardas).ToList();
                    break;

                case "pavarde_asc":
                    pacientai = pacientai.OrderBy(x => x.Pavarde).ToList();
                    break;
                case "pavarde_desc":
                    pacientai = pacientai.OrderByDescending(x => x.Pavarde).ToList();
                    break;

                case "data_asc":
                    pacientai = pacientai.OrderBy(x => x.GimimoData).ToList();
                    break;
                case "data_desc":
                    pacientai = pacientai.OrderByDescending(x => x.GimimoData).ToList();
                    break;
            }
            return pacientai;
        }
    }
}
