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
using System.Web;

namespace CRUD_MVC_5.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly string _stringConnection;
        
        public PersonaRepository(DataAccess dataAccess)
        {
            _stringConnection = dataAccess.ConnectionStringSQL;
        }

        private SqlConnection connection()
        {
            return new SqlConnection(_stringConnection);
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
        
        public PersonaEntity FindPerson(int? id)
        {
            PersonaEntity persona = null;
            SqlConnection sqlConnection = connection();
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                //manda lo que se ejecuta
                sqlCommand.CommandText = "dbo.sp_buscar_persona";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pId",SqlDbType.Int).Value=id; 
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
                }
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();

            }
            return persona;
        }
        public bool ModifyPerson(int id, PersonaEntity person)
        {
            PersonaEntity persona = null;
            SqlConnection sqlConnection = connection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                persona = FindPerson(id);
                if (persona == null)
                {
                    return false;
                }
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_modificar_persona";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pId", SqlDbType.Int).Value = person.Id;
                sqlCommand.Parameters.Add("pNombre", SqlDbType.VarChar).Value = person.Name;
                sqlCommand.Parameters.Add("pApellido", SqlDbType.VarChar).Value = person.FirtsName;
                sqlCommand.Parameters.Add("pEmail", SqlDbType.VarChar).Value = person.Email;
                sqlCommand.Parameters.Add("pTelefono", SqlDbType.VarChar).Value = person.Phone;
                //guarda lo que trae la consulta
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw new Exception($"Se produjo un error al editar el producto: {ex.Message}");
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public bool DeletePerson(int? id)
        {
            bool result=false;
            PersonaEntity persona = null;
            SqlConnection sqlConnection = connection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                persona = FindPerson(id);
                if(persona == null)
                {
                    return result;
                }
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_eliminar_persona";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pId", SqlDbType.Int).Value = id;
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                result=true;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw new Exception($"Se produjo un error al guardas los productos: {ex.Message}");
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return result;
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