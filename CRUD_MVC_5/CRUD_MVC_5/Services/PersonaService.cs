using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_MVC_5.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public bool CreatePersonService(PersonaEntity persona)
        {
            return _personaRepository.CreatePerson(persona);
        }

        public async Task<List<PersonaEntity>> ListPersonService()
        {
            return await _personaRepository.ListPersons();
        }

        public PersonaEntity FindPersonService(int? id)
        {
            return _personaRepository.FindPerson(id);
        }

        public bool DeletePersonService(int? id)
        {
            return _personaRepository.DeletePerson(id);
        }

        public bool ModifyPersonService(int id, PersonaEntity person)
        {
            return _personaRepository.ModifyPerson(id, person);
        }
    }
}