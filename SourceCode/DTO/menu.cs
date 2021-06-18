﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class menu
    {
        public menu(string foodName, int count, float price, float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }

        public menu(DataRow row)
        {
            this.FoodName = row["Name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }

        private float totalPrice;
        public float TotalPrice 
        { 
            get => totalPrice; 
            set => totalPrice = value; 
        }

        private float price;
        public float Price 
        { 
            get => price; 
            set => price = value; 
        }

        private int count;
        public int Count 
        { 
            get => count; 
            set => count = value; 
        }

        private string foodName;
        public string FoodName 
        { 
            get => foodName; 
            set => foodName = value; 
        }
    }
}
