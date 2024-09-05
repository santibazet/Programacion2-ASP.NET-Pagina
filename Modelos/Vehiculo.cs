using System.ComponentModel.DataAnnotations;

namespace ObligatorioPruebaRazor.Modelos
{
    public class Vehiculo
    {
        [Key]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Numero identificatorio")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El campo {0} debe tener exactamente 5 dígitos.")]
        public string? CN { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Numero de matrícula")]
        [RegularExpression(@"^\d{3} [A-Za-z]{4}$", ErrorMessage = "El campo {0} debe tener el formato correcto: 3 números seguidos de un espacio y 4 letras.")]
        public string? Matricula { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Marca")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Modelo")]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Año")]
        [Range(1900, 2023, ErrorMessage = "El campo {0} debe estar entre 1900 y el año actual.")]
        public int Anio { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; } = "No asignado"; // Valor por defecto "No asignado"

        [Display(Name = "Libre")]
        public bool Libre { get; set; } = true;
    }
}