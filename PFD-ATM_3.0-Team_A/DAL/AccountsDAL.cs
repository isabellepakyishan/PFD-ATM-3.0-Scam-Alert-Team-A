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
    public class AccountsDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public AccountsDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("PFDATM3.0TeamAConnectionString");

            //Instantiate a SqlConnection object with the Connection String read. 
            conn = new SqlConnection(strConn);
        }

        public bool IsAccountExist(string accountNo)
        {
            bool accountFound = false;

            //Create a SqlCommand object and specify the SQL statement to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT AccountNo FROM Accounts WHERE AccountNo=@enteredAccount";
            cmd.Parameters.AddWithValue("@enteredAccount", accountNo);

            //Open a database connection and execute the SQL statement
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                //Records found
                while (reader.Read())
                {
                    if (reader.GetString(0) == accountNo)
                        //The email address is used by another staff
                        accountFound = true;
                }
            }
            else
            {
                //No record
                accountFound = false; // The email address given does not exist
            }

            reader.Close();
            conn.Close();

            return accountFound;
        }

/*        public Accounts GetAccount(string accountNo)
        {
            //Create a SqlCommand object and specify the SQL statement to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Accounts WHERE AccountNo=@enteredAccount";
            cmd.Parameters.AddWithValue("@enteredAccount", accountNo);

            //Open a database connection and execute the SQL statement
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            Accounts retrievedAccount = new Accounts();
            while (reader.Read())
            {
                retrievedAccount.AccountNo = reader.GetString(0);
                retrievedAccount.Name = reader.GetString(1);
                retrievedAccount.NRIC = reader.GetString(2);
                retrievedAccount.Contact = reader.GetString(3);
                retrievedAccount.Pin = reader.GetString(4);
                retrievedAccount.Balance = reader.GetDecimal(5);
                retrievedAccount.WithdrawalLimit = reader.GetDecimal(6);
                retrievedAccount.TransferLimit = reader.GetDecimal(7);
            }

            return retrievedAccount;
        }*/
    }
}
