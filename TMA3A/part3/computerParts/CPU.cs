using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public class CPU : Components
    {
        private int core;

        public CPU (string brand, double price, int core) : base (brand, price)
        {
            this.core = core;
        }

        public int Core
        {
            get { return core; }
            set { core = value; }
        }

        public override string ToString()
        {
            return base.ToString() + SPACE + core + SPACE + "Cores Processor";
        }
    }
}