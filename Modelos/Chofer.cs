using System.ComponentModel.DataAnnotations;

namespace ObligatorioWeb.Modelos
{
    public class Chofer
    {
        [Key]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Numero identificatorio")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El campo {0} debe tener exactamente 8 dígitos.")]
        public string? Ci { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Apellido { get; set; }

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(18, int.MaxValue, ErrorMessage = "El chofer debe tener al menos a 18 años.")]
        public int? Edad { get; set; }

        [Display(Name = "Vehiculo Asignado")]
        public string? Vehiculo { get; set; } = "Sin asignar";
    }
}
