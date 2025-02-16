namespace CheckoutKata;

public interface IItemRepository
{
    Item? GetBySku(string sku);
}
