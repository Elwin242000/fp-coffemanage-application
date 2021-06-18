using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dataCheckin, DateTime? dateCheckout, int status, int discount)
        {
            this.ID = id;
            this.DateCheckIn = dataCheckin;
            this.DateCheckOut = dateCheckout;
            this.Status = status;
            this.Discount = discount;
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckin"];
            var dateCheckOutTemp = row["dateCheckout"];
            if (dateCheckOutTemp.ToString() != "")
                this.dateCheckOut = (DateTime?)dateCheckOutTemp;
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }

        private int discount;
        public int Discount 
        { 
            get => discount; 
            set => discount = value; 
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime? dateCheckOut;

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        private DateTime? dateCheckIn;

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        private int iD;

        public int ID 
        {
            get { return iD; } 
            set { iD = value; } 
        }
    }
}
