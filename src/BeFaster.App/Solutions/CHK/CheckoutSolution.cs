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
           List<(char SKU,int QuantityRequired, int? DiscountedPrice, char? FreeItemSKU,int? FreeItemQuantity)> itemDiscounts = new()
            {
                {( 'A', 5, 200,null,null) },
               {('A', 3, 130, null, null) },
                {('B', 2, 45, null, null) },
                {('E', 2,null, 'B', 1)  },
                {('F', 2, null, 'F',1)   }
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
            var itemCounts=new Dictionary<char, int>();
            foreach(var discount in itemDiscounts)
            {
               var sku = discount.SKU;
                var quantityRequired=discount.Quantity;
                var freeItemSKU=(char)discount.FreeItemSKU;
                var freeItemQuantity = discount.FreeItemQuantity;
                if (itemCounts.ContainsKey(sku))
                {
                    var numOfOffers=itemCounts[sku] / quantityRequired;
                    if (numOfOffers>0)
                    {
                        if(itemCounts.ContainsKey(freeItemSKU))
                            itemCounts[freeItemSKU]=Math.Max(0,itemCounts[freeItemSKU] - numOfOffers * freeItemQuantity.Value);
                    }
                }
            }
            foreach (var checkoutItem in checkoutItems.ToList())
            {
                var item = checkoutItem.Key;
                var quantity = checkoutItem.Value;
                if (!itemDiscounts.ContainsKey(checkoutItem.Key))
                {
                    total += quantity * itemPricing[item];
                    continue;
                }
                var sortedDiscounts = itemDiscounts[item]
                    .OrderByDescending(x => x.ItemQuantity)
                    .ThenBy(x => x.FreeItem.HasValue).ToList();
                foreach (var discountedItem in sortedDiscounts)
                {
                    if (quantity < discountedItem.ItemQuantity)
                    {
                        continue;
                    }
                    var discountsApplied = quantity / discountedItem.ItemQuantity;
                    total += discountsApplied * discountedItem.ItemQuantityPrice;
                    quantity -= discountsApplied * discountedItem.ItemQuantity;
                    if (discountedItem.FreeItem.HasValue)
                    {

                        var freeItem = discountedItem.FreeItem.Value;

                        if (checkoutItems.ContainsKey(freeItem))
                        {
                            var freeItemsToApply = discountsApplied;
                            if (checkoutItems[freeItem] >= freeItemsToApply)
                            {
                                checkoutItems[freeItem] -= freeItemsToApply;

                            }
                            else
                            {
                                freeItemsToApply = checkoutItems[freeItem];
                                checkoutItems[freeItem] = 0;
                            }
                           total -= freeItemsToApply * itemPricing[freeItem];
                        }

                    }
                }
                if (quantity > 0)
                {
                    total += quantity * itemPricing[item];
                }
            }


            return total;
        }
    }
}
