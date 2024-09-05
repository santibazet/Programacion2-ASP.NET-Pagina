using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioPruebaRazor.Modelos;
using ObligatorioWeb.Datos;
using ObligatorioWeb.Modelos;

namespace ObligatorioWeb.Pages.PaginaVehiculos
{
    public class EditarModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;
        public EditarModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        [BindProperty]
        public Vehiculo? Vehiculo { get; set; }

        //Obtiene un vehicula el cual posea el CN proporcionado por parametro
        public async Task OnGet(string CN)
        {
            Vehiculo = await _contexto.Vehiculo.FindAsync(CN);
        }

        //Metodo que edita los datos de un vehiculo
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var VehiculoDesdeDB = await
               _contexto.Vehiculo.FindAsync(Vehiculo.CN);//encuentra el vehiculo que se esta modificando
                if (VehiculoDesdeDB != null)
                {//Le modifica las propiedades del vehiculo asignandole las nuevas
                    VehiculoDesdeDB.Matricula = Vehiculo.Matricula;
                    VehiculoDesdeDB.Marca = Vehiculo.Marca;
                    VehiculoDesdeDB.Modelo = Vehiculo.Modelo;
                    VehiculoDesdeDB.Anio = Vehiculo.Anio;
                    await _contexto.SaveChangesAsync();
                    return RedirectToPage("IndexVehiculos");//Nos devuelve al index de vehiculos
                }
                else
                {
                    // Manejar el caso en que no se encuentre el curso en la base de datos
                    return NotFound();
                }
            }
            // ModelState no es válido, volver a mostrar la página con los errores
            return Page();
        }
    }
}
