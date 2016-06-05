using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.SimpleLogger;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    [UseReporter(typeof (DiffReporter), typeof (ClipboardReporter))]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var numbers = Enumerable.Range(1, 6);//.Concat(new []{20});
            Approvals.VerifyAll(numbers, i => "{0} => {1}".FormatWith(i, SmallestMultiple(i)).Log("", n => "" + n));
            /*
             * generalize both inputs 
             * maximumDivisor (20) -> {1..20}
             * result ->{all results} 
             *  
             * given d
             * for all di such that 1 < di < d 
             * smallest r such that  r % di =0 
             * 4
             *   {1..d}! 
             *   1 * 2 * 3 * 4
             *   1 * 2 * 3 * (2 * 2)
             *   remove any value such that {1..d} / value is still true.
             *   any 
             *   1 * (1)
             *   {1..{pf(d)}!
             
             * r such that r is smallest & divisable by all {1..d}
             * r => product of prime factors
             * pf(r) and remove 1 form the set   pf(r-1)<  pf(r)
             * pf(r-1) must not by divisable by all {1..d}
             * 
             * 
             * {2,2,5,7,7,3,10} this is divisable by all {1..d}
             * {2,2,5,7,3,10} this is divisable then it smaller.
             * there the shortest list that can't shrink is the smaller number.
             * 
             *  {1}
             *  smallest
             *  
             *  {2,3} smallest
             *  2 * {3}  where list is still valid
             *  3 * {2} 
             *  
             *  {2} * {2}
             *  {2} * {2,5,7,7,3,10}  =  smallest disisable
             *  then niether side is disivable
             *  
             *  
             * 
             * */
        }

        private long SmallestMultiple2(int maximumDivisor)
        {
            // construct prime factor list
            int[] primes = Enumerable.Range(1, maximumDivisor).SelectMany(i => GetPrimeFactors(i)).ToArray();
            // shrink list
            var min = ShrinkList(primes, maximumDivisor);
            //return result
            return min.Multiply();
        }

        private IEnumerable<int> ShrinkList(IEnumerable<int> primes, int maximumDivisor)
        {
            if (primes.Count() <= 1)
            {
                return primes;
            }
            for (int indexToSkip = 0; indexToSkip < primes.Count() ; indexToSkip++)
            {
                var remaining = primes.Take(indexToSkip).Concat(primes.Skip(indexToSkip + 1)).ToArray();
                if (IsDivisable(maximumDivisor, remaining))
                {
                    return ShrinkList(remaining, maximumDivisor);
                }
            }
           
                return primes;
            }

        private bool IsDivisable(int maximumDivisor, IEnumerable<int> tail)
        {
            var result = tail.Multiply();
            return (Enumerable.Range(1, maximumDivisor).All(di => (result%di == 0)));

        }

        private IEnumerable<int> GetPrimeFactors(int number)
        {
            var found = new List<int> {};
            for (int i = 2; i <= number; i++)
            {
                if (number%i == 0)
                {
                    found.Add(i);
                    number = number/i;
                    i = 1;
                }
            }
            return found;
        }

        private long SmallestMultiple(int maximumDivisor)
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                var result = GetPrimeFactors(i).Multiply();
                
                if (Enumerable.Range(1, maximumDivisor).All(di => (result%di == 0)))
                {
                    return result;
                }
            }
            return -1;
        }
    }

    static class Help
    {
        public static long Multiply(this IEnumerable<int> list)
        {
           return  list.Aggregate(1L, (prod, next) => prod*next);
        }
    }
}