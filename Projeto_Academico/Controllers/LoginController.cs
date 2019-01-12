using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto_Academico.Models;
using Projeto_Academico.Models.ViewModels;
using Projeto_Academico.Business;
using System.Security.Cryptography;
using Projeto_Academico.Business.Repository;
using System.Web.Security;

namespace Projeto_Academico.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        protected UsuarioRepository userRep = new UsuarioRepository();
        protected TelaRepository telaRep = new TelaRepository();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model, string returnUrl, [Bind(Include = "nome, apelido, email, senha, verificado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuario = userRep.BuscarLogin(model);
                    SessionControl.PopulaSessao(usuario);
                    telaRep.VerificaNovasTelas(usuario.grupoUsuario);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}