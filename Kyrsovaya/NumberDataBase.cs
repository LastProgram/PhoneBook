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
    /// Предоставляет доступ к номерам телефона хранящихся в базе данных
    /// </summary>
    public class NumberDataBase : DataBase
    {

        private protected Number focusRecord;
        private protected DataGridView dgv;
        private protected TypeNumberDataBase tndb;

        public Number FocusNumber
        {
            get { return focusRecord; }
            set { focusRecord = value; }
        }

        public DataGridView SetDataGridView
        {
            set { dgv = value; }
        }

        public NumberDataBase(DataGridView dgv, string dataSource, string catalog) : base(dataSource, catalog)
        {
            tndb = new TypeNumberDataBase("Anatoly", "Operator");
            this.dgv = dgv;
        }

        public void RefreashNumbers()
        {
            OpenConnection();
            dgv.Rows.Clear();
            SqlCommand command = new SqlCommand("GetNumbers", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dgv.Rows.Add(reader.GetInt32(0), reader.GetString(3),
                    tndb.GetTypeName(reader.GetInt32(2)));
            }
            reader.Close();
            CloseConnection();
        }

        public void ChangeNumber(int idType, string number)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand("ChangeNumber", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("id_number", focusRecord.id));
            command.Parameters.Add(new SqlParameter("id_type", idType));
            command.Parameters.Add(new SqlParameter("number", number));

            command.ExecuteNonQuery();

            CloseConnection();
        }

        public void DeleteNumber()
        {
            OpenConnection();
            SqlCommand Command = new SqlCommand("DeleteNumber", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            Command.Parameters.Add(new SqlParameter("@id_number", focusRecord.id));
            Command.ExecuteNonQuery();

            CloseConnection();
        }

        public void MakeDataGrid()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("id", "id");
            dgv.Columns.Add("number", "Номер");
            dgv.Columns.Add("type", "Тип");

            //dgv.Columns[0].Visible = false;
        }
    }
}
