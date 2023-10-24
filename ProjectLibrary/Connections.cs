using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectLibrary
{
    public static class Connections
    {
        public static SqlConnection GetConnection()
        {
            string strCon = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=ProjectDB;Integrated Security=True";
            return new SqlConnection(strCon);
        }
    }
}
