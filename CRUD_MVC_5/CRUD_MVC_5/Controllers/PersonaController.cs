using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
        [HttpGet]
        public ActionResult Index()
        {
            List<PersonaEntity> personas = new List<PersonaEntity>();
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
        [HttpGet]
        public ActionResult Create()
        {
            PersonaEntity persona = new PersonaEntity();
            //capturar datos
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre, Apellido, Correo, Teléfono")] PersonaEntity persona)
        {
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        cadenaEstevan.Students.Add(persona);
            //        cadenaEstevan.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //}
            //catch (DataException /* dex */)
            //{
            //    //Log the error (uncomment dex variable name and add a line here to write a log.
            //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            //}
            //return View(persona);
            try
            {
                persona = _personaService.CreatePersonService();
            }
            catch (SqlException exc)
            {
                throw new Exception($"Se ha producido un error al crear la personas: {exc.Message}");
            }
            return View(persona);
        }
    }
}