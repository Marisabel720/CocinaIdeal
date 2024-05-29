using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CocinaIdeal.Models
{
    public class Cliente
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Ci { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public int Celular { get; set; }

        //relaciones: 1--->*
        public virtual List<Venta>? Ventas { get; set; }
    }

}
