using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace NoticiasLand.Models
{
    public class Categorias
    {
        [Key]
        public int CategoriaID{ get; set; }

        [Display(Name = "Nome da Categoria")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "O {0} tem de ter no mínimo {2} e no máximo {1}.")]
        [Required(ErrorMessage = ("O {0} é necessário."))]
        [RegularExpression("[A-ZÍÂÓ0-9][a-záéíóúàèìòùâêîôûãõäëïöüç0-9']+((-| )?[A-ZÍÂÓa-záéíóúàèìòùâêîôûãõäëïöüç0-9']+)*",
         ErrorMessage = "{0} Invalido")]
        public string Nome{ get; set; }

        [Display(Name = "Url da Categoria")]
        [Required(ErrorMessage = "O {0} é necessário.")]
        [Index(IsUnique = true)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "O {0} tem de ter no mínimo {2} e no máximo {1}.")]
        [RegularExpression("^[a-z][-a-z0-9]*",
         ErrorMessage = "O {0} começa, obrigatoriamente, por uma letra, só são permitidas letras minúsculas, números e hífens.")]
        public string UrlSlug{ get; set; }

        public ICollection<Noticias> ListaDeNoticias { get; set; }

        public Categorias()
        {
            ListaDeNoticias = new HashSet<Noticias>();
        }
    }
}