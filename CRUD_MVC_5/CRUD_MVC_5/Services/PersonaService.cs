using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRUD_MVC_5.Services
{
    internal class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public bool DeletePersonService(int? id)
        {
            return _personaRepository.DeletePerson(id);
        }

        public PersonaEntity FindPersonService(int? id)
        {
            return _personaRepository.FindPerson(id);
        }

        public bool ModifyPersonService(int id, PersonaEntity person)
        {
            return _personaRepository.ModifyPerson(id, person);
        }

        List<PersonaEntity> IPersonaService.ListPersonService()
        {
            return _personaRepository.ListPersons();
        }


    }
}