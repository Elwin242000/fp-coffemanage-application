using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class accountDAO
    {
        private static accountDAO instance;

        public static accountDAO Instance
        {
            get { if (instance == null) instance = new accountDAO(); return instance; }
            private set { instance = value; }
        }
        private accountDAO() { }
        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = dataProvider.Instance.excuteQuery(query, new object[]{userName,passWord});

            return result.Rows.Count > 0;
        }
        public bool updateAccount(string userName, string displayName, string pass, string newpass)
        {
            int results = dataProvider.Instance.ExcuteNoneQuery("exec USP_updateAccount @userName , @displayName , @password , @newPassword", new object[]{userName,displayName,pass,newpass});
            return results > 0;
        }
        public Account getAccountUN(string userName)
        {
            DataTable data = dataProvider.Instance.excuteQuery("select * from account where userName = '" + userName + "'");

            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public DataTable getListAcc()
        {
            return dataProvider.Instance.excuteQuery("select UserName, DisplayName, Type from Account");
        }
        public bool addAccount(string name, string disname, int type)
        {
            string query = string.Format("insert dbo.Account (UserName, DisplayName, Type ) values (N'{0}', N'{1}', {2})", name, disname, type);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public bool editAccount(string name, string disname, int type)
        {
            string query = string.Format("update dbo.Account set DisplayName = N'{1}', Type = {2} where UserName = N'{0}'", name, disname, type);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public bool deleteAccount(string name)
        {
            string query = string.Format("delete Account where UserName = N'{0}'", name);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public bool resetAccount(string name)
        {
            string query = string.Format("update Account set PassWord = N'0' where UserName = N'{0}'", name);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
    }
}
