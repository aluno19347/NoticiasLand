using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoticiasLand.Models
{
    public class Fotos
    {
        [Key]
        public int FotoID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "A descrição não pode ter mais que 30 carácteres.")]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string FotoUrl { get; set; }

        [ForeignKey("Noticia")] // 1 para muitos
        public int NoticiaFK { get; set; }
        public virtual Noticias Noticia { get; set; }
    }
}