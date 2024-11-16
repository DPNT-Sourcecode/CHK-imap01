using BeFaster.Runner.Exceptions;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string? skus)
        {
            if (string.IsNullOrEmpty(skus))
            {
                return 0;
            }
            //skus = skus.ToUpper();
            int total = 0;
            Dictionary<char, int> itemPricing = new()
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 }
            };
            Dictionary<char, (int, int)> itemDiscounts = new()
            {
                { 'A', (3, 130) },
                { 'B', (2, 45) }
            };

            Dictionary<char, int> checkoutItems = new();
            foreach (var sku in skus)
            {
                if (!itemPricing.ContainsKey(sku))
                {
                    return -1;
                }

                if (!checkoutItems.TryGetValue(sku, out int value))
                {
                    checkoutItems.Add(sku, 1);
                }
                else
                {
                    checkoutItems[sku] = ++value;
                }
            }
            foreach (var checkoutItem in checkoutItems)
            {
                var item = checkoutItem.Key;
                var quantity = checkoutItem.Value;
                if (itemDiscounts.TryGetValue(checkoutItem.Key, out (int, int) discountedItem))
                {
                    if (checkoutItem.Value >= discountedItem.Item1)
                    {
                        var discountsApplied = quantity / discountedItem.Item1;
                        var discountsLeft = quantity % discountedItem.Item1;
                        total += discountsApplied * discountedItem.Item2 + discountsLeft * itemPricing[item];
                    }
                    else
                    {
                        total += quantity * itemPricing[item];
                    }
                }
                else
                {
                    total += quantity * itemPricing[item];
                }
            }
            return total;
        }
    }
}

