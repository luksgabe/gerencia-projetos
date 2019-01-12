using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto_Academico.Models;
using Projeto_Academico.Models.ViewModels;
using System.Security.Cryptography;
using System.Data.Entity;
using Projeto_Academico.Business.Repository;
using Projeto_Academico.Business;

namespace Projeto_Academico.Controllers
{
    public class InicioController : Controller
    {

        private Projeto_AcademicoContext db = new Projeto_AcademicoContext();
        protected UsuarioRepository repUsuario = new UsuarioRepository();
        protected EmpresaRepository repEmpresa = new EmpresaRepository();

        // GET: Inicio
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Cadastro()
        //{
        //    return View();
        //}


        public ActionResult Cadastro(string codEmp = null)
        {
            if (!string.IsNullOrEmpty(codEmp))
            {
                CadastroInicialViewModel model = new CadastroInicialViewModel();
                model.Empresa = repUsuario.GetNomeEmpresa(codEmp);
                ViewBag.convidado = model.convidado = true;
                model.codEmpresa = new Guid(codEmp);
                Session["convidado"] = model.convidado;
                Session["codEmpresa"] = model.codEmpresa;

                return View(model);
            }
            else
            {
                ViewBag.Convidado = false;
                return View();
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(CadastroInicialViewModel model, string returnUrl, [Bind(Include = "nome, apelido, email, senha, verificado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Session["convidado"] != null)
                    {
                        model.convidado = Convert.ToBoolean(Session["convidado"]);
                        model.codEmpresa = new Guid(Session["codEmpresa"].ToString());
                    }
                    repUsuario.ValidaUsuario(model.Email);
                    repUsuario.RegistrarUsuario(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View();
                }

                return RedirectToAction("Validar");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Validar()
        {
            return View();
        }

        public ActionResult Confirmar(string cod)
        {
            try
            {
                var ativacao = db.Ativacao.Include(u => u.usuario).FirstOrDefault(p => p.codigo.Equals(cod));
                SessionControl.PopulaSessao(ativacao.usuario);
                ViewBag.NomeUsuario = SessionControl.UserName;
                ViewBag.CargoUsuario = SessionControl.Cargo;

                if (repUsuario.ConfirmarUsuario(cod))
                {
                    RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
    }
}