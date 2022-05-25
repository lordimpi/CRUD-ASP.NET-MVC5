using CRUD_MVC_5.Models;
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
        [HttpGet]
        //[Route("Persona/FindPerson/{id:int}")]
        public ActionResult FindPerson(int? id)
        {
            PersonaEntity persona = null;
            try
            {
                persona = _personaService.FindPersonService(id);
            }
            catch (SqlException exc)
            {

                throw new Exception($"Se ha producido un error al buscar la persona: {exc.Message}");
            }
            return View(persona);
        }
        [HttpGet]
        [Route("Persona/Modify/{id:int}")]
        public ActionResult Modify(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            PersonaEntity Person = _personaService.FindPersonService(id);
            if (Person==null)
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
        public ActionResult Modify(EditPersonaViewModel model, int? id)
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
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            PersonaEntity Person = _personaService.FindPersonService(id);
            if (Person == null)
            {
                return HttpNotFound();
            }
            return View(Person);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _personaService.DeletePersonService(id);
            }
            catch (SqlException exc)
            {
                throw new Exception($"Se ha producido un error al eliminar la persona: {exc.Message}");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}