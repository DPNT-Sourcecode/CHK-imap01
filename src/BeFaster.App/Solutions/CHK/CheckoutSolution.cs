using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string? skus)
        {
            int total = 0;
            Dictionary<char, int> itemPricing = new[]
            {
                new KeyValuePair<char, int>('A', 50),
                new KeyValuePair<char, int>('B', 30),
                new KeyValuePair<char, int>('C', 20),
                new KeyValuePair<char, int>('D', 15)
            };

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
            foreach(var checkoutItem in checkoutItems)
            {

            }
        }
    }
}

