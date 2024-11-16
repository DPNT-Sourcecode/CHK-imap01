﻿using BeFaster.Runner.Exceptions;
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
                        if (!discountedItem.FreeItem.HasValue)
                        {
                            continue;
                        }
                        var freeItem = discountedItem.FreeItem.Value;

                        if (!checkoutItems.ContainsKey(freeItem))
                        {
                            continue;
                        }
                        var freeItemsToApply = discountsApplied;
                        if (checkoutItems[freeItem] >= freeItemsToApply)
                        {
                            checkoutItems[freeItem] -= freeItemsToApply;
                        }
                        else { checkoutItems[freeItem] = 0; }
                    }
                }
                if(quantity>0)
                {
                    total += quantity * itemPricing[item];
                }
            }
            return total;
        }
    }
}



