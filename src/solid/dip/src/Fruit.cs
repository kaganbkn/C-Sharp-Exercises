using System;
using System.Collections.Generic;
using System.Text;

namespace dip.src
{
    public abstract class Fruit
    {
        public abstract string GetColor();
    }
    public class Banana : Fruit
    {
        public override string GetColor()
        {
            return "Yellow";
        }
    }
    public class Avocado : Fruit
    {
        public override string GetColor()
        {
            return "Green";
        }
    }
}
