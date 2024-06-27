using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BethanysPieShop.Controllers.Api
{
    [Route("api/[controller]")]
    //optional
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        //a get request will be directed here
        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = _pieRepository.AllPies;
            //in the following allPies is automatically converted into json --> default in Asp.Net Core
            //original: return Ok(allPies);
            //durch ToList() wird der DB Aufruf direkt in ein JSON umgewandelt --> Verhinderung Probleme, falls der DbContext verschwindet
            return Ok(allPies.ToList());
        }

        //[HttpGet] would through an error - just one of [Http] is allowd
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //durch Any wird direkt in ein JSON umgewandelt, deswegen ToList() nicht notwendig
            if (!_pieRepository.AllPies.Any(p => p.PieId == id))
                return NotFound();

            //again to List to transform directly to json
            return Ok(_pieRepository.AllPies.Where(p => p.PieId == id).ToList());
        }

        [HttpPost]
        //[FromBody] damit Asp.Net Core weiß, wo er den Suchstring findet
        public IActionResult SearchPies([FromBody]string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();

            //the following cannot be tested through the browser, because there is a post --> use postman
            //original: if (!string.IsNullOrEmpty(searchQuery))
            if (!searchQuery.IsNullOrEmpty())
            {
                pies = _pieRepository.SearchPies(searchQuery);
                //result automatically contains sc 200
            }
            return new JsonResult(pies);         
        }

        ////alt w/o instanciating empty List
        //public IActionResult SearchPiesAlt1([FromBody] string searchQuery)
        //{
        //    if (!searchQuery.IsNullOrEmpty())
        //    {
        //        return new JsonResult(_pieRepository.SearchPies(searchQuery));
        //        //result automatically contains sc 200
        //    }
        //    return new JsonResult(new List<Pie>());
        //}

        ////alt w/o instanciating empty List using the ternary operator
        //public IActionResult SearchPiesAlt2([FromBody] string searchQuery)
        //{
        //    var result =
        //        !searchQuery.IsNullOrEmpty() 
        //        ? _pieRepository.SearchPies(searchQuery).ToList()
        //        : new List<Pie>();
            
        //    return new JsonResult(result);

        //}
    }
}
