using CRUD_MVC_5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_MVC_5.Repositories.Contracts
{
    public interface IPersonaRepository
    {
        List<PersonaEntity> ListPerson();
        bool CreatePerson(PersonaEntity persona);
    }
}
