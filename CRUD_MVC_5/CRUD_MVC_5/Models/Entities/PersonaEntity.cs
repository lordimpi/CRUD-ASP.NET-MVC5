using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC_5.Models.Entities
{
    public class PersonaEntity
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
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        [Phone(ErrorMessage = "El campo {0} debe ser numérico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Phone { get; set; }
    }
}