using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string? skus)
        {
            int total = 0;
            char[] items = ['A', 'B', 'C', 'D'];
            if (string.IsNullOrEmpty(skus))
            {
                return 0;
            }
            Dictionary<char, int> checkoutItems = new();
            foreach(var sku in skus)
            {
                if (items.Contains(sku))
                    return -1;
                checkoutItems.Add(sku, 1);
            }
        }
    }
}
