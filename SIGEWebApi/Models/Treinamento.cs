using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEWebApi.Models
{
    [Table("Treinamento")]
    public class Treinamento
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        public int Id { get; set; }
        public string Descricação { get; set; }
        public DateTime Data { get;set;      }
        public double custo { get; set; }
        public string AreaEnvolvida { get; set; }
        public string AreaRequisitante { get; set; }
        public string Motivo { get; set; }
        public string obs { get; set; }

    }
}