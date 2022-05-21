using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CRUD_MVC_5.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaService _personaService;
        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }
        //solo para traer cosas
        [HttpGet]
        //resultado de la accion
        public ActionResult Index()
        {
            List<PersonaEntity> personas = null;
            try
            {
                personas = _personaService.ListPersonService();
            }
            catch (SqlException exc)
            {

                throw new Exception($"Se ha producido un error al listar las personas: {exc.Message}");
            }
            return View(personas);
        }

    }
}