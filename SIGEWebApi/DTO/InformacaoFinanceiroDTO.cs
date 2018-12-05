using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.DTO
{
    public class InformacaoFinanceiroDTO
    {
        public string mes { get; set; }
        public string idProduto { get; set; }
        public string turno { get; set; }
        public string valor { get; set; }
    }
}