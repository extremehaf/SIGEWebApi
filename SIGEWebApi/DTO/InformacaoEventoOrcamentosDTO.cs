using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.DTO
{
    public class InformacaoEventoOrcamentosDTO
    {
        public int IdEventoOrcamento { get; set; }
        public string Fornecedor { get; set; }
        public double Gasto { get; set; }
        public double Orcamento { get; set; }
        public int IdEventoFK { get; set; }
        public string evento { get; set; }
    }
}