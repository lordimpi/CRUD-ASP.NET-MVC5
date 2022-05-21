using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_MVC_5.Models.Entities
{
    public class PersonaEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener un máximo de {1} caratéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener un máximo de {1} caratéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirtsName { get; set; }

        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "El campo {0} no es valido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener un máximo de {1} caratéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [Phone(ErrorMessage = "El campo {0} debe ser numérico.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Phone { get; set; }
    }
}