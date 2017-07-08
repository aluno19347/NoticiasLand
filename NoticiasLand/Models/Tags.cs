using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoticiasLand.Models
{
    public class Tags
    {
        [Key]
        public int TagID { get; set; }

        [Display(Name = "Nome da Tag")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "O {0} tem de ter no mínimo {2} e no máximo {1}.")]
        [Required(ErrorMessage = ("O {0} é necessário."))]
        [RegularExpression("[A-ZÍÂÓ0-9][a-záéíóúàèìòùâêîôûãõäëïöüç0-9']+((-| )?[A-ZÍÂÓa-záéíóúàèìòùâêîôûãõäëïöüç0-9']+)*",
         ErrorMessage = "{0} Invalido")]
        public string Nome { get; set; }

        [Display(Name = "Url da Tag")]
        [Required(ErrorMessage = "O {0} é necessário.")]
        [Index(IsUnique = true)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "O {0} tem de ter no mínimo {2} e no máximo {1}.")]
        [RegularExpression("^[a-z][-a-z0-9]*",
 ErrorMessage = "O {0} começa, obrigatoriamente, por uma letra, só são permitidas letras minúsculas, números e hífens.")]
        public virtual string UrlSlug { get; set; }

        public ICollection<Noticias> ListaDeNoticias { get; set; } // muitos para muitos

        public Tags()
        {
            ListaDeNoticias = new HashSet<Noticias>();
        }
    }
}