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
        [Route("Persona/Create/{persona:PersonaEntity}")]
        public ActionResult Create(PersonaEntity persona)
        {

            if (persona == null)
            {
                return HttpNotFound();
            }
            bool bandera = _personaService.CreatePersonService(persona);
            if (!bandera)
            {
                return HttpNotFound();
            }
            EditPersonaViewModel model = new EditPersonaViewModel
            {
                Id = Person.Id,
                Name = Person.Name,
                FirtsName = Person.FirtsName,
                Email = Person.Email,
                Phone = Person.Phone
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(EditPersonaViewModel model, int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    PersonaEntity Person = _personaService.FindPersonService(model.Id);
                    Person.Name = model.Name;
                    Person.FirtsName = model.FirtsName;
                    Person.Email = model.Email;
                    Person.Phone = model.Phone;
                    _personaService.ModifyPersonService(model.Id, Person);
                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException exc)
                {

                    throw new Exception($"Se ha producido un error al modificar la persona: {exc.Message}");
                }
            }
            return View(model);
        }
    }
}