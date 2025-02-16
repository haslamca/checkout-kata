namespace CheckoutKata;

public interface ICheckoutService
{
    void Scan(string sku);

    decimal GetTotalPrice();
}
