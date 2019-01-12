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
    public class ProcessoRepository : Repository<Processo>, IProcessoRepository
    {
        public Processo novoProcesso(long idProjeto)
        {
            var projeto = Db.Projeto.FirstOrDefault(p => p.id.Equals(idProjeto));

            var processo = new Processo
            {
                nome = "BackLog",
                listaTarefas = new List<Tarefa>(),
                ativo = true,
                projeto = projeto
            };

            Db.Projeto.Attach(processo.projeto);
            Db.Processo.Add(processo);
            Db.SaveChanges();

            return processo;
        }

        public void AdicionarProcesso(string nome, long idProjeto)
        {
            var projeto = Db.Projeto.FirstOrDefault(p => p.id.Equals(idProjeto));

            var processo = new Processo
            {
                projeto = projeto,
                nome = nome,
                ativo = true,
                listaTarefas = new List<Tarefa>()
            };

            Db.Projeto.Attach(projeto);

            Db.Processo.Add(processo);
            Db.SaveChanges();

            projeto.listaProcessos.Add(processo);
            Db.Entry(projeto).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void AlterarTarefaProcesso(TarefaViewModel model)
        {

            var tarefa = Db.Tarefa.FirstOrDefault(t => t.id.Equals(model.Id));

            var processo = Db.Processo.FirstOrDefault(m => m.id == model.ProcessoID);
            if (tarefa.processo.id != processo.id)
            {
                tarefa.processo = processo;
                Db.Processo.Attach(processo);
            }

            Db.Entry(tarefa).State = EntityState.Modified;
            Db.SaveChanges();


        }

    }
}