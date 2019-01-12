using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_Academico.Business.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {

        public IEnumerable<Cliente> ListaClientes()
        {
            return GetAll();
        }

        public Cliente SalvarCliente(Cliente cliente)
        {
            UsuarioRepository repUser = new UsuarioRepository();
            var usuario = repUser.recuperaUsuarioEmSessao();

            cliente.empresa = usuario.empresa;
            repUser.Dispose();

            cliente.listaProjeto = new List<Projeto>();
            cliente.ativo = true;

            Db.Empresa.Attach(cliente.empresa);
            Db.Cliente.Add(cliente);
            Db.SaveChanges();

            if (usuario.empresa.listaCliente.Count > 0 || usuario.empresa.listaCliente != null)
            {
                usuario.empresa.listaCliente.Add(cliente);
            }
            else
            {
                usuario.empresa.listaCliente = new List<Cliente>();
                usuario.empresa.listaCliente.Add(cliente);
            }

            Db.Entry(usuario.empresa).State = EntityState.Modified;
            Db.SaveChanges();

            return cliente;
        }

        #region Metodos Auxiliares

        public static Boolean verificaClientes()
        {
            ClienteRepository repCli = new ClienteRepository();
            var listClient = repCli.ListaClientes().ToList();

            if (listClient.Count > 0 && listClient != null)
                return true;
            else
                return false;
        }

        public static SelectList populaDdlCliente()
        {
            UsuarioRepository repUser = new UsuarioRepository();
            var usuario = repUser.recuperaUsuarioEmSessao();

            IList<Cliente> listCliente = usuario.empresa.listaCliente.ToList();

            return new SelectList(listCliente, "id", "nome");
        }

        #endregion

    }
}