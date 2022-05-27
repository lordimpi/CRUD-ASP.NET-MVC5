
using System;
using CRUD_MVC_5.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRUD_MVC_5.Repositories.Contracts
{
    public interface IPersonaRepository
    {

        List<PersonaEntity> ListPersons();
        PersonaEntity FindPerson(int? id);
        bool CreatePerson(PersonaEntity persona);
        bool DeletePerson(int? id);
        bool ModifyPerson(int id, PersonaEntity person);
    }
}

