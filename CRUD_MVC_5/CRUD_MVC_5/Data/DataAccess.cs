using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace CRUD_MVC_5.Data
{
    public class DataAccess
    {
        private readonly string connectionStringSQL = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        private readonly string cadenaDaniel = ConfigurationManager.ConnectionStrings["cadenaDaniel"].ToString(); 
        public string ConnectionStringSQL => connectionStringSQL;
        public string CadenaDaniel => cadenaDaniel;
        public DataAccess()
        {
        }

    }
}