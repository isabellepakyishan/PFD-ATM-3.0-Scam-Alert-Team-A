using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;   //Contains IConfiguration interface which reads JSON configuration information
using System.IO;                            //Contains classes from file/directory input/output operations
using System.Data.SqlClient;                //Contains ADO.NET classes like SqlConnection, SqlCommand, SqlDataReader
using PFD_ATM_3._0_Team_A.Models;

namespace PFD_ATM_3._0_Team_A.DAL
{
    public class TransferDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public TransferDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("PFDATM3.0TeamAConnectionString");

            //Instantiate a SqlConnection object with the Connection String read. 
            conn = new SqlConnection(strConn);
        }

        public bool IsRecordExist(string accountNo, string transferaccountNo)
        {
            bool recordFound = false;

            //Create a SqlCommand object and specify the SQL statement to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM TransferRecords WHERE AccountNo = @accountNo AND TransferAccountNo = @transferaccountNo";

            cmd.Parameters.AddWithValue("@accountNo", accountNo);
            cmd.Parameters.AddWithValue("@transferaccountNo", transferaccountNo);


            //Open a database connection and execute the SQL statement
            conn.Open();

            int count = (int) cmd.ExecuteScalar();

            if (count == 0)
            {
                recordFound = false;
            }
            else
            {
                recordFound = true;
            }

            conn.Close();

            return recordFound;
        }

        public void transfer_record_save(string accountNo, string transferaccountNo, Int32 transferAmount, Int32 transferPending)
        {
            //Create a SqlCommand object and specify the SQL statement to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO TransferRecords (AccountNo, TransferAccountNo, TransferAmount, TransferPending) VALUES (@accountNo, @transferaccountNo, @transferAmount, @transferPending)";

            cmd.Parameters.AddWithValue("@accountNo", accountNo);
            cmd.Parameters.AddWithValue("@transferaccountNo", transferaccountNo);
            cmd.Parameters.AddWithValue("@transferAmount", transferAmount);
            cmd.Parameters.AddWithValue("@transferPending", transferPending);

            //Open a database connection and execute the SQL statement
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            conn.Close();
        }
    }
}
