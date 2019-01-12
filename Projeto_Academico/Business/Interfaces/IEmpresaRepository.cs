using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Academico.Business.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Empresa RecuperaEmpresaID(int id);

        Empresa RecuperaEmpresaCod(Guid cod);

        void alteraEmpresa(Empresa empresa);
        
    }
}
