using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public class Ram : Components
    {
        private string memory;

        public Ram(string brand, double price, string memory) : base(brand, price)
        {
            this.memory = memory;
        }

        public string Memory
        {
            get { return memory; }
            set { memory = value; }
        }

        public override string ToString()
        {
            return base.ToString() + " " + memory + " Memory";
        }
    }
}