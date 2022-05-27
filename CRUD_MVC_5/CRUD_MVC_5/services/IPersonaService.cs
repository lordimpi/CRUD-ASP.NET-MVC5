using CRUD_MVC_5.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_MVC_5.Services
{
    public interface IPersonaService
    {
        Task<List<PersonaEntity>> ListPersonService();
        PersonaEntity FindPersonService(int? id);
        bool DeletePersonService(int? id);
        bool ModifyPersonService(int id, PersonaEntity person);
        bool CreatePersonService(PersonaEntity persona);
    }
}
