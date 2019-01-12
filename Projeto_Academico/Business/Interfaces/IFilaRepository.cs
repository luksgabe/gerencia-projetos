using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Academico.Business.Interfaces
{
    public interface IFilaRepository : IRepository<Fila>
    {

        Fila RecuperaFila(long id);

        void AdicionarFila(string nome, long idProjeto);
    }
}
