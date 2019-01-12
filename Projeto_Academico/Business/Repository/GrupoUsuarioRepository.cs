using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Business.Repository
{
    public class GrupoUsuarioRepository : Repository<GrupoUsuario>,IGrupoUsuarioRepository
    {

        public void EstanciaPerfisDefault(Usuario usuario)
        {
            var administrador = new GrupoUsuario { nome = "Administrador",  empresaID = usuario.empresa.id, ativo = true };
            Db.Empresa.Attach(usuario.empresa);
            Add(administrador);

            var GerenteProjetos = new GrupoUsuario { nome = "Gerente de Projetos", empresaID = usuario.empresa.id, ativo = true };
            Db.Empresa.Attach(usuario.empresa);

            var Analista = new GrupoUsuario { nome = "Analista de Sistemas", empresaID = usuario.empresa.id, ativo = true };
            Db.Empresa.Attach(usuario.empresa);
            Add(Analista);

            var Desenvolvedor = new GrupoUsuario { nome = "Desenvolvedor", empresaID = usuario.empresa.id, ativo = true };
            Db.Empresa.Attach(usuario.empresa);
            Add(Desenvolvedor);
        }

        public IEnumerable<GrupoUsuario> ListaGrupoUsuarioEmpresa(long empresaID)
        {
            return GetAll().Where(g => g.empresaID == empresaID);
        }
    }
}