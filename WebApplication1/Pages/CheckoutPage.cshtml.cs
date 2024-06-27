using BethanysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BethanysPieShop.Pages
{
    //needed for the RazorPage Authorization (together with registering service in program.cs
    [Authorize]
    public class CheckoutPageModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private IShoppingCart _shoppingCart;

        public CheckoutPageModel(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        [BindProperty]

        //the property can be used in the razor page UI
        public Order Order { get; set; }
        public void OnGet()
        {
        }

        //no parameters, because we get acces via the order property
        public IActionResult OnPost() 
        {
            //eingefügt, bevor er irgendwas aus der Datenbank anfragt
            if (!ModelState.IsValid)
            { return Page(); }

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0 || _shoppingCart.ShoppingCartItems == null)
            {
                //ModelState is a byproduct of Model Binding - any error that have ocurred will appear there
                //own Error can be added:
                ModelState.AddModelError("", "Your cart is empty, add some pies first.");
            }

            //server-side-validation on all props of the object
            if (ModelState.IsValid)
            {
                //adding order to db
                _orderRepository.CreateOrder(Order);
                _shoppingCart.ClearCart();
                //redirect to checkout complete view
                return RedirectToAction("CheckoutCompletePage");
            }
            //else return the same checkout-view --> repopulate fields entered by the user
            return Page();
        }
    }
}
