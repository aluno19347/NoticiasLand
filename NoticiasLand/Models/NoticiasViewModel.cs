using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoticiasLand.Models
{
    public class NoticiasViewModel
    {
        public int NoticiaID { get; set; }

        [Display(Name = "Url da Notícia")]
        [Required(ErrorMessage = "O {0} é necessário.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "O {0} tem de ter no mínimo {2} e no máximo {1}.")]
        [RegularExpression("^[a-z][-a-z0-9]*",
            ErrorMessage = "O {0} começa, obrigatoriamente, por uma letra, só são permitidas letras minúsculas, números e hífens.")]
        public virtual string UrlSlug { get; set; }

        [Display(Name = "Meta Descrição")]
        [Required(ErrorMessage = "A {0} é necessário.")]
        public string Meta { get; set; } // meta-description

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "O {0} é necessário.")]
        public string Titulo { get; set; }

        [Display(Name = "Subtitulo")]
        public string Subtitulo { get; set; }

        [Display(Name = "Corpo da Noticia")]
        [Required(ErrorMessage = "O {0} é necessário.")]
        [DataType(DataType.MultilineText)]
        public string Texto { get; set; }

        public virtual Categorias Categoria { get; set; }

        public ICollection<Fotos> ListaDeFotos { get; set; } // 1 para muitos

        public ICollection<Tags> ListaDeTags { get; set; } // muitos para muitos
    }
}