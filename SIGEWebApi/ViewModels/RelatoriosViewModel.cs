using SIGEWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.ViewModels
{
    public class RelatoriosViewModel
    {
        public List<IGrouping<string, InformacaoProducaoDTO>> listaInformacoesProducao { get; set; }
        public List<InformacaoFinanceiroDTO> listaInformacoesFinanceiro { get; set; }

        public List<InformacaoVendasDTO> listaInformacoesDeptoVendas { get; set; }
    }
}