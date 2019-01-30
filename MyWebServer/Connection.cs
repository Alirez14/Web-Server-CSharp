using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer
{
    class Connection
    {
        public Connection(string storeProc)
        {
                SqlCommand = new SqlCommand(storeProc, ConnectionString);
        }
        public SqlConnection ConnectionString
        {
            get
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6JC714Q\SQLEXPRESS;Initial Catalog=dotnet;Integrated Security=True");
                return con;
            }
        }

        public SqlCommand SqlCommand { get; }
    }
}
