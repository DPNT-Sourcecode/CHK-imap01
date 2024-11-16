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
                var quantity = checkoutItem.Value;
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
                    if (!checkoutItems.ContainsKey(discountedItem.FreeItem.Value) || quantity < discountedItem.ItemQuantity)
                    {
                        continue;
                    }
                    int totalItemsDiscounted = 0;
                    int totalFreeItems = 0;
                    var freeItem= discountedItem.FreeItem.Value;
                    var timesToApply = (quantity -totalItemsDiscounted)
                        / discountedItem.ItemQuantity;
                    if(timesToApply<=0)
                    {
                        continue;
                    }   
                    totalItemsDiscounted += timesToApply * discountedItem.ItemQuantity;
                    totalFreeItems += timesToApply;

                    if (item == freeItem)
                    {
                        totalItemsDiscounted+= totalFreeItems;
                    }
                    else if (checkoutItems.ContainsKey(freeItem))
                    {
                        if (checkoutItems[freeItem] >= totalFreeItems)
                       checkoutItems[freeItem] -= totalFreeItems;
                        else { checkoutItems[freeItem] = 0; }
                    } 
                    
               
                }
            }
            foreach (var checkoutItem in checkoutItems)
            {
                var item = checkoutItem.Key;
                var quantity = checkoutItem.Value;
                if (!itemDiscounts.ContainsKey(checkoutItem.Key))
                {
                    total += quantity * itemPricing[item];
                    continue;
                }
                else
                {
                    var sortedDiscounts = itemDiscounts[item].Where(x => x.FreeItem is null).OrderByDescending(x => x.ItemQuantity).ToList();
                    foreach (var discountedItem in sortedDiscounts)
                    {
                        if (checkoutItem.Value < discountedItem.ItemQuantity)
                        {
                            continue;
                        }
                        var discountsApplied = quantity / discountedItem.ItemQuantity;
                        total += discountsApplied * discountedItem.ItemQuantityPrice;
                        quantity -= discountsApplied * discountedItem.ItemQuantity;
                    }

                }
                
            }
            return total;
        }
    }
}

