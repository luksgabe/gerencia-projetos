using Projeto_Academico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto_Academico.Models;
using Projeto_Academico.Business.Repository;
using System.Net;
using Projeto_Academico.Business;
using System.IO;

namespace Projeto_Academico.Controllers
{

    [MyAuthorize]
    public class ControleController : BaseController
    {

        UsuarioRepository repUser = new UsuarioRepository();

        // GET: Controle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Convidar()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            UsuariosViewModel model = new UsuariosViewModel();


            model.listaUsuario = repUser.recuperaListaUsuario();
            return View(model);
        }

        [HttpGet]
        public ActionResult UsuarioEdicao(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            GrupoUsuarioRepository repGrupo = new GrupoUsuarioRepository();
            var model = repUser.UsuarioDV(id);

            var user = repUser.GetById((int)id);
            var perfilId = user.grupoUsuario.id;

            ViewBag.Perfil = new SelectList(model.listaGrupo, "id", "nome", perfilId);
            TempData["UsuarioID"] = id;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioEdicao(UsuariosViewModel model, string listaGrupoIndexChange)
        {
            GrupoUsuarioRepository repGrupo = new GrupoUsuarioRepository();
            var user = repUser.GetById((int)model.Id);
            model.listaGrupo = repGrupo.ListaGrupoUsuarioEmpresa(user.empresa.id);

            ViewBag.Perfil = new SelectList(model.listaGrupo, "id", "nome", Convert.ToInt64(listaGrupoIndexChange));

            if (string.IsNullOrEmpty(repUser.ValidaEdicaoUsuario(model)))
            {
                model.listaGrupoIndexChange = Convert.ToInt16(listaGrupoIndexChange);

                model = Editar(model);
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", repUser.ValidaEdicaoUsuario(model));
                return View(model);
            }
        }

        public ActionResult MeuPerfil(Usuario user = null)
        {
            UsuariosViewModel model = new UsuariosViewModel();
            UploadFileRepository repUpd = new UploadFileRepository();

            try
            {
                if (user.id != 0)
                {
                    model = repUser.UsuarioDV(user.id);
                    ViewBag.ImagemPerfil = model.ImagemCaminho;
                    return View(model);
                }
                else
                {
                    var usuario = repUser.recuperaUsuarioEmSessao();

                    model = repUser.UsuarioDV(usuario.id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            ViewBag.ImagemPerfil = model.ImagemCaminho;
            return View(model);
        }

        #region Ações
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EnviarConvite(ConvidarViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Servico_Email.Envio_Email(model.Email, null, "Convite à " + model.Nome + " para ingressar no SGP", "Convidar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return RedirectToAction("Convidar");
        }

        public UsuariosViewModel Editar(UsuariosViewModel model)
        {
            model = repUser.EditarUsuario(model);
            return model;
        }

        public ActionResult AlterarPerfil(UsuariosViewModel model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var upload = UploadImage(file, model.Id);
                model.ImagemCaminho = upload.caminhoServidor;
            }
            else
            {
                model.ImagemCaminho = string.Empty;
            }
            var user = repUser.EditarPerfil(model);
            return RedirectToAction("MeuPerfil", user);
        }

        public UploadFile UploadImage(HttpPostedFileBase file, long codUser = 0)
        {
            UploadFileRepository repUpd = new UploadFileRepository();
            UploadFile upload = new UploadFile();
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (file.ContentLength > 0)
                    {

                        string filename = Path.GetFileName(file.FileName);
                        string contentType = Path.GetFileName(file.FileName);

                        var uploadPath = Server.MapPath("~/Content/Imagens");
                        var caminhoArquivo = Path.Combine(@uploadPath, filename);
                        file.SaveAs(caminhoArquivo);
                        var caminhoArquivoServidor = "/Content/Imagens/" + contentType;

                        upload = repUpd.SalvaImagemUsuario(uploadPath, caminhoArquivo, caminhoArquivoServidor, filename, contentType, file.ContentLength, codUser);

                        return upload;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return upload;
        }


        #endregion

    }
}