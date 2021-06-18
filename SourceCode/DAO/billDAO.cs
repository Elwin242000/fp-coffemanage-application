using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class billDAO
    {
        private static billDAO instance;

        public static billDAO Instance 
        { 
            get { if (instance == null) instance = new billDAO(); return billDAO.instance; } 
            private set { billDAO.instance = null; } 
        }

        private billDAO() { }

        public int GetUncheckbillid(int id)
        {
            DataTable data = dataProvider.Instance.excuteQuery("select * from dbo.Bill where idTable = " + id + " and status = 0");

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        public void checkOut(int id, int discount, float totalPrice)
        {
            string query = "update dbo.Bill set DateCheckOut = GETDATE(), status = 1 , " + "discount = " + discount + ", totalPrice = " + totalPrice +  " where id = " + id;
            dataProvider.Instance.excuteQuery(query);
        }

        public void InsertBill(int id)
        {
            dataProvider.Instance.excuteQuery("exec USP_InsertBill @idTable", new object[] { id });
        }

        public DataTable getListBillDate(DateTime checkIn, DateTime checkOut)
        {
            return dataProvider.Instance.excuteQuery("exec USP_ListBillDate @checkIn , @checkOut", new object[]{checkIn,checkOut});
        }

        public int getMaxIDbill()
        {
            try
            {
                return (int)dataProvider.Instance.excuteScalar("select MAX(id) from dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
