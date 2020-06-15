using System;
using System.Collections.Generic;
using System.Text;

namespace dip.src
{
    public class Apple
    {
        public virtual string GetColor()
        {
            return "Red";
        }
    }
    public class Orange:Apple
    {
        public override string GetColor()
        {
            return "Orange";
        }
    }
}
