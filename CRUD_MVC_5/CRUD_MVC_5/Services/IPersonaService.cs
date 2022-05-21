using CRUD_MVC_5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_MVC_5.Services
{
    public interface IPersonaService
    {
        List<PersonaEntity> ListPersonService();
    }
}
