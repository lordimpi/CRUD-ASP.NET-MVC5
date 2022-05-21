using System;
using CRUD_MVC_5.Data;
using System.Data;
using CRUD_MVC_5.Repositories.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CRUD_MVC_5.Models.Entities;
using System.Data.SqlClient;

namespace CRUD_MVC_5.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly string _stringConnection;
        public PersonaRepository(DataAccess dataAccess)
        {
            _stringConnection = dataAccess.CadenaDaniel;
        }

        private SqlConnection connection()
        {
            return new SqlConnection(_stringConnection);
        }

        public List<PersonaEntity> ListPersons()
        {
            List<PersonaEntity> Personas = new List<PersonaEntity>();
            PersonaEntity persona = null;
            //se usa para crear eliminar
            SqlConnection sqlConnection = connection();
            SqlCommand sqlCommand = null;
            // leer datos de lo que trae la consulta y se guardan
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                //manda lo que se ejecuta
                sqlCommand.CommandText = "dbo.sp_listar_personas";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //guarda lo que trae la consulta
                sqlDataReader = sqlCommand.ExecuteReader();
                //lee cada columna hasta el final
                while (sqlDataReader.Read())
                {
                    persona = new PersonaEntity
                    {
                        Id = Convert.ToInt32(sqlDataReader["id"]),
                        Name = sqlDataReader["nombre"].ToString(),
                        FirtsName = sqlDataReader["apellido"].ToString(),
                        Email = sqlDataReader["email"].ToString(),
                        Phone = sqlDataReader["telefono"].ToString()
                    };
                    Personas.Add(persona);
                }
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
                
            }
            return Personas;
        }
    }
}