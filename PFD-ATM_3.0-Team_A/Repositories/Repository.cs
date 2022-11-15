using Microsoft.Extensions.Configuration;
using PFD_ATM_3._0_Team_A.Models;
using PFD_ATM_3._0_Team_A.Repositories.RowMapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace PFD_ATM_3._0_Team_A.Repositories
{
    public abstract class Repository<MODEL, ID> : IRepository<MODEL, ID> where MODEL : IModel<ID>
    {
        protected abstract string Table { get; }
        protected abstract string TableID { get; }
        protected abstract IRowMapper<MODEL> RowMapper { get; }

        protected SqlConnection Connection { get; }

        public Repository()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("PFDATM3.0TeamAConnectionString");
            Connection = new SqlConnection(connectionString);
        }

        public MODEL FindByID(ID id)
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Table} WHERE {TableID} = @id";
            command.Parameters.AddWithValue("@id", id);

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            MODEL model = default;
            while (reader.Read())
            {
                model = RowMapper.Convert(reader);
            }

            reader.Close();
            Connection.Close();

            return model;
        }

        public List<MODEL> FindAll()
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {Table}";

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<MODEL> modelList = new List<MODEL>();
            while (reader.Read())
            {
                MODEL model = RowMapper.Convert(reader);
                modelList.Add(model);
            }

            reader.Close();
            Connection.Close();

            return modelList;
        }

        public bool IsExistByID(ID id)
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT COUNT(1) FROM {Table} WHERE {TableID} = @id";
            command.Parameters.AddWithValue("@id", id);

            Connection.Open();
            int count = (int)command.ExecuteScalar();

            Connection.Close();
            return count > 0;
        }

        public MODEL Save(MODEL model) => (null == model.ID) ? Insert(model) : UpdateByID(model);

        public abstract MODEL Insert(MODEL model);

        public abstract MODEL UpdateByID(MODEL model);

        public int DeleteByID(ID id)
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandText = $"DELETE FROM {Table} WHERE {TableID} = @id";
            command.Parameters.AddWithValue("@id", id);

            Connection.Open();
            int rowDeleted = command.ExecuteNonQuery();

            Connection.Close();
            return rowDeleted;
        }
    }
}
