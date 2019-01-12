using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Business.Repository
{
    public class ObservacaoRepository : Repository<Observacoes>, IObservacaoRepository
    {
        UsuarioRepository repUser = new UsuarioRepository();
        TarefaRepository repTask = new TarefaRepository();
        ProjetoRepository repPro = new ProjetoRepository();

        public void SalvaObservacao(Observacoes observacoes)
        {
            var task = repTask.GetById(Convert.ToInt16(SessionControl.TarefaId));
            var user = repUser.recuperaUsuarioEmSessao();

            observacoes.tarefa = task;
            observacoes.usuario = user;
            observacoes.dataCriacao = DateTime.Now;
            repUser.Dispose();
            repTask.Dispose();

            Db.Usuarios.Attach(observacoes.usuario);
            Db.Tarefa.Attach(observacoes.tarefa);

            Db.Observacoes.Add(observacoes);
            Db.SaveChanges();

        }
        public ICollection<Observacoes> listaObservacoes(Tarefa tarefa)
        {
            return GetAll().Where(o => o.tarefa.id == tarefa.id).ToList();
        }

    }
}