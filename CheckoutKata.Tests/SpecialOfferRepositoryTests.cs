#nullable disable

using NSubstitute;
using NUnit.Framework;

namespace CheckoutKata.Tests;

public class SpecialOfferRepositoryTests
{
    private ISpecialOfferRepository _specialOfferRepository;

    [SetUp]
    public void Setup()
    {
        _specialOfferRepository = Substitute.For<ISpecialOfferRepository>();
    }

    [Test]
    public void ShouldReturnOfferWhenExists()
    {
        var existingOfferSku = "A";
        _specialOfferRepository
            .GetBySku(existingOfferSku)
            .Returns(new SpecialOffer(existingOfferSku, 3, 130m));

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
        _specialOfferRepository
            .GetBySku(nonExistingOfferSku)
            .Returns((SpecialOffer)null);

        var offer = _specialOfferRepository.GetBySku(nonExistingOfferSku);

        Assert.That(offer, Is.Null);
    }
}
