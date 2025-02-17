namespace CheckoutKata;

public class CheckoutService : ICheckoutService
{
    private readonly IItemRepository _itemRepository;
    private readonly List<Item> _receipt = [];

    public CheckoutService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public void Scan(string sku)
    {
        if (string.IsNullOrEmpty(sku))
        {
            return;
        }

        var item = _itemRepository.GetBySku(sku);

        if (item == null)
        {
            return;
        }

        _receipt.Add(item);
    }

    public decimal GetTotalPrice()
    {
        return _receipt.Sum(i => i.UnitPrice);
    }
}
