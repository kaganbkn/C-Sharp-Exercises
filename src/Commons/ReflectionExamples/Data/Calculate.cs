using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionExamples.Data
{
    public class Calculate
    {
        private int _number1;
        private int _number2;
        public Calculate(int number11, int number22)
        {
            _number1 = number11;
            _number2 = number22;
        }
        public int Add(int number, int number1)
        {
            return number + number1;
        }
        public int Subtraction(int number, int number1)
        {
            return number - number1;
        }
        public int Multiplication(int number, int number1)
        {
            return number * number1;
        }
        public int Division(int number, int number1)
        {
            return number / number1;
        }
    }
}
