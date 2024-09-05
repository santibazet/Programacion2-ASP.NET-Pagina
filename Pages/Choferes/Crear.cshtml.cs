using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioWeb.Datos;
using ObligatorioWeb.Modelos;

namespace ObligatorioWeb.Pages.Choferes
{
    public class CrearModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;
        public CrearModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        [BindProperty]
        public Chofer? Chofer { get; set; }
        public void OnGet()
        {
        }

        //Agrega al chofer nuevo y luego guarda los cambios
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _contexto.Add(Chofer);//Agrega el chofer
            await _contexto.SaveChangesAsync();//guarda los cambios
            return RedirectToPage("Index");//Nos redirecciona al Index de choferes
        }
    }
}
