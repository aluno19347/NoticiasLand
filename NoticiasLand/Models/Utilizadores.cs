using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoticiasLand.Models
{
    public class Utilizadores
    {
        [Key]
        public int UtilizadorID { get; set; }

        [Required(ErrorMessage = "O {0} é necessário.")]
        [Display(Name = "Nome de Utilizador")]
        [StringLength(50, ErrorMessage = "O nome não pode ter mais que 50 carácteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é necessário.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        public string Foto { get; set; }

        public ICollection<Noticias> ListaDeNoticias { get; set; } // 1 para muitos

        public Utilizadores()
        {
            ListaDeNoticias = new HashSet<Noticias>();
        }
    }
}