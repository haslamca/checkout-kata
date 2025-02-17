namespace CheckoutKata;

public class CheckoutService : ICheckoutService
{
    private readonly IItemRepository _itemRepository;
    private readonly ISpecialOfferRepository _specialOfferRepository;
    private readonly List<Item> _receipt = [];

    public CheckoutService(
        IItemRepository itemRepository,
        ISpecialOfferRepository specialOfferRepository)
    {
        _itemRepository = itemRepository;
        _specialOfferRepository = specialOfferRepository;
    }

    public void Scan(string sku)
    {
        if (string.IsNullOrEmpty(sku))
        {
            return;
        }

        var item = _itemRepository.GetBySku(sku.ToUpper());

        if (item == null)
        {
            Console.WriteLine("Invalid SKU.");
            return;
        }

        _receipt.Add(item);
    }

    public decimal GetTotalPrice()
    {
        var total = 0m;

        var itemGroups = _receipt.GroupBy(i => i.Sku).ToDictionary(i => i.Key, i => i.Count());

        foreach (var itemGroup in itemGroups)
        {
            var sku = itemGroup.Key;
            var quantity = itemGroup.Value;

            var item = _itemRepository.GetBySku(sku);
            var offer = _specialOfferRepository.GetBySku(sku);

            if (item != null)
            {
                if (offer != null && quantity >= offer.Quantity)
                {
                    var leftover = quantity % offer.Quantity;

                    if (leftover > 0)
                    {
                        total += leftover * item.UnitPrice;
                    }

                    total += (quantity - leftover) / offer.Quantity * offer.SpecialPrice;
                }
                else
                {
                    total += quantity * item.UnitPrice;
                }
            }
        }

        return total;
    }
}
