using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using Projeto_Academico.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Business.Repository
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {

        public Tarefa IncluirTarefa(ProjetoViewModel pTarefa)
        {
            FilaRepository repFila = new FilaRepository();
            UsuarioRepository repUsuario = new UsuarioRepository();

            var fila = Db.Fila.FirstOrDefault(p => p.id.Equals(pTarefa.IdFila));

            var user = Db.Usuarios.FirstOrDefault(p => p.id.Equals(pTarefa.IdUsuario));

            var processo = Db.Processo.FirstOrDefault(p => p.projeto.id.Equals(fila.projeto.id) && p.nome.Equals("BackLog"));

            var tarefa = new Tarefa
            {
                nome = pTarefa.NomeTarefa,
                dataInicio = pTarefa.DataInicio,
                dataEntrega = pTarefa.DataVencimento,
                descricao = pTarefa.Descricao,
                fila = fila,
                ativo = true,
                tempoTotal = string.Empty,
                processo = processo
            };

            tarefa.listaArquivos = new List<UploadFile>();

            tarefa.listaUsuarios = new List<Usuario>();
            tarefa.listaUsuarios.Add(user);

            if (user.listaTarefas == null)
                user.listaTarefas = new List<Tarefa>();

            fila.listaTarefas.Add(tarefa);
            processo.listaTarefas.Add(tarefa);

            Db.Fila.Attach(tarefa.fila);
            Db.Processo.Attach(tarefa.processo);

            Db.Tarefa.Add(tarefa);
            Db.SaveChanges();

            Db.Entry(fila).State = EntityState.Modified;
            Db.SaveChanges();

            user.listaTarefas.Add(tarefa);

            Db.Entry(tarefa).State = EntityState.Modified;
            Db.SaveChanges();

            Db.Entry(user).State = EntityState.Modified;
            Db.SaveChanges();

            return tarefa;
        }

        public Tarefa AlterarTarefa(TarefaViewModel model)
        {
            var tarefa = GetById((int)model.Id);

            tarefa.dataInicio = model.DataInicio;
            tarefa.dataEntrega = model.DataEntrega;
            tarefa.descricao = model.Descricao;

            var listaUsuarios = tarefa.listaUsuarios.Where(u => u.id == model.UsuarioID).ToList();
            if (listaUsuarios.Count <= 0)
            {
                var usuario = Db.Usuarios.FirstOrDefault(u => u.id == model.Id);
                tarefa.listaUsuarios.Add(usuario);
            }

            var processo = Db.Processo.FirstOrDefault(m => m.id == model.ProcessoID);
            if (tarefa.processo.id != processo.id)
            {
                tarefa.processo = processo;
                Db.Processo.Attach(processo);
            }

            Db.Entry(tarefa).State = EntityState.Modified;
            Db.SaveChanges();


            return tarefa;
        }

        public IEnumerable<Tarefa> ListaTarefas(long idFila)
        {
            return GetAll().Where(p => p.fila.id.Equals(idFila));
        }

        public IEnumerable<Tarefa> ListaTarefasProcs(long idProc)
        {
            return GetAll().Where(p => p.processo.id.Equals(idProc));
        }

        public Tarefa RecuperaTarefa(long id)
        {
            return GetById((int)id);
        }

        public static long NumeroTarefas(long idUser)
        {
            Projeto_AcademicoContext db = new Projeto_AcademicoContext();
            var user = db.Usuarios.FirstOrDefault(x => x.id.Equals(idUser));

            return user.listaProjetosEnvolvidos.Count;
        }


        public static TarefaViewModel TarefaDV(long? id)
        {
            TarefaViewModel model = new TarefaViewModel();
            ObservacaoRepository repObs = new ObservacaoRepository();

            var repTask = new TarefaRepository();

            var tarefa = repTask.GetById((int)id);
            var responsavel = tarefa.listaUsuarios.FirstOrDefault();

            model.Id = tarefa.id;
            model.Nome = tarefa.nome;
            model.Descricao = tarefa.descricao;
            model.Observacoes = tarefa.listaObservacoes;
            model.DataInicio = tarefa.dataInicio;
            model.DataEntrega = tarefa.dataEntrega;
            model.ListaArquivos = tarefa.listaArquivos;
            model.ListaUsuarios = tarefa.listaUsuarios;
            model.ListaObservacoes = tarefa.listaObservacoes;
            model.Fila = tarefa.fila;
            model.Processo = tarefa.processo;
            model.TempoTotal = tarefa.tempoTotal;
            model.UsuarioID = responsavel.id;
            model.Observacoes = repObs.listaObservacoes(tarefa);
            model.tipoProjeto = Convert.ToInt64(tarefa.fila.projeto.tipo);
            model.ProcessoID = tarefa.processo.id;

            return model;
        }

    }
}