namespace CheckoutKata;

public class SpecialOfferRepository : ISpecialOfferRepository
{
    private readonly List<SpecialOffer> _offers;

    public SpecialOfferRepository()
    {
        _offers = [];
        InitializeRepository();
    }

    public SpecialOffer? GetBySku(string sku)
    {
        return null;
    }

    private void InitializeRepository()
    {
        _offers.Add(new SpecialOffer("A", 3, 130m));
        _offers.Add(new SpecialOffer("B", 2, 45m));
    }
}
