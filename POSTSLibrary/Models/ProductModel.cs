using System;
using System.Collections.Generic;
using System.Text;

namespace POSTSLibrary.Models
{
    public class ProductModel
    {
        private string _id;
        private double _price;
        private BulkSpecial _special;


        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public BulkSpecial Special
        {
            get { return _special; }
            set { _special = value; }
        }


        public ProductModel(string id)
        {
            _id = id;
            _price = 0.00;
        }
        public ProductModel(string id, double price)
        {
            _id = id;
            _price = price;
        }
        public ProductModel(string id, double price, BulkSpecial special)
        {
            _id = id;
            _price = price;
            _special = special;
        }
    }
    public class BulkSpecial
    {
        public int Quantity;
        public double BulkPrice;

        public BulkSpecial(int quantity, double bulkPrice)
        {
            Quantity = quantity;
            BulkPrice = bulkPrice;
        }

    }

}
