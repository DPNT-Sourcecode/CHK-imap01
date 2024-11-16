using BeFaster.App.Solutions.CHL;
using BeFaster.App.Solutions.SUM;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CheckoutSolutionTest
    {
        [TestCase("ABCD", ExpectedResult = 115)]
        [TestCase("AAAB", ExpectedResult = 160)]
        [TestCase("ABCEEEEEGG", ExpectedResult = -1)]
        [TestCase("AAABB", ExpectedResult = 175)]
        [TestCase("AAABBCCD", ExpectedResult = 175)]
        public int ComputeSum(string? skus)
        {
            return CheckliteSolution.ComputePrice(skus);
        }
    }
}

