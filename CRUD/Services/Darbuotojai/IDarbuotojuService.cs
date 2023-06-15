using CRUD.Models;

namespace CRUD.Services.Darbuotojai
{
    public interface IDarbuotojuService
    {
        IEnumerable<Darbuotojas> RastiVisus(bool? aktyvus, bool? neaktyvus);
        Darbuotojas RastiPagalId(int id);
        ICollection<Pareiga> RastiPareigas();
        void Sukurti(Darbuotojas darbuotojas, int[] Pareigos);
        void Redaguoti(Darbuotojas darbuotojas, int[] Pareigos);
        void Panaikinti(int id);
    }
}
