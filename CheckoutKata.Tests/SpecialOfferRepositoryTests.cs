#nullable disable

using NUnit.Framework;

namespace CheckoutKata.Tests;

public class SpecialOfferRepositoryTests
{
    private ISpecialOfferRepository _specialOfferRepository;

    [SetUp]
    public void Setup()
    {
        _specialOfferRepository = new SpecialOfferRepository();
    }

    [Test]
    public void ShouldReturnOfferWhenExists()
    {
        var existingOfferSku = "A";

        var offer = _specialOfferRepository.GetBySku(existingOfferSku);

        Assert.That(offer, Is.Not.Null);
        Assert.That(existingOfferSku, Is.EqualTo(offer.Sku));
        Assert.That(3, Is.EqualTo(offer.Quantity));
        Assert.That(130m, Is.EqualTo(offer.SpecialPrice));
    }

    [Test]
    public void ShouldReturnNullWhenOfferDoesNotExist()
    {
        var nonExistingOfferSku = "X";

        var offer = _specialOfferRepository.GetBySku(nonExistingOfferSku);

        Assert.That(offer, Is.Null);
    }
}
