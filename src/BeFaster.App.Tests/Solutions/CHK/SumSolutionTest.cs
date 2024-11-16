﻿using BeFaster.App.Solutions.CHK;
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
        [TestCase(null, ExpectedResult = -1)]
        [TestCase("AAABB", ExpectedResult = 175)]
        [TestCase("AAABBCCD", ExpectedResult = 230)]
        public int ComputePrice(string? skus)
        {
            return CheckoutSolution.ComputePrice(skus);
        }
    }
}