using CRUD_MVC_5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CRUD_MVC_5.Repositories.Contracts
{
    public interface IPersonaRepository
    {
        Task<List<PersonaEntity>> ListPersons();
    }
}