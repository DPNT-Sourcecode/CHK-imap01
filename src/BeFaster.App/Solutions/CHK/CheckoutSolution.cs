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
                {'F',10 },
            };
            Dictionary<char, List<(int ItemQuantity, int ItemQuantityPrice, char? FreeItem)>> itemDiscounts = new()
            {
                { 'A', [(5, 200,null),(3, 130,null)] },
                { 'B', [(2, 45,null)] },
                {'E', [(2, 80,'B')]   },
                {'F', [(2, 20,'F')]   }
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

            foreach (var checkoutItem in checkoutItems.ToList())
            {
                var item = checkoutItem.Key;
                var checkoutItemQuantity = checkoutItem.Value;
                if (!itemDiscounts.ContainsKey(checkoutItem.Key))
                {
                    continue;
                }
                var itemDiscount = itemDiscounts[item];
                var sortedDiscounts = itemDiscounts[item].OrderByDescending(x => x.ItemQuantity).OrderBy(x => x.FreeItem.HasValue);
                foreach (var discountedItem in sortedDiscounts)
                {
                    if (!discountedItem.FreeItem.HasValue)
                    {
                        continue;
                    }
                    if (!checkoutItems.ContainsKey(discountedItem.FreeItem.Value) || checkoutItemQuantity < discountedItem.ItemQuantity)
                    {
                        continue;
                    }
                    var freeItem= discountedItem.FreeItem.Value;
                    var timesToApply = checkoutItemQuantity / discountedItem.ItemQuantity;
                    if(timesToApply<=0)
                    {
                        continue;
                    }
                    if (item == freeItem)
                    {
                        int totalItemsUsed = timesToApply * discountedItem.ItemQuantity + timesToApply;
                        if (checkoutItemQuantity >= totalItemsUsed)
                        {
                            checkoutItems[item] -= totalItemsUsed;
                        }
                        else
                        {
                            checkoutItemQuantity = 0;
                        }
                    }
                    else if (checkoutItems.ContainsKey(discountedItem.FreeItem.Value))
                    {
                        checkoutItems[discountedItem.FreeItem.Value] -= timesToApply;
                    } 
                    
               
                }
            }
            foreach (var checkoutItem in checkoutItems)
            {
                var item = checkoutItem.Key;
                var quantity = checkoutItem.Value;
                if (itemDiscounts.ContainsKey(checkoutItem.Key))
                {
                    var sortedDiscounts = itemDiscounts[item].Where(x => x.FreeItem is null).OrderByDescending(x => x.ItemQuantity).ToList();
                    foreach (var discountedItem in sortedDiscounts)
                    {
                        if (checkoutItem.Value >= discountedItem.ItemQuantity)
                        {
                            var discountsApplied = quantity / discountedItem.ItemQuantity;
                            total += discountsApplied * discountedItem.ItemQuantityPrice;
                            quantity -= discountsApplied * discountedItem.ItemQuantity;
                        }
                    }

                }
                total += quantity * itemPricing[item];
            }
            return total;
        }
    }
}



