namespace TABP.Application.Utilities;

public static class DiscountedPriceCalculator
{
    public static decimal GetFinalDiscountedPrice(
        DateTime startingDate,
        DateTime endingDate,
        decimal perNightPrice,
        decimal? discountPercentage)
    {
        var effectiveDiscountPercentage = discountPercentage ?? 0;
        var totalNights = (endingDate - startingDate).Days + 1;
        var totalOriginalPrice = totalNights * perNightPrice;

        return ApplyDiscount(totalOriginalPrice, effectiveDiscountPercentage);
    }

    private static decimal ApplyDiscount(decimal originalPrice, decimal discountPercentage) =>
        originalPrice - (originalPrice * (discountPercentage / 100));
}