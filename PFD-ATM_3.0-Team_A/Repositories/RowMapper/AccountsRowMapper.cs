using PFD_ATM_3._0_Team_A.Models;
using System;
using System.Data.SqlClient;

namespace PFD_ATM_3._0_Team_A.Repositories.RowMapper
{
    public class AccountsRowMapper : RowMapper<Accounts>
    {
        #region Singleton
        private AccountsRowMapper() { }

        public static AccountsRowMapper Instance => Singleton.Instance;

        private class Singleton
        {
            static Singleton() { }

            internal static readonly AccountsRowMapper Instance = new AccountsRowMapper();
        }
        #endregion

        public override Accounts Convert(SqlDataReader reader)
        {
            string accountNo = (string)reader["ID"];
            string name = (string)reader["Name"];
            string nric = (string)reader["NRIC"];
            string contact = (string)reader["Contact"];
            string pin = (string)reader["Pin"];
            decimal balance = (decimal)reader["Balance"];
            decimal withdrawalLimit = (decimal)reader["WithdrawalLimit"];
            decimal transferLimit = (decimal)reader["TransferLimit"];


            return new Accounts
            {
                ID = accountNo,
                Name = name,
                NRIC = nric,
                Contact = contact,
                Pin = pin,
                Balance = balance,
                WithdrawalLimit = withdrawalLimit,
                TransferLimit = transferLimit
            };
        }
    }
}

