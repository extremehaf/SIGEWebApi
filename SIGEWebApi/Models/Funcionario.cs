using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SIGEWebApi.Models
{
    [Table("FUNCIONARIO")]
    public class Funcionario
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 1)]
        public string Nome { get; set; }

        [Column(Order = 2)]
        public int Matricula { get; set; }

        [Column(Order = 3)]
        public string Cargo { get; set; }
    }
}