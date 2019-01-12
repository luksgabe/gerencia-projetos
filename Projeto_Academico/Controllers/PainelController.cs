using Projeto_Academico.Business;
using Projeto_Academico.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Projeto_Academico.Models;
using Projeto_Academico.ViewModels;
using System.Net;
using Newtonsoft.Json.Converters;

namespace Projeto_Academico.Controllers
{
    [MyAuthorize]
    public class PainelController : BaseController
    {
        ProjetoRepository repPro = new ProjetoRepository();
        TarefaRepository repTask = new TarefaRepository();

        // GET: Projeto
        public ActionResult Index()
        {
            ViewBag.Clientes = ClienteRepository.verificaClientes();
            ViewBag.listaCliente = ClienteRepository.populaDdlCliente();
            ViewBag.responsavelProjeto = ProjetoRepository.populaDdlUsuario();
            ViewBag.listaCenarios = ProjetoRepository.populaDdlCenarios();

            UsuarioRepository userRep = new UsuarioRepository();
            var user = userRep.recuperaUsuarioEmSessao();
            ViewBag.nTarefas = TarefaRepository.NumeroTarefas(user.id);


            return View();
        }

        public ActionResult Projeto(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                SessionControl.ProjetoId = id.ToString();
                var projeto = repPro.GetById((int)id);

                ViewBag.NomeProjeto = projeto.nome;
                ProjetoViewModel model = new ProjetoViewModel();

                model.NomeProjeto = projeto.nome;
                model.Tipo = projeto.tipo;

                ViewBag.ListFilas = model.ListaFilas = projeto.listaFilas;

                ViewBag.listaUsuario = ProjetoRepository.populaDdlUsuario();
                ViewBag.listaUsers = ProjetoRepository.populaMultDdlUsuarios(SessionControl.ProjetoId);
                ViewBag.sizeUserList = ProjetoRepository.populaMultDdlUsuarios(SessionControl.ProjetoId).Count();

                return View(model);
            }
        }

        public ActionResult Tarefa(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                SessionControl.TarefaId = id.ToString();
                var model = new TarefaViewModel();
                try
                {
                    model = TarefaRepository.TarefaDV(id);
                    ViewBag.listaUsuario = ProjetoRepository.populaDdlUsuario();
                    ViewBag.listaProcesso = ProjetoRepository.populaDdlProcesso();

                }
                catch (Exception ex)
                {
                    throw;
                }
                return View(model);
            }
        }

        public ActionResult KanbanBoard()
        {
            long projetoID = Convert.ToInt64(SessionControl.ProjetoId);
            ViewBag.idProjeto = projetoID;

            ProjetoViewModel model = new ProjetoViewModel();

            var projeto = repPro.GetById((int)projetoID);

            model.ListaProcessos = projeto.listaProcessos;
            return View(model);
        }


        #region Metodos de ação

        [HttpPost]
        public ActionResult AdicionarFila(ProjetoViewModel model)
        {
            FilaRepository repFila = new FilaRepository();
            repFila.AdicionarFila(model.fila.Nome, Convert.ToInt64(SessionControl.ProjetoId));

            return RedirectToAction("Projeto", new { id = SessionControl.ProjetoId });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarProcesso(ProjetoViewModel model)
        {
            ProcessoRepository repProcs = new ProcessoRepository();

            repProcs.AdicionarProcesso(model.NovoProcesso.Nome, Convert.ToInt64(SessionControl.ProjetoId));

            return RedirectToAction("KanbanBoard", new { id = Convert.ToInt16(SessionControl.ProjetoId) });
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarUsuario(ProjetoViewModel model, int[] IdUsuario)
        {
            try
            {
                string msgErro = repPro.IncluirUsuario(IdUsuario, Convert.ToInt16(SessionControl.ProjetoId));
                if (!string.IsNullOrEmpty(msgErro))
                {
                    throw new Exception(msgErro);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("Projeto", new { id = Convert.ToInt16(SessionControl.ProjetoId) });
        }


        public ActionResult SalvarComentario(string modelo)
        {
            ObservacaoRepository repObs = new ObservacaoRepository();

            repObs.SalvaObservacao(JsonConvert.DeserializeObject<Observacoes>(modelo));

            return RedirectToAction("Tarefa", new { id = Convert.ToInt64(SessionControl.TarefaId) });
        }

        public ActionResult AlterarTarefa(string modelo)
        {
            var format = "dd/MM/yyyy HH:mm:ss";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            var model = JsonConvert.DeserializeObject<TarefaViewModel>(modelo, dateTimeConverter);

            Tarefa tarefa = new Tarefa();
            try
            {
                tarefa = repTask.AlterarTarefa(model);
            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("Tarefa", new { id = tarefa.id });
        }

        public ActionResult AlterarProcesso(string modelo)
        {
            var model = JsonConvert.DeserializeObject<TarefaViewModel>(modelo);

            try
            {
                ProcessoRepository repProcs = new ProcessoRepository();
                repProcs.AlterarTarefaProcesso(model);
            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("KanbanBoard");
        }

        [HttpPost]
        public ActionResult IncluirTarefa(string modelo)
        {
            try
            {
                TarefaRepository repTarefa = new TarefaRepository();
                var tarefa = repTarefa.IncluirTarefa(JsonConvert.DeserializeObject<ProjetoViewModel>(modelo));
            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("Projeto", new { id = Convert.ToInt64(SessionControl.ProjetoId) });
        }


        [HttpPost]
        public ActionResult ProjetoSalvarCliente(string modelo)
        {
            try
            {
                ClienteRepository repCliente = new ClienteRepository();

                var cliente = repCliente.SalvarCliente(JsonConvert.DeserializeObject<Cliente>(modelo));
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NovoProjeto(NovoProjetoViewModel model)
        {
            ProjetoRepository repPro = new ProjetoRepository();
            var projeto = repPro.NovoProjeto(model);

            return RedirectToAction("Projeto", new { id = projeto.id });
        }

        public ActionResult DadosTarefa(string modelo)
        {
            var model = JsonConvert.DeserializeObject<TarefaViewModel>(modelo);

            model = TarefaRepository.TarefaDV(model.Id);

            //return RedirectToAction("KanbanBoard", model);

            return Json(new { data = model });
        }

        #endregion

    }
}