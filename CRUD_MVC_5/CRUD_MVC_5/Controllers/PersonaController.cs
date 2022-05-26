using CRUD_MVC_5.Models;
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
    }
}