using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CRUD_MVC_5.Data
{
    public class DataAccess
    {
        private readonly string connectionStringSQL = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        private readonly string cadenaDaniel = ConfigurationManager.ConnectionStrings["cadenaDaniel"].ToString();
        private readonly string cadenaEstevan = ConfigurationManager.ConnectionStrings["cadenaEstevan"].ToString();
        public string ConnectionStringSQL => connectionStringSQL;
        public string CadenaDaniel => cadenaDaniel;
        public string CadenaEstevan => cadenaEstevan;
        public DataAccess()
        {
        }
    }
}