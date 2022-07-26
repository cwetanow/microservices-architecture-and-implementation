namespace Basket.WebApi.Entities;

public class ShoppingCart
{
    public string User { get; set; } = string.Empty;

    public IEnumerable<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
}