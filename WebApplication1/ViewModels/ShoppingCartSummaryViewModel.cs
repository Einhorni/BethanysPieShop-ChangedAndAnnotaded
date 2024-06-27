using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    public class ShoppingCartSummaryViewModel
    {
        public int ShoppingCartTotalItems { get; }

        public ShoppingCartSummaryViewModel(int shoppingCartTotalItems)
        {
            ShoppingCartTotalItems = shoppingCartTotalItems;
        }
    }
}
