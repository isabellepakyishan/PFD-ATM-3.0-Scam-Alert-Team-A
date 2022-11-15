using System.Data.SqlClient;

namespace PFD_ATM_3._0_Team_A.Repositories.RowMapper
{
    public interface IRowMapper<MODEL>
    {
        public MODEL Convert(SqlDataReader reader);
    }
}
