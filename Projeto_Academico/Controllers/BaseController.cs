using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Projeto_Academico.Models;
using Projeto_Academico.Business.Repository;
using Projeto_Academico.Business;
using System.IO;

namespace Projeto_Academico.Controllers
{
    public class BaseController : Controller
    {
        private readonly Projeto_AcademicoContext context = new Projeto_AcademicoContext();
        private readonly UsuarioRepository repUser = new UsuarioRepository();
        private readonly UploadFileRepository repFile = new UploadFileRepository();

        [MyAuthorize]
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var dados = repUser.recuperaUsuarioEmSessao();
            if (dados != null)
            {
                ViewBag.UserName = dados.apelido;
                ViewBag.Cargo = dados.cargo;
                ViewBag.EmpresaNome = dados.empresa.nome;
                string caminhoImagem = dados.uploadImage != null ? dados.uploadImage.caminhoServidor : string.Empty;
                ViewBag.imgPerfil = repFile.BuscarArquivoPorCaminho(caminhoImagem);
                ViewBag.ListaProjetos = dados.listaProjetosEnvolvidos;
            }

        }


        public ActionResult ActionUpload(string action, string controller, HttpPostedFileBase file = null, Usuario user = null)
        {



            return RedirectToAction(action, controller);
        }

        public string DataHoraAtual()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }



        // GET: Entidade



        //public ActionResult Index()
        //{
        //    return View(db.Usuarios.ToList());
        //}

        //// GET: Entidade/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Usuario usuario = db.Usuarios.Find(id);
        //    if (usuario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usuario);
        //}

        //// GET: Entidade/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Entidade/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,nome,email,senha")] Usuario usuario)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Usuarios.Add(usuario);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(usuario);
        //}

        //// GET: Entidade/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Usuario usuario = db.Usuarios.Find(id);
        //    if (usuario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usuario);
        //}

        //// POST: Entidade/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,nome,email,senha")] Usuario usuario)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(usuario).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(usuario);
        //}

        //// GET: Entidade/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Usuario usuario = db.Usuarios.Find(id);
        //    if (usuario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usuario);
        //}

        //// POST: Entidade/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Usuario usuario = db.Usuarios.Find(id);
        //    db.Usuarios.Remove(usuario);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
