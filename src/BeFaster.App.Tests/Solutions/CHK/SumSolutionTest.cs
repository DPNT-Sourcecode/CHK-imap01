using BeFaster.App.Solutions.CHK;
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
        [TestCase("AAABE", ExpectedResult = 200)]
        [TestCase("AAAEE", ExpectedResult = 210)]
        [TestCase("AAABEE", ExpectedResult = 210)]
        [TestCase("EE", ExpectedResult = 80)]
        [TestCase("EEE", ExpectedResult = 120)]
        [TestCase("EEB", ExpectedResult = 80)]
        [TestCase("EEEB", ExpectedResult = 120)]
        [TestCase("EEEEBB", ExpectedResult = 160)]
        [TestCase("BEBEEE", ExpectedResult = 160)]
        [TestCase("ABCDEABCDE", ExpectedResult = 280)]
        [TestCase("AAAAAB", ExpectedResult = 230)]
        [TestCase("AAAAAAB", ExpectedResult = 280)]
        [TestCase("AAAAAAAAB", ExpectedResult = 360)]
        [TestCase("AAAAAAAAAAB", ExpectedResult = 430)]
        [TestCase("AAAAAAAAAAAAAB", ExpectedResult = 560)]
        [TestCase("AAAAAAAAAAAAAAB", ExpectedResult = 610)]
        [TestCase("ABCEEEEEGG2", ExpectedResult = -1)]
        [TestCase(null, ExpectedResult = 0)]
        [TestCase("AAABB", ExpectedResult = 175)]
        [TestCase("AAABBCCD", ExpectedResult = 230)]
        [TestCase("AAABBCCD", ExpectedResult = 230)]
        [TestCase("F", ExpectedResult = 10)]
        [TestCase("FF", ExpectedResult = 20)]
        [TestCase("FFF", ExpectedResult = 20)]
        [TestCase("FFFF", ExpectedResult = 30)]
        [TestCase("FFFFFF", ExpectedResult = 40)]
        [TestCase("FFFEE", ExpectedResult = 100)]
        [TestCase("WXYZ", ExpectedResult = 170)]

        [TestCase("HHHHH", ExpectedResult = 45)]
        [TestCase("HHHHHHHHHH", ExpectedResult = 80)]
        [TestCase("KK", ExpectedResult = 150)]
        [TestCase("NNNNM", ExpectedResult = 160)]

        [TestCase("PPPPP", ExpectedResult = 200)]
        [TestCase("QQQ", ExpectedResult = 80)]
        [TestCase("RRR", ExpectedResult = 150)]
        [TestCase("RRRQ", ExpectedResult = 150)]
        [TestCase("UUU", ExpectedResult =120)]
        [TestCase("VV", ExpectedResult = 90)]
        [TestCase("VVV", ExpectedResult =130)]
        public int ComputePrice(string? skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}



