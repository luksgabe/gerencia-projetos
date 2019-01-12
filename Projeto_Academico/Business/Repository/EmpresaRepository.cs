using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Business.Repository
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        public Empresa RecuperaEmpresaID(int id)
        {
            return GetById(id);
        }

        public Empresa RecuperaEmpresaCod(Guid cod)
        {
            //Guid codigo = new Guid(cod);
            return GetAll().FirstOrDefault(e => e.codEmpresa.Equals(cod));
        }

        public void alteraEmpresa(Empresa empresa)
        {
            Db.Empresa.Attach(empresa);
            Update(empresa);
        }

    }
}