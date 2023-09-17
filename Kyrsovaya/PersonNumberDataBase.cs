using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    /// <summary>
    /// Предоставляет доступ к номерам телефона работника хранящихся в базе данных
    /// </summary>
    public class PersonNumberDataBase : NumberDataBase
    {
        public int idPerson;
        public int IdPerson
        {
            set { idPerson = value; }
            get { return idPerson; }
        }

        public PersonNumberDataBase(DataGridView dgv, string dataSource, string catalog) : base(dgv, dataSource, catalog) { }

        public void RefreashPersonNumbers()
        {
            OpenConnection();
            dgv.Rows.Clear();
            SqlCommand command = new SqlCommand("GetPersonNumbers", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("id_person", idPerson));

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dgv.Rows.Add(reader.GetInt32(0), reader.GetString(3),
                    tndb.GetTypeName(reader.GetInt32(2)));
            }
            reader.Close();
            CloseConnection();
        }

        public void AddPersonNumber(string number, int idType)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand("AddNumber", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("id_person", idPerson));
            command.Parameters.Add(new SqlParameter("id_type", idType));
            command.Parameters.Add(new SqlParameter("number", number));

            command.ExecuteNonQuery();

            CloseConnection();
        }
    }
}
