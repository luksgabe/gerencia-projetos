using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Models.ViewModels
{
    public class InicioViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
    }


    public class CadastroInicialViewModel
    {
        [Required(ErrorMessage = "Informe nome completo", AllowEmptyStrings = false)]
        [Display(Name = "Nome completo")]
        [MaxLength(40), MinLength(5)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Apelido")]
        [MaxLength(40), MinLength(5)]
        public string Apelido { get; set; }

        [Required]
        [Display(Name = "Nome da Empresa")]
        [MaxLength(40), MinLength(5)]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "Informe E-mail", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "O/A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha do usuário", AllowEmptyStrings = false)]
        public string Senha { get; set; }

        [MaxLength(15)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha" , ErrorMessage = "A senha e confirmação de senha não coincidem")]
        [Required(ErrorMessage = "Informe a senha do usuário", AllowEmptyStrings = false)]
        public string ConfirmarSenha { get; set; }
        public bool convidado { get; set; }
        public Guid codEmpresa { get; set; }


    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe E-mail", AllowEmptyStrings = false)]
        [Display(Name = "Login")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha do usuário", AllowEmptyStrings = false)]
        public string Senha { get; set; }
    }

}