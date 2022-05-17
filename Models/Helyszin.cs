using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Atletika.Models
{
    [Table("Helyszin")]
    public class Helyszin
    {
        [Key]
        public int HelyId { get; set; }
        public int Ev { get; set; }
        public string Orszag { get; set; }
        public string Varos { get; set; }
    }
}
