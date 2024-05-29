using CocinaIdeal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CocinaIdeal.Contexto
{   
    public class MiContexto : DbContext
    {
        public MiContexto(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Cocina> Cocinas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }

}
