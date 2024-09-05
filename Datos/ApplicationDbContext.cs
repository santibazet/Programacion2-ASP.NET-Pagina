using Microsoft.EntityFrameworkCore;
using ObligatorioPruebaRazor.Modelos;
using ObligatorioWeb.Modelos;

namespace ObligatorioWeb.Datos
{
    public class ApplicationDbContext : DbContext 
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        public DbSet<Chofer> Chofer { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set;}

        internal async Task<Vehiculo> ObtenerVehiculoAntiguo()
        {
            return await Vehiculo.OrderBy(c => c.Anio).FirstOrDefaultAsync();
        }
    }
}
