using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public class HardDrive : Components
    {
        string size;

        public HardDrive (string brand, double price, string size) : base (brand, price)
        {
            this.size = size;
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public override string ToString()
        {
            return base.ToString() + SPACE + size + SPACE + "Hard Drive";
        }
    }
}