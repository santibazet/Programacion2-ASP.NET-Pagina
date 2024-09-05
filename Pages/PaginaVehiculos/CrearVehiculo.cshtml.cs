using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioPruebaRazor.Modelos;
using ObligatorioWeb.Datos;
using ObligatorioWeb.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioWeb.Pages.PaginaVehiculos
{
    public class CrearVehiculoModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CrearVehiculoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vehiculo? Vehiculos { get; set; }
        
        public void OnGet()
        {
        }

        //Crea un vehiculo y lo guarda en la BD
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
                _context.Add(Vehiculos);
                await _context.SaveChangesAsync();
                return RedirectToPage("IndexVehiculos");
            
        }
    }
}
