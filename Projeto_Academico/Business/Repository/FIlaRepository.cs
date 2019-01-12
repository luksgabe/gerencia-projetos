using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Business.Repository
{
    public class FilaRepository : Repository<Fila>, IFilaRepository
    {
        public Fila RecuperaFila(long id)
        {
            return GetById((int)id);
        }

        public void AdicionarFila(string nome, long idProjeto)
        {
            var fila = new Fila();
            var projeto = Db.Projeto.FirstOrDefault(p => p.id.Equals(idProjeto));

            fila.nome = nome;
            fila.projeto = projeto;
            fila.listaTarefas = new List<Tarefa>();
            fila.ativo = true;
            Db.Projeto.Attach(fila.projeto);

            Db.Fila.Add(fila);
            Db.SaveChanges();

            projeto.listaFilas.Add(fila);
            Db.Entry(projeto).State = EntityState.Modified;
            Db.SaveChanges();

        }


    }
}