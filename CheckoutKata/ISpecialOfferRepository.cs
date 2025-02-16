namespace CheckoutKata;

public interface ISpecialOfferRepository
{
    SpecialOffer? GetBySku(string sku);
}
