using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.DTO
{
    public class InformacaoEventoOrcamentosDTO
    {
        public int IdEventoOrcamento { get; set; }
        public int Fornecedor { get; set; }
        public int Gasto { get; set; }
        public int Orcamento { get; set; }
        public int IdEventoFK { get; set; }
        public int evento { get; set; }
    }
}