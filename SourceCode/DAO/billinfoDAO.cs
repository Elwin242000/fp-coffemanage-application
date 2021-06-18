using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class billinfoDAO
    {
        private static billinfoDAO instance;

        public static billinfoDAO Instance 
        { 
            get { if (instance == null) instance = new billinfoDAO(); return billinfoDAO.instance; } 
            private set { billinfoDAO.instance = value; }
        }

        private billinfoDAO() { }

        public void deleteBillinfo(int id)
        {
            dataProvider.Instance.excuteQuery("delete dbo.Billinfo where idFood = " + id);
        }

        public List<Billinfo> getListBillInfo(int id)
        {
            List<Billinfo> listBillInfo = new List<Billinfo>();

            DataTable data = dataProvider.Instance.excuteQuery("select * from dbo.BillInfo where idBill = " + id);

            foreach (DataRow item in data.Rows)
            {
                Billinfo info = new Billinfo(item);
                listBillInfo.Add(info);
            }    

            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            dataProvider.Instance.excuteQuery("exec USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
    }
}
