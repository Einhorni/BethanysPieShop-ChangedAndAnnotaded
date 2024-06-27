using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BethanysPieShop.Models;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        //readonly instances
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController (IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        //public IActionResult List()
        //{
        //    //ViewBag.CurrentCategory = "Cheese cakes";
        //    //return View(_pieRepository.AllPies);
        //    PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "All pies");
        //    return View(pieListViewModel);
        //}

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }
            //getestet: List_EmptyCategory_returnsAllPies() - ViewResult -> did we arrive here and return a View result?
            return View(new PieListViewModel(pies, currentCategory));
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if(pie == null)
                //Not Found is a method defined in the controller base and will return a NOT Found result
                return NotFound();
            return View(pie);
        }

        //the following is for the usage of jquery and the ajax call
        //all the functionality is going to be handled purely in a client-side way (using jquery)
        public IActionResult Search ()
        { return View(); }

        //legt fest unter welcher Route die Methode erreichbar sein soll --> https://localhost:7081/BlazorSearch
        //unter der normalen Route: https://localhost:7081/Pie/BlazorSearch -->
        //  findet er die blazor.web.js der neu angelegten View Blazor Search nicht
        [Route("/BlazorSearch")]
        public IActionResult BlazorSearch()
        { return View(); }
    }
}
