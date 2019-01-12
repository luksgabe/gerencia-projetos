using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Academico.Business.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {

        IEnumerable<Cliente> ListaClientes();

        Cliente SalvarCliente(Cliente cliente);
    }
}
