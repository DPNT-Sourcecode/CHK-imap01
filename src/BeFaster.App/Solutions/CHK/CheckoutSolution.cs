using BeFaster.Runner.Exceptions;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public static class CheckoutSolution
    {
        public static int ComputePrice(string? skus)
        {
            int total = 0;
            Dictionary<char, int> itemPricing = new();
            itemPricing.Add('A', 50);
            itemPricing.Add('B', 30);
            itemPricing.Add('C', 20);
            itemPricing.Add('D', 15);
            Dictionary<char, (int,int)> itemDiscount = new();
            itemDiscount.Add('A', (3,130));
            itemDiscount.Add('B',(2, 45));
            if (string.IsNullOrEmpty(skus))
            {
                return 0;
            }
            Dictionary<char, int> checkoutItems = new();
            foreach(var sku in skus)
            {
                if (!itemPricing.Keys.Contains(sku))
                    return -1;
                checkoutItems.Add(sku, 1);
            }
            foreach(var checkoutItem in checkoutItems)
            {

            }
        }
    }
}


