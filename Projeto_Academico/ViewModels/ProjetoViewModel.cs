using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Projeto_Academico.Models;
using Projeto_Academico.Business;
using Projeto_Academico.Business.Repository;

namespace Projeto_Academico.ViewModels
{
    public class NovoProjetoViewModel
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Informe nome do cliente", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Cliente")]
        [MaxLength(40), MinLength(3)]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Informe área de atuação", AllowEmptyStrings = false)]
        [Display(Name = "Área de atuação")]
        [MaxLength(40), MinLength(3)]
        public string AreaAtuacao { get; set; }

        [Display(Name = "Cliente")]
        public IEnumerable<Cliente> listaClientes { get; set; }

        [Required(ErrorMessage = "Informe nome do projeto", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Projeto")]
        [MaxLength(35), MinLength(3)]
        public string NomeProjeto { get; set; }
        public int IdUsuario { get; set; }
        public IEnumerable<Usuario> listaUsuarios { get; set; }
        public int IdCenario { get; set; }
        [Display(Name = "Cenário")]
        public IEnumerable<Cenario> listaCenarios { get; set; }

        public ProjetoViewModel projeto { get; set; }


    }

    public class ProjetoViewModel
    {
        public string NomeProjeto { get; set; }

        public virtual IEnumerable<Fila> ListaFilas { get; set; }

        public int IdFila { get; set; }

        [Required(ErrorMessage = "Informe nome da tarefa", AllowEmptyStrings = false)]
        [Display(Name = "Descrição sucinta")]
        [MaxLength(40), MinLength(3)]
        public string NomeTarefa { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataVencimento { get; set; }

        [Display(Name = "Quem pode fazer isso?")]
        public virtual IEnumerable<Fila> ListaUsuarios { get; set; }
        public int IdUsuario { get; set; }

        public string processo { get; set; }

        public long Tipo { get; set; }

        public ICollection<Tarefa> ListaTarefa(long pIdFila)
        {
            TarefaRepository repTask = new TarefaRepository();
            return repTask.ListaTarefas(pIdFila).ToList();
        }
        public ICollection<Tarefa> ListaTarefaProcs(long pIdProcs)
        {
            TarefaRepository repTask = new TarefaRepository();
            return repTask.ListaTarefasProcs(pIdProcs).ToList();
        }
        public FilaViewModel fila { get; set; }

        public CriarNovoProcessoViewModel NovoProcesso { get; set; }

        public virtual IEnumerable<Processo> ListaProcessos { get; set; }

        public TarefaViewModel tarefa { get; set; }
    }

    public class TarefaViewModel
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Descricao detalhada")]
        public string Descricao { get; set; }

        public virtual ICollection<Observacoes> Observacoes { get; set; }

        public virtual ICollection<Usuario> ListaUsuarios { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy hh:mm}")]
        public DateTime DataInicio { get; set; }

        public DateTime DataEntrega { get; set; }

        public string TempoTotal { get; set; }
        public virtual ICollection<UploadFile> ListaArquivos { get; set; }
        public virtual ICollection<Observacoes> ListaObservacoes { get; set; }
        public virtual Fila Fila { get; set; }
        public virtual Processo Processo { get; set; }

        [Display(Name = "Responsável")]
        public long UsuarioID { get; set; }

        [Display(Name = "Processo")]
        public long ProcessoID { get; set; }

        public string Observacao { get; set; }

        public long tipoProjeto { get; set; }
    }

    public class FilaViewModel
    {
        [Required(ErrorMessage = "Informe nome da fila", AllowEmptyStrings = false)]
        [Display(Name = "Nome da fila de tarefas")]
        [MaxLength(25), MinLength(3)]
        public String Nome { get; set; }

        public long idProjeto { get; set; }

        public ProjetoViewModel projeto { get; set; }
    }

    public class ProcessoViewModel
    {
        public long IdProcesso { get; set; }
        public String Nome { get; set; }
        public long IdProjeto { get; set; }
        public virtual IEnumerable<Tarefa> ListaTarefas { get; set; }
    }

    public class CriarNovoProcessoViewModel
    {
        [Required(ErrorMessage = "Informe nome do processo", AllowEmptyStrings = false)]
        [Display(Name = "Nome do processo")]
        [MaxLength(25), MinLength(3)]
        public String Nome { get; set; }
    }

}