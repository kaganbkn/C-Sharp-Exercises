using System;
using System.Linq;
using Xunit;

namespace TestExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ReverseString("qwe"));
            Console.Read();
        }

        public static string ReverseString(string value)
        {
            var temp = value.ToCharArray();
            return temp.Reverse().ToString();
        }

        [Fact]
        public static void Test_Reverse_String()
        {
            var result = ReverseString("qwe");
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
