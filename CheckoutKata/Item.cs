namespace CheckoutKata;

public class Item
{
    public string Sku { get; init; }

    public decimal UnitPrice { get; private set; }

    public Item(string sku, decimal unitPrice)
    {
        Sku = sku;
        UnitPrice = unitPrice;
    }
}
