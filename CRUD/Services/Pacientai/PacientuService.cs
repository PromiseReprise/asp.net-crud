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

        public ICollection<Darbuotojas> RastiDaktarus()
        {
            throw new NotImplementedException();
            /*            var visiDarbuotojai = _db.Darbuotojai.Include(m => m.Pareigos).ToList();
                        foreach (dar)
                        return visiDaktarai;*/
        }

        public void Sukurti(Pacientas pacientai, int[] Daktarai)
        {
            throw new NotImplementedException();
        }
    }
}
