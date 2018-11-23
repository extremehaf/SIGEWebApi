using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEWebApi.Controllers
{
    public class OperacaoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarFuncionario()
        {
            return View("Cadastro.cshtml");
        }
    }
}