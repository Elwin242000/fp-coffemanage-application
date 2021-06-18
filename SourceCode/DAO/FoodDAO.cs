using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; } 
        }

        private FoodDAO() { }

        public List<Food> GetFoodList(int id)
        {
            List<Food> list = new List<Food>();

            string query = "select * from Food where idCategory = " + id ;

            DataTable data = dataProvider.Instance.excuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        public List<Food> getListfood()
        {
            List<Food> list = new List<Food>();

            string query = "exec USP_getfoodlist";

            DataTable data = dataProvider.Instance.excuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        public bool addFood(string name, int idCate, float price)
        {
            string query = string.Format("insert dbo.Food (name, idCategory, price ) values (N'{0}', {1}, {2})", name, idCate, price);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public bool editFood(int idFood, string name, int idCate, float price)
        {
            string query = string.Format("update dbo.Food set name = N'{0}', idCategory = {1}, price = {2} where id = {3}", name, idCate, price, idFood);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public bool deleteFood(int idFood)
        {
            billinfoDAO.Instance.deleteBillinfo(idFood);
            string query = string.Format("delete Food where id = {0}", idFood);
            int result = dataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }
        public List<Food> SearchFoodName(string name)
        {
            List<Food> list = new List<Food>();

            string query = string.Format("select * from Food where name like N'%{0}%' ", name);

            DataTable data = dataProvider.Instance.excuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
    }
}
