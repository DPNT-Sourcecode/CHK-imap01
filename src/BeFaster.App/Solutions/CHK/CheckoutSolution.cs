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
                { 'D', 15 },
                {'E',40 },
            };
            Dictionary<char, List<(int, int, char?)>> itemDiscounts = new()
            {
                { 'A', [(5, 200,null),(3, 130,null)] },
                { 'B', [(2, 45,null)] },
                {'E', [(2, 80,'B')]   }
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
            //free items

            foreach (var checkoutItem in checkoutItems)
            {
                var item = checkoutItem.Key;
                var quantity = checkoutItem.Value;
                if (itemDiscounts.ContainsKey(checkoutItem.Key))
                {
                    var itemDiscount = itemDiscounts[item];
                    var sortedDiscounts = itemDiscounts[item].OrderByDescending(x => x.Item1).OrderBy(x => x.Item3.HasValue);
                    foreach (var discountedItem in sortedDiscounts)
                    {
                        if (discountedItem.Item3.HasValue)
                        {

                            if (checkoutItems.ContainsKey(discountedItem.Item3.Value) && quantity>= discountedItem.Item1)
                            {
                                var discountedItemQuantity = checkoutItems[discountedItem.Item3.Value];
                                checkoutItems[discountedItem.Item3.Value] -= discountedItem.Item3.Value.ToString().Length*discountedItemQuantity;
                            }
                        }
                    }
                }
            }
                foreach (var checkoutItem in checkoutItems)
            {
                var item = checkoutItem.Key;
                var quantity = checkoutItem.Value;
                if (itemDiscounts.ContainsKey(checkoutItem.Key))
                {
                    var sortedDiscounts = itemDiscounts[item].OrderByDescending(x => x.Item1).OrderBy(x => x.Item3.HasValue) ;
                    foreach (var discountedItem in sortedDiscounts)
                    {
                        if (checkoutItem.Value >= discountedItem.Item1)
                        {
                            var discountsApplied = quantity / discountedItem.Item1;

                            total += discountsApplied * discountedItem.Item2;
                            quantity -= discountsApplied * discountedItem.Item1;
                            //if (discountedItem.Item3.HasValue)
                            //{if(checkoutItems.ContainsKey(discountedItem.Item3.Value))
                            //    {
                            //        var discountedItemQuantity = checkoutItems[discountedItem.Item3.Value];
                            //        var timesToApplyFreeItem = 0;
                            //        if(discountedItemQuantity >= discountsApplied)
                            //        {
                            //            timesToApplyFreeItem = discountsApplied;
                            //        }
                            //        else
                            //        {
                            //            timesToApplyFreeItem = discountedItemQuantity;
                            //        }
                            //        total -= timesToApplyFreeItem * itemPricing[discountedItem.Item3.Value];
                            //    }

                            //}
                        }
                    }

                }
                total += quantity * itemPricing[item];
            }
            return total;
        }
    }
}






