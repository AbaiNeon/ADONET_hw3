using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;


namespace ADONET_hw3
{
    class Program
    {
        public static DbConnection CreateConnection()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ProviderName);

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;

            return connection;
        }

        static void Main(string[] args)
        {
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                DbCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "create table gruppa(id int identity primary key, name varchar not null)";
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (DbException ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
