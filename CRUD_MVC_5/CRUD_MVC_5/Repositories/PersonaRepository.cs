using CRUD_MVC_5.Data;
using CRUD_MVC_5.Models.Entities;
using CRUD_MVC_5.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_MVC_5.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly string _stringConnection;
        public PersonaRepository(DataAccess dataAccess)
        {
            _stringConnection = dataAccess.CadenaEstevan;
        }
        private SqlConnection Connection()
        {
            return new SqlConnection(_stringConnection);
        }
        public List<PersonaEntity> ListPerson()
        {
            List<PersonaEntity> personas = new List<PersonaEntity>();
            PersonaEntity persona = null;
            SqlConnection sqlConnection = Connection();
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "dbo.sp_listar_personas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    persona = new PersonaEntity
                    {
                        Id = Convert.ToInt32(sqlDataReader["id"]),
                        Name = sqlDataReader["nombre"].ToString(),
                        Email = sqlDataReader["email"].ToString(),
                        FirtsName = sqlDataReader["apellido"].ToString(),
                        Phone = sqlDataReader["telefono"].ToString()
                    };
                    personas.Add(persona);
                }
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return personas;
        }

        public bool CreatePerson(PersonaEntity person)
        {
            SqlConnection sqlConnection = Connection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            bool result = false;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_registrar_persona";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pNombre", SqlDbType.VarChar).Value = person.Name;
                sqlCommand.Parameters.Add("pApellido", SqlDbType.VarChar).Value = person.FirtsName;
                sqlCommand.Parameters.Add("pCorreo", SqlDbType.VarChar).Value = person.Email;
                sqlCommand.Parameters.Add("pTelefono", SqlDbType.VarChar).Value = person.Phone;
                //guarda lo que trae la consulta
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw new Exception($"Se produjo un error al Crear nueva persona: {ex.Message}");
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return result;
        }
    }
}