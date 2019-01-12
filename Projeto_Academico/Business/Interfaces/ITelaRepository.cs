using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;

namespace Projeto_Academico.Business.Interfaces
{
    public interface ITelaRepository : IRepository<Tela>
    {
        IEnumerable<Tela> registrosTela();

        IEnumerable<GrupoUsuario> registrosGrupo();

        void VerificaNovasTelas(GrupoUsuario perfil);
    }
}
