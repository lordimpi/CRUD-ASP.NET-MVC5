using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_MVC_5.Service
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<List<PersonaEntity>> ListPersonService()
        {
            return await _personaRepository.ListPersons();
        }
    }
}