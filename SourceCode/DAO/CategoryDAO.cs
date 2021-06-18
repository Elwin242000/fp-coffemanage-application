using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        } 
        private CategoryDAO() { }
        public List<Category> getListCategory()
        {
            List<Category> list = new List<Category>();

            string query = "select * from dbo.FoodCategory";
            DataTable data = dataProvider.Instance.excuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }
        public Category getCategorybyid(int id)
        {
            Category category = null;

            string query = "select * from FoodCategory where id = " + id;
            DataTable data = dataProvider.Instance.excuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }

            return category;
        }

        public bool addCategory(string name)
        {
            string query = string.Format("insert dbo.FoodCategory (name) values (N'{0}')", name);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool editCategory(string name, int id)
        {
            string query = string.Format("update dbo.FoodCategory set name = N'{0}' where id = {1}", name, id);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool deleteCategory(string name)
        {
            string query = string.Format("delete dbo.FoodCategory where name = N'{0}'", name);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
    }
}
