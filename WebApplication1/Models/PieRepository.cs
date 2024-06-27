using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        //Di from DB content because data is not mocked anymore
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;



        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                //this will launch a query, asking for all the record in the pies table + Include information about the category for each entity
                //include ist dafür verantwortlich, dass beim aufruf einer pieId über die api, die Kategorien mitsamt der pies dieser Kategorie mitgeladen werden.
                //--> bessere Lösung return _bethanysPieShopDbContext.Pies;
                //und Filterung der Pies dann über das CategoryRepo, da hier ja sowieso die Pies einer Cat. erreichbar sind.
                return _bethanysPieShopDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bethanysPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _bethanysPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _bethanysPieShopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }
    }
}
