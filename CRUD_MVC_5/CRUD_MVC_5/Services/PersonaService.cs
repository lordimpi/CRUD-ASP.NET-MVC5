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

        List<PersonaEntity> IPersonaService.ListPersonService()
        {
            return _personaRepository.ListPersons();
        }
    }
}