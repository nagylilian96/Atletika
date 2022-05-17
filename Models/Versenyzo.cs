using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Atletika.Models
{
    [Table("Versenyzo")]
    public class Versenyzo
    {
        [Key]
        public int VersId { get; set; }
        public string Nev { get; set; }
        public bool Neme { get; set; }
    }
}
