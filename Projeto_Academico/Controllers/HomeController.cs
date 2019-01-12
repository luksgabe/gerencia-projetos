using Projeto_Academico.Business;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_Academico.Controllers
{
    public class HomeController : BaseController
    {

        // GET: Home
        [MyAuthorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Sair()
        {
            SessionControl.FimSession();
            return RedirectToAction("Index", "Login");
        }
    }
}