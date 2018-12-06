using SIGEWebApi.DTO;
using SIGEWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.ViewModels
{
    public class RelatoriosViewModel
    {
        public List<InformacaoEventoOrcamentosDTO> listaEventosOrcamentos { get; set; }

        public List<IGrouping<string, InformacaoProducaoDTO>> listaInformacoesProducao { get; set; }
        public List<InformacaoFinanceiroDTO> listaInformacoesFinanceiro { get; set; }
        public List<InformacaoVendasDTO> listaInformacoesDeptoVendas { get; set; }

        public List<HorasTrabalhadas> listaHorasTrabalhadas { get; set; }

        public List<InformacaoEventosDTO> listaEventos{ get; set; }
    }
}