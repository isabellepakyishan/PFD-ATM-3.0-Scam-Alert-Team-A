using Microsoft.Extensions.Configuration;   //Contains IConfiguration interface which reads JSON configuration information
using System.IO;                            //Contains classes from file/directory input/output operations
using System.Data.SqlClient;                //Contains ADO.NET classes like SqlConnection, SqlCommand, SqlDataReader

namespace PFD_ATM_3._0_Team_A.DAL
{
    public class WithdrawalRecordsDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public WithdrawalRecordsDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("PFDATM3.0TeamAConnectionString");

            //Instantiate a SqlConnection object with the Connection String read. 
            conn = new SqlConnection(strConn);
        }

        public int InsertWithdrawalRecord(string accountNo, decimal finalBalance, bool withdrawalSuspicious)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO WithdrawalRecords VALUES(@AccountNo, @FinalBalance, @WithdrawalSuspicious)";

            cmd.Parameters.AddWithValue("@AccountNo", accountNo);
            cmd.Parameters.AddWithValue("@FinalBalance", finalBalance);
            cmd.Parameters.AddWithValue("@WithdrawalSuspicious", withdrawalSuspicious);

            conn.Open();
            int count = cmd.ExecuteNonQuery();
            conn.Close();
            return count;
        }
    }
}
