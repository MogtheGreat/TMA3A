using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMA3A.part3.computerParts
{
    public class OpSys : Components
    {
        public OpSys(string brand, double price) : base(brand, price) { }

        public override string ToString()
        {
            return base.ToString() + SPACE + "OS";
        }
    }
}