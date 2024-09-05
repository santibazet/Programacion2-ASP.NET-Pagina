using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ObligatorioPruebaRazor.Modelos;
using ObligatorioWeb.Datos;
using ObligatorioWeb.Modelos;
using System;

namespace ObligatorioWeb.Pages.PaginaVehiculos
{
    public class IndexVehiculosModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;
        public IndexVehiculosModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Vehiculo> Vehiculos { get; set; }
        [BindProperty] //Tambien en el indes se agrego un dropdown desde la linea 18 a la 29
        public string listado { get; set; } //variable de la que depende el filtrado, si se cambia el valor
                                                      //a Asignado, todos, No asignado, Antiguo cambia el filtrado
        public List<Vehiculo> filtrados { get; set; }//Lista donde se ingresan los valores filtrados
        public Vehiculo MasAntiguo { get; set; }//vehiculo mas antiguo





        //Cuando se cambia de opcion en el select ejecuta el codigo
        public async Task OnPost(string accion)
        {//verifica que opcion del dropdown esta seleccionada y le cambia el valor a listado
            switch (accion)
            {
                case "todos":
                    listado = "Todos";
                    break;
                case "Asignado":
                    listado = "Vehículos asignados a un Chofer";

                    break;
                case "Antiguo":
                    listado = "Vehículo más antiguo";

                    break;
                case "No asignado":
                    listado = "Vehículos sin asignar a un Chofer";
                    break;

            }

            Vehiculos = await _contexto.Vehiculo.ToListAsync();//Obtiene una lista de vehiculos de BD
            MasAntiguo = await _contexto.ObtenerVehiculoAntiguo();//Obtiene el vehiculo mas viejo que hay en la BD
            //Dependindo del valor de listado, filtrara de diferentes formas la lista de vehiculos
            if (listado == "Todos")
            {
                filtrados = Vehiculos.ToList();//sin filtros
            }
            else if (listado == "Vehículos sin asignar a un Chofer")
            {
                filtrados = Vehiculos.Where(v => v.Estado == "No asignado").ToList();//los que no estan asignados
            }
            else if (listado == "Vehículos asignados a un Chofer")
            {
                filtrados = Vehiculos.Where(v => v.Estado == "Asignado").ToList();//Los que estan asignados
            }
            else if (listado == "Vehículo más antiguo")//El mas antiguo
            {
                filtrados = new List<Vehiculo>();
                if (MasAntiguo != null)
                {
                    filtrados.Add(MasAntiguo);
                }
            }

        }


        //Funcion que inicia con la pagina, obtiene una lista de vehiclos y lo asigna a la lista filtrados
        public async Task OnGet()
        {
            Vehiculos = await _contexto.Vehiculo.ToListAsync();
            filtrados = Vehiculos.ToList();

        }
        

        //Funcion del boton borrar de vehiculos
        public async Task<IActionResult> OnPostBorrar(string CN)
        {
            var vehiculo = await _contexto.Vehiculo.FindAsync(CN);//Obtiene el vehiculo que poseea el CN
            if (vehiculo == null)
            {
                return NotFound();
            }
            _contexto.Vehiculo.Remove(vehiculo);//Lo elimina de la BD
            await _contexto.SaveChangesAsync();//Guarda los cambios
            return RedirectToPage();
        }
    }
}