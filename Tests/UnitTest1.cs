using System;
using ApprovalUtilities.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test3And4SidesShouldEqual5()
        {
            double result = Hypotenuse(3,4);
            result.Should().Be(5);
        }

        [TestMethod]
        public void TestTheorem()
        {
            int x = 3;
            for (int y = 1; y < int.MaxValue; y++)
            {
                double hypotenuse = Hypotenuse(x, y);
                double i = (x*x + y*y);
                Assert.AreEqual(i, hypotenuse*hypotenuse, 0.001, "[{0}, {1}]".FormatWith(x, y));
            }
        }
        private double Hypotenuse(int y, int x)
        {
            return Math.Sqrt(x*x + y*y);
        }
    }
}
