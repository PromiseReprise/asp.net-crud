namespace CRUD.Models
{
    public class Pareiga
    {
        public int Id { get; set; }
        public string Pareigos { get; set; }
        public ICollection<Darbuotojas> Darbuotojai { get; set; }
    }
}