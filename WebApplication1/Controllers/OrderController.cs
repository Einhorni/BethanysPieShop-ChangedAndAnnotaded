using BethanysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    //the user has to be signed in in order to access any of the actions defined here
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private IShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        //this action method is invoked, when the page is loaded - when the browser makes the request to order/checkout //Get-request is received
        //[HttpGet]     --> default, could be added but hasn't to
        public IActionResult Checkout()
        {
            return View();
        }

        //is called when a post is received
        [HttpPost]
        //Order order ist created by ASp.NEt Core through model binding before the Action method is invoked
        public IActionResult Checkout(Order order)
        {
            //eingefügt, bevor er irgendwas aus der Datenbank anfragt
            if (!ModelState.IsValid)
            { return View(); }
            
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                //ModelState is a byproduct of Model Binding - any error that have ocurred will appear there
                //own Error can be added:
                ModelState.AddModelError("", "Your cart is empty, add some pies first.");
            }

            //server-side-validation on all props of the object
            if (ModelState.IsValid)
            {
                //adding order to db
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                //redirect to checkout complete view
                return RedirectToAction("CheckoutComplete");
            }
            //else return the same checkout-view --> repopulate fields entered by the user
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our delicious pies!";
            return View();
        }
    }
}
