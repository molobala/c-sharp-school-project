using System.Data.SqlClient;

namespace DB
{
    internal class Helper
    {
        private static readonly string HOST = "MOLO\\SQLEXPRESS";

        private static readonly string BD = "search_eng";
        // la connection a la base de données

        public static SqlConnection open()
        {
            var str = "Data Source=" + HOST + "; Initial Catalog=" + BD + "; Integrated Security=true";
            var con = new SqlConnection(str);
            con.Open();
            return con;
        }

        // de deconnecter
        public static void close(SqlConnection con)
        {
            con.Close();
        }
    }
}