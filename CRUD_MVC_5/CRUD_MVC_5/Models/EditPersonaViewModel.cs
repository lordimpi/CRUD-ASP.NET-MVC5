using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_MVC_5.Models
{
    public class EditPersonaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener un maximo de {1} caracéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener un maximo de {1} caracéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]

        public string FirtsName { get; set; }

        [Display(Name = "Correo")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener un maximo de {1} caracéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo {0} no es valido.")]
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        [Phone(ErrorMessage = "El campo {0} debe ser numérico.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "La cantidad debe ser de 10 dígitos.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Phone { get; set; }
    }
}