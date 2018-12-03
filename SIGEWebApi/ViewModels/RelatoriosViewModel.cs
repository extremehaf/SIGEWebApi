using SIGEWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.ViewModels
{
    public class RelatoriosViewModel
    {
        public IEnumerable<IGrouping<string, InformacaoProducaoDTO>> listaInformacoesProducao { get; set; }
    }
}