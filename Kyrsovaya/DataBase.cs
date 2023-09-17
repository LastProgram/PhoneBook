using System;
using System.Data.SqlClient;

namespace PhoneBook
{
    /// <summary>
    /// Определяет подключение базы данных
    /// </summary>
    public abstract class DataBase
    {
        private SqlConnection connection;

        private protected SqlConnection Connection
        {
            private set { connection = value; }
            get { return connection; }
        }

        public DataBase(string dataSource, string catalog)
        {
            Connection = new SqlConnection($@"Data Source={dataSource};Initial Catalog={catalog};Integrated Security=True");
        }

        private protected void OpenConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        private protected void CloseConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
    }
}
