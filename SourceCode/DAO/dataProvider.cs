using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class dataProvider
    {

        private static dataProvider instance;

        public static dataProvider Instance
        {
            get { if (instance == null) instance = new dataProvider(); return dataProvider.instance; }
            private set { dataProvider.instance = value; } 
        }

        private dataProvider() { }

        private string connectionSTR = "Data Source=.\\sqlexpress;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        public DataTable excuteQuery(string query, object[] para = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (para != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }    
                    }    
                }    

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExcuteNoneQuery(string query, object[] para = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (para != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        public object excuteScalar(string query, object[] para = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (para != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, para[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
