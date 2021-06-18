using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class menuDAO
    {
        private static menuDAO instance;

        public static menuDAO Instance
        {
            get { if (instance == null) instance = new menuDAO(); return menuDAO.instance; }
            private set { menuDAO.instance = value; }
        }

        private menuDAO() { }

        public List<menu> GetListMenu(int id)
        {
            List<menu> listmenu = new List<menu>();

            string query = "select f.name, bi.count, f.price, f.price*bi.count AS totalPrice from dbo.BillInfo as bi, dbo.Bill as b, dbo.Food as f where bi.idBill = b.id and bi.idFood = f.id and b.status = 0 and b.idTable = " + id;
            DataTable data = dataProvider.Instance.excuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                menu meNu = new menu(item);
                listmenu.Add(meNu);
            }    

            return listmenu;
        }
    }
}
