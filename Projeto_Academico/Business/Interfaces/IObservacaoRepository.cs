using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Academico.Business.Interfaces
{
    public interface IObservacaoRepository : IRepository<Observacoes>
    {

        void SalvaObservacao(Observacoes observacoes);
        ICollection<Observacoes> listaObservacoes(Tarefa tarefa);
    }
}
