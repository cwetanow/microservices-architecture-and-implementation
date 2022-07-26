namespace Basket.WebApi.Entities;

public class ShoppingCart
{
    private readonly List<ShoppingCartItem> items = new();

    public string User { get; } = string.Empty;

    public IReadOnlyCollection<ShoppingCartItem> Items => items.AsReadOnly();
}
