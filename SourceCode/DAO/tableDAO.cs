using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    class tableDAO
    {
        private static tableDAO instance;

        public static tableDAO Instance
        {
            get { if (instance == null) instance = new tableDAO(); return tableDAO.instance; }
            private set { instance = value; }
        }

        public static int tableWidth = 70;
        public static int tableHeight = 70;

        private tableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tablelist = new List<Table>();

            DataTable data = dataProvider.Instance.excuteQuery("USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tablelist.Add(table);
            }    

            return tablelist;
        }

        public bool addTable(string name)
        {
            string query = string.Format("insert dbo.TableFood (name) values (N'{0}')", name);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public bool editTable(int id, string name, string status)
        {
            string query = string.Format("update dbo.TableFood set name = N'{0}', status = N'{1}' where id = {2}", name, status, id);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public bool deleteTable(int id)
        {
            string query = string.Format("delete dbo.TableFood where id = {0}", id);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
    }
}
