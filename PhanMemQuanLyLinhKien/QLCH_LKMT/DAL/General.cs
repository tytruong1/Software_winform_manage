using System;
using System.Data.SqlClient;
using System.Data;


namespace DAL
{

    public class General
    {
        public SqlConnection conn = new SqlConnection(@"Data Source=PC\SQLEXPRESS; Initial Catalog=QLCH; Integrated Security=True");
        public DataTable dt = new DataTable();

        public SqlConnection GetConn()
        {
            if(conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
             
        }
        public SqlDataReader Read(string query)
        {
            GetConn();
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }
            catch(Exception err)
            {
                throw;
            }
            
        }
    }
}
