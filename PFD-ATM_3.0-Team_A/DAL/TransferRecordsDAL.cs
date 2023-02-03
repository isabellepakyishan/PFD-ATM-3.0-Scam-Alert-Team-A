using Microsoft.Extensions.Configuration;   //Contains IConfiguration interface which reads JSON configuration information
using System.IO;                            //Contains classes from file/directory input/output operations
using System.Data.SqlClient;                //Contains ADO.NET classes like SqlConnection, SqlCommand, SqlDataReader
using System;

namespace PFD_ATM_3._0_Team_A.DAL
{
    public class TransferRecordsDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        
        public TransferRecordsDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("PFDATM3.0TeamAConnectionString");

            //Instantiate a SqlConnection object with the Connection String read. 
            conn = new SqlConnection(strConn);
        }

        public bool HasTransferOccurredBefore(string accountNo, string transferAccountNo)
        {
            bool transferOccurred = false;

            //Create a SqlCommand object and specify the SQL statement to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(*) FROM TransferRecords WHERE AccountNo=@enteredAccount AND TransferAccountNo=@transferaccountNo";
            cmd.Parameters.AddWithValue("@enteredAccount", accountNo);
            cmd.Parameters.AddWithValue("@transferaccountNo", transferAccountNo);

            //Open a database connection and execute the SQL statement
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            if (count == 0)
            {
                transferOccurred = false;
            }
            else
            {
                transferOccurred = true;
            }
            return transferOccurred;
        }

        public int InsertTransferRecord(DateTime transferDate, string accountNo, string transferAccountNo, decimal transferAmount, bool transferSuspicious)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO TransferRecords VALUES(@TransferDate, @AccountNo, @TransferAccountNo, @TransferAmount, @TransferSuspicious)";
            
            cmd.Parameters.AddWithValue("@TransferDate", transferDate);
            cmd.Parameters.AddWithValue("@AccountNo", accountNo);
            cmd.Parameters.AddWithValue("@TransferAccountNo", transferAccountNo);
            cmd.Parameters.AddWithValue("@TransferAmount", transferAmount);
            cmd.Parameters.AddWithValue("@TransferSuspicious", transferSuspicious);
            
            conn.Open();
            int count = cmd.ExecuteNonQuery();
            conn.Close();
            return count;
        }
    }
}
