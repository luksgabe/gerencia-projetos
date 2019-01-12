using Projeto_Academico.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projeto_Academico.ViewModels
{
    public class ConvidarViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        [MaxLength(40), MinLength(5)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
    }

    public class UsuariosViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Informe Nome de usuario", AllowEmptyStrings = false)]
        [MaxLength(30), MinLength(5)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Apelido", AllowEmptyStrings = false)]
        [MaxLength(15), MinLength(5)]
        [Display(Name = "Apelido")]
        public string Apelido { get; set; }


        [Display(Name = "Cargo")]
        [MaxLength(30)]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Informe E-mail", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Status")]
        public bool Ativo { get; set; }
        public string ImagemCaminho { get; set; }

        [Display(Name = "Perfil")]
        public IEnumerable<GrupoUsuario> listaGrupo { get; set; }
        public IEnumerable<Usuario> listaUsuario { get; set; }
        public int listaGrupoIndexChange { get; set; }

    }


}