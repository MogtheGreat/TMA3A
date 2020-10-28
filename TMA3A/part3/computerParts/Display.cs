using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public class Display : Components
    {
        private int width;

        public Display (string brand, double price, int width) : base (brand, price)
        {
            this.width = width;
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public override string ToString()
        {
            return base.ToString() + SPACE + width + "\"" + SPACE + "monitor";
        }
    }
}