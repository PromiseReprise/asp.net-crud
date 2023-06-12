using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Darbuotojas
    {
        public int Id { get; set; }
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GimimoData { get; set; }
        public string Adresas { get; set; }
        public ICollection<Pareiga> Pareigos { get; set;  }
        public int Statusas { get; set; }
    }
}
