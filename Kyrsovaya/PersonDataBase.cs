using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    /// <summary>
    /// Предоставляет доступ к записям работников хранящихся в базе данных
    /// </summary>
    public class PersonDataBase : DataBase
    {
        private Person focusRecord;
        DataGridView dgv;

        public Person FocusRecord
        {
            get { return focusRecord; }
            set { focusRecord = value; }
        }

        public DataGridView SetDataGridView
        {
            set { dgv = value; }
        }

        public PersonDataBase (DataGridView dgv, string dataSource, string catalog) : base(dataSource, catalog)
        {
            this.dgv = dgv;
        }

        public void RefreshRecord()
        {
            OpenConnection();
            dgv.Rows.Clear();

            SqlCommand Command = new SqlCommand("GetPerson", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                dgv.Rows.Add(Reader.GetInt32(0), Reader.GetString(1),
                    Reader.GetString(2), Reader.GetString(3));
            }

            Reader.Close();
            CloseConnection();
        }

        public void SearchRecord(string text)
        {
            OpenConnection();
            dgv.Rows.Clear();

            SqlCommand Command = new SqlCommand("SearchPerson", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("@text", text));

            SqlDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                dgv.Rows.Add(Reader.GetInt32(0), Reader.GetString(1),
                    Reader.GetString(2), Reader.GetString(3));
            }

            Reader.Close();
            CloseConnection();
        }

        public void AddReccord(string surname, string name, string fathername)
        {
            OpenConnection();

            SqlCommand Command = new SqlCommand("AddPerson", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("@surname", surname));
            Command.Parameters.Add(new SqlParameter("@name", name));
            Command.Parameters.Add(new SqlParameter("@fathername", fathername));
            Command.ExecuteNonQuery();

            CloseConnection();
        }

        public void DeleteReccord()
        {
            OpenConnection();
            SqlCommand Command = new SqlCommand("DeletePerson", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("@id_person", FocusRecord.idPerson));
            Command.ExecuteNonQuery();

            Command.Dispose();
            CloseConnection();
        }

        public void ChangeRecord(string surname, string name, string fathername)
        {
            OpenConnection();
            SqlCommand Command = new SqlCommand("ChangePerson", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("@id_person", FocusRecord.idPerson));
            Command.Parameters.Add(new SqlParameter("@surname", surname));
            Command.Parameters.Add(new SqlParameter("@name", name));
            Command.Parameters.Add(new SqlParameter("@fathername", fathername));
            Command.ExecuteNonQuery();

            CloseConnection();
        }

        public void MakeDataGrid()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("id", "id");
            dgv.Columns.Add("surname", "Фамилия");
            dgv.Columns.Add("name", "Имя");
            dgv.Columns.Add("fathername", "Отчество");

            //dgv.Columns[0].Visible = false;
        }
    }
}
