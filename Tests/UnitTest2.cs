using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using ApprovalUtilities.SimpleLogger;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]

    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var numbers = Enumerable.Range(1,13).Concat(new []{20});
            Approvals.VerifyAll(numbers, i =>  "{0} => {1}".FormatWith(i, SmallestMultiple(i)).Log("", n=>""+n));
            /*
             * 
             * generalize both inputs 
             * maximumDivisor (20) -> {1..20}
             * result ->{all results} 
             *  
             * 1 
             * the smallest r such that 0 < r and for all 0 < di <= d then di % r = 0 
             * r = 1 
             *   di = 1
             *   true
             *   di => next
             *   di % r =0 
             *    if di == 1 true
             *    else
             *    di % r
             *    
             * 1 < r
             *  di = 1
             *  true
             *  di => next
             *      return di % r;
             *   
             * 
             * 
             * */
        }

        private int SmallestMultiple(int maximumDivisor)
        {
            for (int result = 1; result < int.MaxValue; result++)
            {
                if (Enumerable.Range(1, maximumDivisor).All(di => (result % di == 0)))
                {
                    return result;
                }
            }
            return -1;
        }
    }
}
