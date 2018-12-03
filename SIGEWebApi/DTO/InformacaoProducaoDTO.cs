using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.DTO
{
    public class InformacaoProducaoDTO
    {
        public int quantidade { get; set; }
        public string mes { get; set; }
        public string turno { get; set; }
        public string nomeProduto { get; set; }
    }
}