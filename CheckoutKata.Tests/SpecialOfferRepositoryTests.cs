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
        var existingSkuOffer = "A";

        var offer = _specialOfferRepository.GetBySku(existingSkuOffer);

        Assert.That(offer, Is.Not.Null);
    }
}
