using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CocinaIdeal.Models
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public int NumeroRecibo { get; set; }
        [Required]
        public DateTime FechaCompra { get; set; }
        [Required]
        public bool EsPagado { get; set; }
        [Required]
        public int NumeroTicketReserva { get; set; }
        [Required]
        public decimal Precio { get; set; }

        //Relaciones de *--->1
        public int UsuarioId { get; set; }
        public int CocinaId { get; set; }
        public int ClienteId { get; set; }

        public virtual Usuario? Usuario { get; set; }
        public virtual Cocina? Cocina { get; set; }
        public virtual Cliente? Cliente { get; set; }

    }
}
