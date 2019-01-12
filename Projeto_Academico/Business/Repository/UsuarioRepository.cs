using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto_Academico.Models;
using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models.ViewModels;
using System.Security.Cryptography;
using System.Data.Entity;
using Projeto_Academico.ViewModels;
using System.IO;

namespace Projeto_Academico.Business.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        Repository<Ativacao> repoAtivacao = new Repository<Ativacao>();
        EmpresaRepository repEmpresa = new EmpresaRepository();
        GrupoUsuarioRepository repGrupo = new GrupoUsuarioRepository();

        public IEnumerable<Usuario> BuscarRegistrosLogin(LoginViewModel model)
        {
            return GetAll().Where(m => m.email.Equals(model.Login) && m.senha.Equals(model.Senha));
        }

        public IList<Ativacao> BuscarRegistroAtivacao(string cod = null)
        {
            return cod == null ? Db.Ativacao.Include(u => u.usuario).ToList() : Db.Ativacao.Where(a => a.codigo.Equals(cod)).Include(u => u.usuario).ToList();
        }

        public Usuario BuscarLogin(LoginViewModel model)
        {
            var hash = new Hash(SHA512.Create());
            model.Senha = hash.GerarHash(model.Senha);

            var listaUsuarios = BuscarRegistrosLogin(model).ToList();

            if (listaUsuarios.Count().Equals(0) || listaUsuarios.Equals(null))
            {
                throw new Exception("Usuário ou senha incorreto");
            }
            else if (!listaUsuarios.FirstOrDefault().verificado)
            {
                throw new Exception("Conta não validada, verifique sua caixa de entrada em seu e-mail para valida-la");
            }
            
            return listaUsuarios.FirstOrDefault();
        }

        public string GetNomeEmpresa(string codEmp)
        {
            EmpresaRepository repEmpresa = new EmpresaRepository();
            var empresa = repEmpresa.RecuperaEmpresaCod(new Guid(codEmp));

            return empresa != null ? empresa.nome : string.Empty;
        }

        public IEnumerable<Usuario> BuscarPorEmail(string email)
        {
            return GetAll().Where(p => p.email.Equals(email));
        }

        public void ValidaUsuario(string email)
        {
            var registro = BuscarPorEmail(email).ToList();
            if (registro.Count > 0 && registro != null)
            {
                if (!registro.FirstOrDefault().verificado)
                {
                    throw new Exception("Conta não validada, verifique sua caixa de entrada em seu e-mail para valida-la");
                }
                throw new Exception("E-mail já cadastrado");
            }
        }

        public void RegistrarUsuario(CadastroInicialViewModel model)
        {
            Servico_Email email = new Servico_Email();
            var usuario = SalvaDependencias(model);

            //Add(usuario);

            email.Email_Autentic(usuario);
        }

        public string AtivarContaUsuario(Usuario pUsuario)
        {
            string codigo = string.Empty;

            codigo = Util.gerarChave().ToString();
            codigo = codigo.Replace(":", "");
            codigo = codigo.Replace("-", "");
            codigo = codigo.Trim();

            //var entidade = GetById((int)usuario.id);
            var ativacao = new Ativacao { codigo = codigo, dataAtivacao = 
                
            DateTime.Today, usuario = pUsuario };
            Db.Usuarios.Attach(ativacao.usuario);
            Db.Ativacao.Add(ativacao);
            Db.SaveChanges();
            codigo = ativacao.codigo;
            return codigo;
        }

        public bool ConfirmarUsuario(string cod)
        {
            Ativacao ativacao = new Ativacao();
            Usuario usuario = new Usuario();
            ativacao = BuscarRegistroAtivacao(cod).FirstOrDefault();
            if (ativacao != null)
            {
                usuario = GetById(Convert.ToInt16(ativacao.usuario.id));
                if (!usuario.verificado)
                {
                    usuario.verificado = true;
                    Update(usuario);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                throw new Exception("Código de ativação inválido");
            }
        }

        public Usuario SalvaDependencias(CadastroInicialViewModel model)
        {
            var user = recuperaUsuario(null, model);

            if (!model.convidado)
            {
                user.empresa = recuperaEmpresa(user, model);
                Db.Empresa.Attach(user.empresa);
            }
            else
            {
                user.empresa = salvaEmpresaUsuario(user, model);
            }
            if (!model.convidado)
            {
                repGrupo.EstanciaPerfisDefault(user);
            }

            user.grupoUsuario = recuperaGrupoUsuario(user);

            Db.Entry(user).State = EntityState.Modified;
            Db.SaveChanges();
            Db.Dispose();

            return user;
        }

        public Usuario recuperaUsuario(Usuario usuario = null, CadastroInicialViewModel model = null)
        {
            var hash = new Hash(SHA512.Create());

            if (usuario != null)
            {
                Add(usuario);
            }
            else if (model != null)
            {
                //popula usuario e salva no banco
                usuario = new Usuario
                {
                    nome = model.Nome,
                    apelido = model.Apelido,
                    email = model.Email,
                    senha = hash.GerarHash(model.Senha),
                    ativo = true
                };
                Add(usuario);
                //Dispose();
            }

            return usuario;
        }

        public Empresa recuperaEmpresa(Usuario usuario, CadastroInicialViewModel model)
        {
            //if (!string.IsNullOrEmpty(nomeEmpresa))
            //{
            //    if (Db.Empresa.Where(p => p.nome.Equals(nomeEmpresa)).ToList().Count > 0 && Db.Empresa.Where(p => p.nome.Equals(nomeEmpresa)).ToList() != null)
            //        throw new Exception("Já existe uma empresa com esse nome");
            //}

            var empresa = new Empresa { nome = model.Empresa, nomeGerente = usuario.nome, codEmpresa = Util.gerarChave() };
            empresa.listaUsuario = new List<Usuario>();
            empresa.listaUsuario.Add(usuario);

            Db.Empresa.Add(empresa);
            Db.SaveChanges();
            return empresa;
        }

        public GrupoUsuario recuperaGrupoUsuario(Usuario usuario)
        {
            var grupo = Db.GrupoUsuario.FirstOrDefault(g => g.nome.Equals("Administrador"));
            if (grupo.listaUsuario == null || grupo.listaUsuario.Count <= 0)
            {
                grupo.listaUsuario = new List<Usuario>();
            }
            grupo.listaUsuario.Add(usuario);
            grupo.empresaID = usuario.empresa.id;
            //Db.Empresa.Attach(grupo.empresa);

            Db.Entry(grupo).State = EntityState.Modified;
            Db.SaveChanges();

            return grupo;
        }
        public Usuario recuperaUsuarioEmSessao()
        {
            int id = (int)SessionControl.Id;
            return GetById(id);
        }

        public Empresa salvaEmpresaUsuario(Usuario user, CadastroInicialViewModel model)
        {
            user.empresa = Db.Empresa.FirstOrDefault(e => e.codEmpresa.Equals(model.codEmpresa));
            var empresa = user.empresa;
            if (empresa.listaUsuario != null || empresa.listaUsuario.Count <= 0)
            {
                empresa.listaUsuario.Add(user);
            }
            Db.Entry(empresa).State = EntityState.Modified;
            Db.SaveChanges();

            return empresa;
        }

        public IEnumerable<Usuario> recuperaListaUsuario()
        {
            EmpresaRepository repEmp = new EmpresaRepository();
            var usuario = recuperaUsuarioEmSessao();
            var empresa = repEmp.GetAll().FirstOrDefault(e => e.id.Equals(usuario.empresa.id));
            return empresa.listaUsuario;
        }

        public UsuariosViewModel UsuarioDV(long? pId)
        {
            var user = GetById((int)pId);
            UsuariosViewModel model = new UsuariosViewModel();
            UploadFileRepository repUpd = new UploadFileRepository();

            model.Id = user.id;
            model.Nome = user.nome;
            model.Apelido = user.apelido;
            model.Cargo = user.cargo;
            model.Email = user.email;
            model.Ativo = user.ativo;
            model.listaUsuario = null;
            model.listaGrupoIndexChange = (int)user.grupoUsuario.id;
            model.listaGrupo = repGrupo.ListaGrupoUsuarioEmpresa(user.empresa.id);
            model.ImagemCaminho = user.uploadImage != null ? repUpd.BuscarArquivoPorCaminho(user.uploadImage.caminhoServidor) : repUpd.BuscarArquivoPorCaminho(null);

            return model;
        }

        public UsuariosViewModel EditarUsuario(UsuariosViewModel model)
        {
            //var usuario = GetById((int)model.Id);
            var usuario = Db.Usuarios.FirstOrDefault(u => u.id == model.Id);
            usuario.nome = model.Nome;
            usuario.apelido = model.Apelido;
            usuario.cargo = model.Cargo;
            
            GrupoUsuarioRepository repGrupo = new GrupoUsuarioRepository();
            //var grupo = repGrupo.GetById(model.listaGrupoIndexChange);
            var grupo = Db.GrupoUsuario.FirstOrDefault(u => u.id == model.listaGrupoIndexChange);

            usuario.grupoUsuario.listaUsuario.Remove(usuario); //deleta usuario da lista de usuarios que fazem parte desse grupo.

            usuario.grupoUsuario = grupo; //atribui novo grupo desse usuário

            usuario.grupoUsuario.listaUsuario.Add(usuario); //atribui usuario a nova lista de usuário.
            Db.Entry(grupo).State = EntityState.Modified;
            Db.SaveChanges();

            Db.Entry(usuario).State = EntityState.Modified;
            Db.SaveChanges();

            return model = UsuarioDV(usuario.id);
        }

        public string ValidaEdicaoUsuario(UsuariosViewModel model)
        {
            if (string.IsNullOrEmpty(model.Nome))
            {
                return "Campo nome deve ser preenchido";
            }else if(string.IsNullOrEmpty(model.Apelido))
            {
                return "Campo apelido deve ser preenchido";
            }else if (string.IsNullOrEmpty(model.Cargo))
            {
                return "Campo cargo deve ser preenchido";
            }else
            {
                return string.Empty;
            }
            
        }
        public Usuario EditarPerfil(UsuariosViewModel model)
        {
            UploadFileRepository repoImage = new UploadFileRepository();

            var user = GetById((int)model.Id);
            user.nome = model.Nome;
            user.apelido = model.Apelido;
            user.cargo = model.Cargo;
            user.uploadImage = repoImage.recuperaArquivo(model.ImagemCaminho);

            Db.UploadFile.Attach(user.uploadImage);
            Db.Entry(user).State = EntityState.Modified;
            Db.SaveChanges();

            return user;
        }


    }
}