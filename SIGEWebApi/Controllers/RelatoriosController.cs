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
            var lstRetorno = await IntegracaoProducao.GetAsync("getAllProducaoPorMesTurno");
            IEnumerable<IGrouping<string, InformacaoProducaoDTO>> agrupado = lstRetorno.GroupBy(i => i.mes);
            retorno.listaInformacoesProducao = agrupado;

            return View(retorno);
        }

        // GET: Relatorios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Relatorios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relatorios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relatorios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Relatorios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relatorios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Relatorios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
