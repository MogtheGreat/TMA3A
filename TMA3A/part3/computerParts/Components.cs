using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public abstract class Components
    {
        private string brand;
        private double price;
        private int quantity;
        protected const char SPACE = ' ';

        protected Components(string brnd = "", double prc = 0)
        {
            brand = brnd;
            price = prc;
            quantity = 1;
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public override string ToString()
        {
            return brand;
        }
    }
}