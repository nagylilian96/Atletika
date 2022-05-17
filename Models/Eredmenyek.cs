using System.ComponentModel.DataAnnotations.Schema;

namespace Atletika.Models
{
    [Table("Eredmenyek")]
    public class Eredmenyek
    {
        [ForeignKey("Helyszin")]
        public int HelyId { get; set; }
        public virtual Helyszin Helyszin { get; set; }

        [ForeignKey("Versenyzo")]
        public int VersId { get; set; }
        public virtual Versenyzo Versenyzo { get; set; }
        
        public string Vsenyszam { get; set; }
        public int? Helyezes { get; set; }
    }
}
