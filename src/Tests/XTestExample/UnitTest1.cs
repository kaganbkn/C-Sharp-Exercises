using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TestData;
using Xunit;

namespace XTestExample
{
    public class UnitTest1
    {
        [Fact]
        public static void Test_Reverse_String()
        {
            var data = new DummyData();
            var result = data.ReverseString("qwe");
            Assert.True(result == "ewq");
        }

        [Fact]
        public void PassingTest()
        {
            var data = new DummyData();
            Assert.Equal(4, data.Add(2, 2));
        }

    }
}
