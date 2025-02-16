namespace CheckoutKata;

public class SpecialOffer
{
    public string Sku { get; init; }

    public int Quantity { get; private set; }

    public decimal SpecialPrice { get; private set; }

    public SpecialOffer(string sku, int quantity, decimal specialPrice)
    {
        Sku = sku;
        Quantity = quantity;
        SpecialPrice = specialPrice;
    }
}
