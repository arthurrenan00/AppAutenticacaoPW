using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAutenticacao.Classes
{
    public class Usuario
    {
        public string Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Login { get; set; }
        [Required, MaxLength(100)]
        public string Senha { get; set; }
    }
}