namespace CheckoutKata;

public class ItemRepository : IItemRepository
{
    private readonly List<Item> _items;

    public ItemRepository()
    {
        _items = [];
        InitializeRepository();
    }

    public Item? GetBySku(string sku)
    {
        return null;
    }

    private void InitializeRepository()
    {
        _items.Add(new Item("A", 50m));
        _items.Add(new Item("B", 30m));
        _items.Add(new Item("C", 20m));
        _items.Add(new Item("D", 15m));
    }
}
