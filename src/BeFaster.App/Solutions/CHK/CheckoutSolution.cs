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
            List<(char SKU, int QuantityRequired, int DiscountedPrice)> discountOffers = new()
            {
                {( 'A', 5, 200) },
               {('A', 3, 130) },
                {('B', 2, 45) },
                {('F', 3, 20)   }
            };
            var freeItemOffers = new List<(char SKU, int QuantityRequired, char FreeItemSKU, int FreeItemQuantity)>()
            {
                {('E', 2, 'B', 1) },
                {('F', 2, 'F', 1) }
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
            var itemCounts = new Dictionary<char, int>();
            foreach (var discount in freeItemOffers)
            {
                var sku = discount.SKU;
                var quantityRequired = discount.QuantityRequired;
                var freeItemSKU = discount.FreeItemSKU;
                var freeItemQuantity = discount.FreeItemQuantity;
                if (itemCounts.ContainsKey(sku))
                {
                    var numOfOffers = itemCounts[sku] / quantityRequired;
                    if (numOfOffers > 0)
                    {
                        if (freeItemSKU == sku)
                        {
                            int totalGroupSize = quantityRequired + freeItemQuantity;
                            int numOfGroups = itemCounts[sku] / totalGroupSize;
                            int remainingItems = itemCounts[sku] % totalGroupSize;
                            int chargeableItems = numOfGroups * quantityRequired;
                            if (remainingItems >= quantityRequired)
                            {
                                chargeableItems += quantityRequired;
                            }
                            else { chargeableItems += remainingItems; }
                            itemCounts[sku] = chargeableItems;
                        }
                        else
                        {
                            if (itemCounts.ContainsKey(freeItemSKU))
                            {
                                itemCounts[freeItemSKU] = Math.Max(0, itemCounts[freeItemSKU] - numOfOffers * freeItemQuantity);
                            }
                        }
                    }
                }
            }
            foreach (var item in itemCounts)
            {
                var sku = item.Key;
                var quantity = item.Value;
                var applicableOffers = discountOffers.Where(x => x.SKU == sku).OrderByDescending(o => o.QuantityRequired).ToList();
                if (applicableOffers.Any())
                {
                    foreach (var offer in applicableOffers)
                    {
                        var quantityRequired = offer.QuantityRequired;
                        var discountedPrice = offer.DiscountedPrice;
                        if (quantity >= quantityRequired)
                        {
                            int numOfOffers = quantity / quantityRequired;
                            total += numOfOffers * discountedPrice;
                            quantity -= numOfOffers * quantityRequired;

                        }
                    }

                    if (quantity > 0)
                    {
                        total += quantity * itemPricing[sku];
                    }
                }
                else
                {
                    total += quantity * itemPricing[sku];
                }
            }
            return total;
        }
    }
}





