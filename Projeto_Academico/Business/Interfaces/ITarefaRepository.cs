using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using Projeto_Academico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Academico.Business.Interfaces
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Tarefa IncluirTarefa(ProjetoViewModel pTarefa);
        IEnumerable<Tarefa> ListaTarefas(long idFila);
        Tarefa RecuperaTarefa(long id);
        Tarefa AlterarTarefa(TarefaViewModel model);
    }
}
