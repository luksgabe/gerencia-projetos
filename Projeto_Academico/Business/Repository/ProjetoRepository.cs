using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using Projeto_Academico.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_Academico.Business.Repository
{
    public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
    {
        ClienteRepository repCliente = new ClienteRepository();
        UsuarioRepository repUsuario = new UsuarioRepository();
        ProcessoRepository repProcess = new ProcessoRepository();

        public Projeto NovoProjeto(NovoProjetoViewModel model)
        {

            var cliente = repCliente.GetById(model.IdCliente);
            var responsavel = repUsuario.GetById(model.IdUsuario);
            repCliente.Dispose();
            repUsuario.Dispose();

            var projeto = new Projeto
            {
                nome = model.NomeProjeto,
                responsavelProjeto = responsavel.nome,
                ativo = true,
                cliente = cliente,
                tipo = model.IdCenario,
                listaUsuariosEnvolvidos = new List<Usuario>(),
                listaFilas = new List<Fila>(),
                listaProcessos = new List<Processo>()
            };

            Db.Usuarios.Attach(responsavel);
            Db.Cliente.Attach(projeto.cliente);

            projeto.listaUsuariosEnvolvidos.Add(responsavel);

            Db.Projeto.Add(projeto);
            Db.SaveChanges();

            VerificaCenarios(projeto);

            responsavel.listaProjetosEnvolvidos = new List<Projeto>();
            responsavel.listaProjetosEnvolvidos.Add(projeto);

            Db.Entry(responsavel).State = EntityState.Modified;
            Db.SaveChanges();

            IncluirProcessoProjeto(projeto);

            return projeto;
        }

        public Projeto RecuperaProjetoSessao()
        {
            int id = Convert.ToInt16(SessionControl.ProjetoId);
            return GetById(id);
        }



        #region Metodos Auxiliares
        public static SelectList populaDdlUsuario()
        {
            UsuarioRepository repUser = new UsuarioRepository();
            var usuario = repUser.recuperaUsuarioEmSessao();

            IList<Usuario> listUsuario = usuario.empresa.listaUsuario.ToList();

            return new SelectList(listUsuario, "id", "apelido");
        }

        public static SelectList populaDdlProcesso()
        {
            ProjetoRepository repPro = new ProjetoRepository();

            var projeto = repPro.RecuperaProjetoSessao();

            IList<Processo> listProcesso = projeto.listaProcessos.ToList();

            return new SelectList(listProcesso, "id", "nome");
        }


        public static SelectList populaDdlCenarios()
        {
            var listCenario = Cenario.listCenario();
            return new SelectList(listCenario, "id", "nome");
        }

        public static SelectList populaMultDdlUsuarios(string idProjeto)
        {
            Projeto_AcademicoContext db = new Projeto_AcademicoContext();
            UsuarioRepository repUser = new UsuarioRepository();

            long id = Convert.ToInt64(idProjeto);

            var usuario = repUser.recuperaUsuarioEmSessao();
            var projeto = db.Projeto.FirstOrDefault(p => p.id.Equals(id));

            var listUsers = usuario.empresa.listaUsuario.ToList();//usuarios da empresa

            return new SelectList(listUsers, "id", "apelido"); ;
        }


        public void VerificaCenarios(Projeto pProjeto)
        {
            if (pProjeto.tipo == 0)//processos de projetos com cenário de certeza
            {
                #region Processo Requisitos
                var fila = new Fila
                {
                    nome = "Requisitos",
                    projeto = pProjeto,
                    listaTarefas = new List<Tarefa>(),
                    ativo = true
                };
                Db.Projeto.Attach(fila.projeto);
                Db.Fila.Add(fila);
                Db.SaveChanges();

                pProjeto.listaFilas.Add(fila);
                #endregion

                #region Processo Análise
                fila = new Fila
                {
                    nome = "Análise",
                    projeto = pProjeto,
                    listaTarefas = new List<Tarefa>(),
                    ativo = true
                };
                Db.Projeto.Attach(fila.projeto);
                Db.Fila.Add(fila);
                Db.SaveChanges();

                pProjeto.listaFilas.Add(fila);
                #endregion

                #region Processo Projeto
                fila = new Fila
                {
                    nome = "Projeto",
                    projeto = pProjeto,
                    listaTarefas = new List<Tarefa>(),
                    ativo = true
                };
                Db.Projeto.Attach(fila.projeto);
                Db.Fila.Add(fila);
                Db.SaveChanges();

                pProjeto.listaFilas.Add(fila);
                #endregion

                #region Processo Implementação
                fila = new Fila
                {
                    nome = "Implementação",
                    projeto = pProjeto,
                    listaTarefas = new List<Tarefa>(),
                    ativo = true
                };
                Db.Projeto.Attach(fila.projeto);
                Db.Fila.Add(fila);
                Db.SaveChanges();

                pProjeto.listaFilas.Add(fila);

                #endregion

                #region Processo Teste
                fila = new Fila
                {
                    nome = "Teste",
                    projeto = pProjeto,
                    listaTarefas = new List<Tarefa>(),
                    ativo = true
                };
                Db.Projeto.Attach(fila.projeto);
                Db.Fila.Add(fila);
                Db.SaveChanges();

                pProjeto.listaFilas.Add(fila);

                #endregion

                #region Processo Manutenção

                fila = new Fila
                {
                    nome = "Manutenção",
                    projeto = pProjeto,
                    listaTarefas = new List<Tarefa>(),
                    ativo = true
                };
                Db.Projeto.Attach(fila.projeto);
                Db.Fila.Add(fila);
                Db.SaveChanges();

                pProjeto.listaFilas.Add(fila);
                #endregion


                Db.Entry(pProjeto).State = EntityState.Modified;
                Db.SaveChanges();
            }
        }

        public String IncluirUsuario(int[] IdUsuario, int idProjeto)
        {

            if (IdUsuario.Count() < 0)
                throw new Exception("Nenhum usuário selecionado.");

            var projeto = Db.Projeto.FirstOrDefault(p => p.id.Equals(idProjeto));
            var usuario = new Usuario();
            string mensagem = string.Empty;

            for (int i = 0; i < IdUsuario.Length; i++)
            {
                long id = (long)IdUsuario[i];

                usuario = projeto.listaUsuariosEnvolvidos.FirstOrDefault(p => p.id.Equals(id));
                if (usuario != null)
                {
                    mensagem += "Usuário " + usuario.apelido + " já está envolvido no projeto; ";
                }
                else
                {
                    usuario = Db.Usuarios.FirstOrDefault(u => u.id == id);
                    if (usuario != null)
                    {
                        projeto.listaUsuariosEnvolvidos.Add(usuario);
                        usuario.listaProjetosEnvolvidos.Add(projeto);

                        Db.Entry(projeto).State = EntityState.Modified;
                        Db.SaveChanges();
                        Db.Entry(usuario).State = EntityState.Modified;
                        Db.SaveChanges();
                    }
                }
            }

            return mensagem;
        }

        public void IncluirProcessoProjeto(Projeto projeto)
        {
            var processo = new Processo
            {
                nome = "BackLog",
                listaTarefas = new List<Tarefa>(),
                ativo = true,
                projeto = projeto
            };

            Db.Projeto.Attach(processo.projeto);
            Db.Processo.Add(processo);
            Db.SaveChanges();

            projeto.listaProcessos.Add(processo);

            Db.Entry(projeto).State = EntityState.Modified;
            Db.SaveChanges();
        }

        #endregion
    }
}