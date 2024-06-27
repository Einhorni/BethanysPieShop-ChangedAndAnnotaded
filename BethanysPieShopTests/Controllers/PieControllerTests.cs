using BethanysPieShop.Controllers;
using BethanysPieShop.ViewModels;
using BethanysPieShopTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShopTests.Controllers
{
    //classes müssen public sein
    public class PieControllerTests
    {
        [Fact]
        public void List_EmptyCategory_returnsAllPies()
        { 
            //arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //act
            var result = pieController.List("");

            //assert

            //result VS. Type VieResult: wird hier ein ViewResult zurückgegeben? --> Assert.IsType // stimmt der Ausgabetyp?
            //Original: return -->View<--(new PieListViewModel(pies, currentCategory));
            var viewResult = Assert.IsType<ViewResult>(result);
            //viewResult.ViewData.Model VS. PieListViewModel: ist das model, das wir in die Endfunktion eingeben der richtige Typ?
            //Original: return View(new -->PieListViewModel<-- (pies, currentCategory));
            //der Typ in () wird erst während der Laufzeit des Programms gesetzt, deswegen sieht man den richtigen Typen nicht, wenn man über dber das Wort hovert
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model); //oder nur viewResult.Model
            //ist das Ergebnis richtig?
            Assert.Equal(10, pieListViewModel.Pies.Count());
        }

        
    }
}
