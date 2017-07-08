using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoticiasLand.Models
{
    public class Noticias
    {
        [Key]
        public int NoticiaID { get; set; }

        [ForeignKey("Utilizador")] // 1 para 1
        public int UtilizadorFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(30)]
        public string UrlSlug{ get; set; }

        [Required]
        public string Meta { get; set; } // meta-description

        [Required]
        public string Titulo { get; set; }

        public string Subtitulo { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [ForeignKey("Categoria")] // 1 para 1
        public int CategoriaFK{ get; set; }
        public virtual Categorias Categoria { get; set; }

        [Required]
        public string Texto { get; set; }

        public int Likes { get; set; }

        public ICollection<Fotos> ListaDeFotos { get; set; } // 1 para muitos

        public ICollection<Tags> ListaDeTags { get; set; } // muitos para muitos

        public Noticias()
        {
            ListaDeTags = new HashSet<Tags>();
            ListaDeFotos = new HashSet<Fotos>();
        }
    }
}