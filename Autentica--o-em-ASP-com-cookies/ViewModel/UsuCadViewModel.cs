using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Compare = System.ComponentModel.DataAnnotations.CompareAttribute;
using System.Web.Mvc;

namespace AppAutenticacao.ViewModel
{
    public class UsuCadViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo \'nome\' é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Limite de 100 caracteres.")]
        public string UsuNome { get; set; }
        [Required(ErrorMessage = "O campo \'login\' é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O login deve conter no máximo 50 caracteres")]
        [Remote("SelectLogin", "Autenticacao", ErrorMessage = "O login já existe!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo \'senha\' é obrigatório.")]
        [MaxLength(100, ErrorMessage = "a senha deve conter no máximo 100 caracteres")]
        [MinLength(5, ErrorMessage = "a senha deve conter no mínimo 5 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Display(Name = "Confirme a senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas são diferentes")]
        public string ConfirmaSenha { get; set; }
    }
}