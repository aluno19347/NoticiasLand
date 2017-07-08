using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoticiasLand.Models
{
    public class Logins
    {
        [Key]
        [StringLength(16, ErrorMessage = "Username não pode ser maior que 16 carácteres.")]
        [RegularExpression(@"[a-zA-Z''-'\s]*$")]
        public string Username { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public decimal Nivel { get; set; }
    }
}