using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Projeto_Academico.Models;

namespace Projeto_Academico.Models
{
    public class Empresa
    {
        public long id { get; set; }
        public Guid codEmpresa { get; set; }
        public string nome { get; set; }
        public string nomeGerente { get; set; }
        public virtual ICollection<Usuario> listaUsuario { get; set; }
        public virtual ICollection<Cliente> listaCliente { get; set; }
    }

    public class Usuario
    {
        public long id { get; set; }
        public string nome { get; set; }
        public string apelido { get; set; }
        public string cargo { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public bool verificado { get; set; }
        public bool ativo { get; set; }
        public virtual GrupoUsuario grupoUsuario { get; set; }
        public virtual Empresa empresa { get; set; }
        public virtual UploadFile uploadImage { get; set; }
        public virtual ICollection<Projeto> listaProjetosEnvolvidos { get; set; }
        public virtual ICollection<Tarefa> listaTarefas { get; set; }
        public virtual ICollection<Observacoes> listaObservacoes { get; set; }
    }
    public class Ativacao
    {
        public long id { get; set; }
        public string codigo { get; set; }
        public DateTime dataAtivacao { get; set; }
        public virtual Usuario usuario { get; set; }
    }

    public class GrupoUsuario
    {
        public long id { get; set; }
        public string nome { get; set; }
        public bool ativo { get; set; }
        public long empresaID { get; set; }
        public virtual ICollection<Usuario> listaUsuario { get; set; }
        public virtual ICollection<Tela> listaPermissoesTela { get; set; }
    }
    public class Tela
    {
        public long id { get; set; }
        public string controller { get; set; }
        public string nome { get; set; }
        public virtual ICollection<GrupoUsuario> gruposPermitidos { get; set; }
    }

    public class UploadFile
    {
        public long id { get; set; }
        public string nome { get; set; }
        public int tamanho { get; set; }
        public int peso { get; set; }
        public string tipo { get; set; }
        public string caminho { get; set; } = null;
        public string caminhoServidor { get; set; }
    }

    public class Cliente
    {
        public long id { get; set; }
        public string nome { get; set; }
        public string areaAtuacao { get; set; }
        public virtual Empresa empresa { get; set; }
        public virtual ICollection<Projeto> listaProjeto { get; set; }
        public bool ativo { get; set; }
    }

    public class Projeto
    {
        public long id { get; set; }
        public string nome { get; set; }
        public string responsavelProjeto { get; set; }
        public virtual Cliente cliente { get; set; }
        public virtual ICollection<Usuario> listaUsuariosEnvolvidos { get; set; }
        public virtual ICollection<Fila> listaFilas { get; set; }
        public virtual ICollection<Processo> listaProcessos { get; set; }
        public bool ativo { get; set; }
        public Int32 tipo { get; set; }
    }

    public class Fila
    {
        public long id { get; set; }
        public string nome { get; set; }
        public virtual Projeto projeto { get; set; }
        public virtual ICollection<Tarefa> listaTarefas { get; set; }
        public bool ativo { get; set; }
    }

    public class Processo
    {
        public long id { get; set; }
        public string nome { get; set; }
        public virtual Projeto projeto { get; set; }
        public virtual ICollection<Tarefa> listaTarefas { get; set; }
        public bool ativo { get; set; }
    }

    public class Tarefa
    {
        public long id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public virtual ICollection<Usuario> listaUsuarios { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataEntrega { get; set; }
        public string tempoTotal { get; set; }
        public virtual ICollection<UploadFile> listaArquivos { get; set; }
        public virtual ICollection<Observacoes> listaObservacoes { get; set; }
        public virtual Fila fila { get; set; }
        public virtual Processo processo { get; set; }
        public bool ativo { get; set; }

    }

    public class Observacoes
    {
        public long id { get; set; }
        public string comentario { get; set; }
        public virtual Usuario usuario { get; set; }
        public virtual Tarefa tarefa { get; set; }
        public DateTime dataCriacao { get; set; }

    }


    //public class Lembrete
    //{
    //    public long id { get; set; }
    //    public DateTime dataHora { get; set; }
    //    public string nomeDestinatario { get; set; }
    //    public string mensagem { get; set; }
    //}

}