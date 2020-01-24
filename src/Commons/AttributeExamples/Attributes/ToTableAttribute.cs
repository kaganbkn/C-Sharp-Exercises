using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeExamples
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ToTableAttribute :Attribute
    {
        private readonly string _tableName;
        public ToTableAttribute(string tableName)
        {
            _tableName = tableName;
        }
    }
}
