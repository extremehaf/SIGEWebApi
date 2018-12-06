using SIGEWebApi.DAL;
using SIGEWebApi.DTO;
using SIGEWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace SIGEWebApi.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public async Task<ActionResult> Index()
        {
            RelatoriosViewModel retorno = new RelatoriosViewModel();
            HorasTrabalhadasController HtController = new HorasTrabalhadasController();
            await BuscarDadosProducao(retorno);

            retorno.listaInformacoesFinanceiro = await BuscarDadosFinanceiro();
            retorno.listaInformacoesDeptoVendas = await BuscarDadosDeptoVendas();
            retorno.listaHorasTrabalhadas =  HtController.GetHorasTrabalhadas().ToList();
            retorno.listaEventos = await BuscarEventos();
            //retorno.listaEventosOrcamentos = await BuscarEventosOrcamentos();

            return View(retorno);
        }

        private static async Task BuscarDadosProducao(RelatoriosViewModel retorno)
        {
            var lstRetorno = await IntegracaoProducao.GetAsync("getAllProducaoPorMesTurno");
            List<IGrouping<string, InformacaoProducaoDTO>> agrupado = lstRetorno.GroupBy(i => i.mes.ToLower().Trim()).ToList();


            retorno.listaInformacoesProducao = agrupado.OrderBy(x => x.Key).ToList();
        }

        private static async Task<List<InformacaoFinanceiroDTO>> BuscarDadosFinanceiro()
        {
            var lstRetorno = await IntegracaoFinanceiro.GetAsync("api/GastosProducao");

            return lstRetorno;
        }

        private static async Task<List<InformacaoVendasDTO>> BuscarDadosDeptoVendas()
        {
            var lstRetorno = await IntegracaoVendas.GetAsync("api/Vendas");

            return lstRetorno;
        }

        private static async Task<List<InformacaoEventosDTO>> BuscarEventos()
        {
            var lstRetorno = await IntegracaoVendas.GetEventosAsync("api/Eventos");

            var lstOrcamentos = await IntegracaoVendas.GetEventosOrcamentosAsync("api/EventoOrcamentos");

            foreach(var evento in lstRetorno)
            {
                evento.eventoOrcamentos = lstOrcamentos.Where(x => x.IdEventoFK == evento.IdEvento).Sum(y => y.Gasto);
            }

            return lstRetorno;
        }

        private static async Task<List<InformacaoEventoOrcamentosDTO>> BuscarEventosOrcamentos()
        {
            var lstRetorno = await IntegracaoVendas.GetEventosOrcamentosAsync("api/EventoOrcamentos");

            return lstRetorno;
        }

        public static int ConverterMesToInt(string mes)
        {
            switch (mes.ToLower().Trim())
            {
                case "janeiro":
                    return 1;
                case "fevereiro":
                    return 2;
                case "março":
                    return 3;
                case "abril":
                    return 4;
                case "maio":
                    return 5;
                case "junho":
                    return 6;
                case "julho":
                    return 7;
                case "agosto":
                    return 8;
                case "setembro":
                    return 9;
                case "outubro":
                    return 10;
                case "novembro":
                    return 11;
                case "dezembro":
                    return 12;
                default: return 0;
            }
        }

        //// GET: Relatorios/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Relatorios/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Relatorios/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Relatorios/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Relatorios/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Relatorios/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Relatorios/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
