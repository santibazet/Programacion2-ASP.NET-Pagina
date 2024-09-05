using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioPruebaRazor.Modelos;
using ObligatorioWeb.Datos;
using ObligatorioWeb.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioWeb.Pages.Choferes
{
    public class EditarModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;
        public EditarModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Chofer? Chofer { get; set; }
        public List<Vehiculo> VehiculosDisponibles { get; set; }

        //obtiene un chofer que poseea el Ci que se envia por parametro y obtiene la lista de vehiculos de la BD
        public async Task OnGet(string Ci)
        {
            Chofer = await _contexto.Chofer.FindAsync(Ci);//Obtiene el chofer que poseea el CN proporcionado
            VehiculosDisponibles = await _contexto.Vehiculo.ToListAsync();//Obtiene una lista de vehiculos
        }

        //Metodo de asignacion de vehiculo a chofer
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                // Validar si la matrícula ya está asignada a otro chofer
                if (Chofer.Vehiculo != "Sin asignar" && await _contexto.Chofer.AnyAsync(c => c.Vehiculo == Chofer.Vehiculo && c.Ci != Chofer.Ci))
                {
                    var otroChofer = await _contexto.Chofer.FirstOrDefaultAsync(c => c.Vehiculo == Chofer.Vehiculo && c.Ci != Chofer.Ci);
                    var mensajeError = $"El vehículo {Chofer.Vehiculo} ya está asignado al chofer {otroChofer.Nombre} ({otroChofer.Ci}).";
                    ModelState.AddModelError("Chofer.Vehiculo", mensajeError);
                    VehiculosDisponibles = await _contexto.Vehiculo.ToListAsync();
                    return Page();
                }


                var choferDesdeDB = await _contexto.Chofer.FindAsync(Chofer.Ci);//Encuentra un chofer que posea el CI porporiconado
                if (choferDesdeDB != null)
                {//Cambia sus propiedades antiguas por las nuevas
                    choferDesdeDB.Nombre = Chofer.Nombre;
                    choferDesdeDB.Apellido = Chofer.Apellido;
                    choferDesdeDB.Edad = Chofer.Edad;

                    if (Chofer.Vehiculo == "Sin asignar")
                    {
                        var vehiculoAsignado = await _contexto.Vehiculo.FirstOrDefaultAsync(v => v.Matricula == choferDesdeDB.Vehiculo);
                        if (vehiculoAsignado != null)
                        {
                            vehiculoAsignado.Estado = "No asignado"; // Cambiar estado a "No asignado"
                        }
                        choferDesdeDB.Vehiculo = "Sin asignar"; // Asignar "Sin asignar" al chofer
                        if (vehiculoAsignado?.Estado == "No asignado" && choferDesdeDB.Vehiculo == "Sin asignar")
                        {
                            vehiculoAsignado.Libre = true;
                        }
                    }
                    else
                    {
                        var vehiculoNuevo = await _contexto.Vehiculo.FirstOrDefaultAsync(v => v.Matricula == Chofer.Vehiculo);
                        if (vehiculoNuevo != null)
                        {
                            var vehiculoActual = await _contexto.Vehiculo.FirstOrDefaultAsync(v => v.Matricula == choferDesdeDB.Vehiculo);
                            if (vehiculoActual != null)
                            {
                                vehiculoActual.Estado = "No asignado"; // Cambiar estado de la matrícula anterior a "No asignado"
                            }
                            vehiculoNuevo.Estado = "Asignado"; // Actualizar estado de la nueva matrícula a "Asignado"
                            choferDesdeDB.Vehiculo = Chofer.Vehiculo;
                            vehiculoNuevo.Libre = false;
                        }
                    }

                    await _contexto.SaveChangesAsync();//guarda los cambios
                    return RedirectToPage("Index");
                }
                else
                {
                    // Manejar el caso en que no se encuentre el chofer en la base de datos
                    return NotFound();
                }
            }
            // ModelState no es válido, volver a mostrar la página con los errores
            return Page();
        }
    }
}
