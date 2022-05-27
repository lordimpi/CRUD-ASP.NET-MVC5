using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_MVC_5.services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }
        public List<PersonaEntity> ListPersonService()
        {
            return _personaRepository.ListPerson();
        }
        public bool CreatePersonService(PersonaEntity persona)
        {
            return _personaRepository.CreatePerson(persona);
        }
    }
}