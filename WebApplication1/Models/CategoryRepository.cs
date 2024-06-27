using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public CategoryRepository(BethanysPieShopDbContext bethanysPieShopDBContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDBContext;
        }

        public IEnumerable<Category> AllCategories => _bethanysPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
        //=
        //public IEnumerable<Category> AllCategories
        //{
        //    get
        //    {
        //        return _bethanysPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
        //    }
        //}

        //another version to get the pies of a certain category using CategoryRepo and not PieRepo
        //public Category SpecialCategorie => _bethanysPieShopDbContext.Categories.Include(x => x.Pies).Where(x => x.CategoryName == "Cheese Cakes").FirstOrDefault();
    }
}
