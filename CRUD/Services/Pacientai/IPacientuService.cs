using CRUD.Models;

namespace CRUD.Services.Pacientai
{
    public interface IPacientuService
    {
        IEnumerable<Pacientas> RastiVisus();
        ICollection<Darbuotojas> RastiDaktarus();
        void Sukurti(Pacientas pacientai, int[] Daktarai);
    }
}
