using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEWebApi.Models
{
    public class HorasTrabalhadas
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 1)]
        public string HorasTrabalhadasNoMes { get; set; }

        [Required]
        [Column(Order = 2)]
        public int Mes { get; set; }
    }
}