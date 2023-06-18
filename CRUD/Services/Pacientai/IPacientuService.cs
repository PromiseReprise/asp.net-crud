using CRUD.Models;

namespace CRUD.Services.Pacientai
{
    public interface IPacientuService
    {
        IEnumerable<Pacientas> RastiVisus();
        Pacientas RastiPagalId(int id);
        ICollection<Darbuotojas> RastiGydytojus();
        void Sukurti(Pacientas pacientas, int[] Gydytojai);
        void Redaguoti(Pacientas pacientas, int[] Gydytojai);
        IEnumerable<Pacientas> Rusiuoti(IEnumerable<Pacientas> pacientai, string rusiavimoTipas);
    }
}
