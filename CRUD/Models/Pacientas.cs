using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Pacientas
    {
        public int Id { get; set; }
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GimimoData { get; set; }
        public ICollection<Darbuotojas>? Darbuotojai { get; set; }
    }
}
