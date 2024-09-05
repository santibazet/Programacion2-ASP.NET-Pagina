using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioWeb.Datos;
using ObligatorioWeb.Modelos;

namespace ObligatorioWeb.Pages.Choferes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;
        public IndexModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IEnumerable<Chofer> Choferes { get; set; }
        
        //Obtiene una lista de choferes de la BD
        public async Task OnGet()
        {
            Choferes = await _contexto.Chofer.ToListAsync();
        }

        //Metodo relacionado al boton de borrar
        public async Task<IActionResult> OnPostBorrarAsync(string Ci)
        {
            var chofer = await _contexto.Chofer.FindAsync(Ci);
            if (chofer == null)
            {
                return NotFound();
            }

            // Buscar el vehículo vinculado al chofer a eliminar
            var vehiculoAsignado = await _contexto.Vehiculo.FirstOrDefaultAsync(v => v.Matricula == chofer.Vehiculo);

            // Si se encuentra el vehículo, actualizar su estado a "No asignado"
            if (vehiculoAsignado != null)
            {
                vehiculoAsignado.Estado = "No asignado";
            }

            _contexto.Chofer.Remove(chofer);//Elimina al chofer seleccionado
            await _contexto.SaveChangesAsync();//guarda los cambios

            return RedirectToPage();
        }

    }
}
