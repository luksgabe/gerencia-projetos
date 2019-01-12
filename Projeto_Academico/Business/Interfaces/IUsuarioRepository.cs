using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using Projeto_Academico.Models.ViewModels;
using Projeto_Academico.ViewModels;

namespace Projeto_Academico.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        IEnumerable<Usuario> BuscarRegistrosLogin(LoginViewModel model);
        Usuario BuscarLogin(LoginViewModel model);
        IEnumerable<Usuario> BuscarPorEmail(string email);
        void ValidaUsuario(string email);
        void RegistrarUsuario(CadastroInicialViewModel usuario);
        string AtivarContaUsuario(Usuario usuario);
        bool ConfirmarUsuario(string cod);
        Usuario SalvaDependencias(CadastroInicialViewModel model);
        Usuario recuperaUsuario(Usuario usuario = null, CadastroInicialViewModel model = null);
        Empresa recuperaEmpresa(Usuario usuario, CadastroInicialViewModel model);
        GrupoUsuario recuperaGrupoUsuario(Usuario usuario);
        Usuario recuperaUsuarioEmSessao();
        IEnumerable<Usuario> recuperaListaUsuario();
        UsuariosViewModel EditarUsuario(UsuariosViewModel model);

    }



}
