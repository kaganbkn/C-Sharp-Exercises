using System;
using System.Collections.Generic;
using System.Text;

namespace Basics.Exceptions
{
    public class ValueIsExceedException :Exception
    {
        public ValueIsExceedException(string message) : base(message)
        {

        }
    }
}
