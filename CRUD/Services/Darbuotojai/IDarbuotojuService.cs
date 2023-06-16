using CRUD.Models;

namespace CRUD.Services.Darbuotojai
{
    public interface IDarbuotojuService
    {
        // Duomenų valdymas
        IEnumerable<Darbuotojas> RastiVisus();
        Darbuotojas RastiPagalId(int id);
        ICollection<Pareiga> RastiPareigas();
        void Sukurti(Darbuotojas darbuotojas, int[] Pareigos);
        void Redaguoti(Darbuotojas darbuotojas, int[] Pareigos);
        void Panaikinti(int id);

        // Rūšiavimas
        IEnumerable<Darbuotojas> Rusiuoti(IEnumerable<Darbuotojas> darbuotojai, string rusiavimoTipas);

        // Filtravimas
        IEnumerable<Darbuotojas> Filtruoti(IEnumerable<Darbuotojas> darbuotojai, string paieskosKategorija, string paieskosUzklausa, bool? aktyvus, bool? neaktyvus);
    }
}
