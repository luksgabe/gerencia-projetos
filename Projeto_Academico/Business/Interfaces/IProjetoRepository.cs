using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using Projeto_Academico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Projeto_Academico.Business.Interfaces
{
    public interface IProjetoRepository : IRepository<Projeto>
    {
        Projeto NovoProjeto(NovoProjetoViewModel model);
    }
}
