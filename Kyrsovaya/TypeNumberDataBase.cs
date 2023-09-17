using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    /// <summary>
    /// Предоставляет доступ к типам телефона хранящихся в базе данных
    /// </summary>
    public class TypeNumberDataBase : DataBase
    {
        private List<TypeNumber> typeList;

        public TypeNumberDataBase(string dataSource, string catalog) : base(dataSource, catalog)
        {
            typeList = new List<TypeNumber>();
        }
        public int GetId(string typeName)
        {
            RefreshTypes();
            foreach (TypeNumber i in typeList)
            {
                if (i.name == typeName)
                    return i.idType;
            }
            return 0;
        }

        public int GetId(int index)
        {
            if (index > typeList.Count - 1)
                return 0;

            return typeList[index].idType;
        }

        public string[] GetListTypeName()
        {
            RefreshTypes();
            string[] typeNameList = new string[typeList.Count];
            for (int i = 0; i < typeNameList.Length; i++)
            {
                typeNameList[i] = typeList[i].name;
            }
            return typeNameList;
        }

        public string GetTypeName(int idType)
        {
            RefreshTypes();
            foreach (TypeNumber i in typeList)
            {
                if (i.idType == idType)
                {
                    return i.name;
                }
            }
            return null;
        }

        public bool AddType(string typeName)
        {
            RefreshTypes();
            foreach (TypeNumber i in this.typeList)
            {
                if (i.name == typeName)
                {
                    return false;
                }
            }

            OpenConnection();
            SqlCommand command = new SqlCommand("AddType", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("type", typeName));
            command.ExecuteNonQuery();

            CloseConnection();
            return true;
        }

        public void ChangeType(int idType, string type)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand("ChangeType", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@id_type", idType));
            command.Parameters.Add(new SqlParameter("@type", type));
            command.ExecuteNonQuery();

            CloseConnection();
        }

        public void DeleteType(int idtype)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand("DeleteType", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("id_type", idtype));
            command.ExecuteNonQuery();

            CloseConnection();
        }

        public void RefreshTypes()
        {
            OpenConnection();
            typeList.Clear();
            SqlCommand command = new SqlCommand("GetType", Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                typeList.Add(new TypeNumber(reader.GetInt32(0), reader.GetString(1)));
            }
            reader.Close();
            CloseConnection();
        }
    }
}
