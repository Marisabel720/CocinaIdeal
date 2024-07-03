using CocinaIdeal.DTo;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CocinaIdeal.Models
{
    public class Cocina
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required]
        public int Id { get; set; }
        //[Required]
        public int CantidadStock { get; set; }
        //[Required]
        public string? Marca { get; set; }
        //[Required]
        public string? Modelo { get; set; }
        public string? ImagenCocina { get; set; }
       // [Required]
        public decimal Precio { get; set; }
       /* [Required]
        public DateTime FechaDeLanzamiento { get; set; }
       */


        //para archivos (foto)
        [NotMapped]
        [Display (Name = "Cargar Foto")]
        public IFormFile? ProductoFile { get; set; }


        //relaciones: 1--->*
        public virtual List<Venta>? Venta { get; set; }
    }
}
