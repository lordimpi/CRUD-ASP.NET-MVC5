using CRUD_MVC_5.Models;
using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            List<PersonaEntity> personas = new List<PersonaEntity>();
            try
            {

                personas = await _personaService.ListPersonService();
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
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(EditPersonaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PersonaEntity persona = new PersonaEntity
                    {
                        Name = model.Name,
                        Email = model.Email,
                        FirtsName = model.FirtsName,
                        Phone = model.Phone
                    };
                    _personaService.CreatePersonService(persona);
                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException exc)
                {
                    throw new Exception($"Se ha producido un error al crear la persona: {exc.Message}");
                }
             }
             return View(model);
        }
        
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