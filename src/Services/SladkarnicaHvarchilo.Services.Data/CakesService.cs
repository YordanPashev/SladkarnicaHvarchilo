namespace SladkarnicaHvarchilo.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SladkarnicaHvarchilo.Data.Common.Repositories;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Data.Contracts;

    using static SladkarnicaHvarchilo.Common.GlobalConstants;

    public class CakesService : ICakesService
    {
        private readonly IDeletableEntityRepository<Cake> cakeRepo;

        public CakesService(IDeletableEntityRepository<Cake> cakeRepo)
            => this.cakeRepo = cakeRepo;

        public async Task AddNewCake(Cake cake)
        {
            await this.cakeRepo.AddAsync(cake);
            await this.cakeRepo.SaveChangesAsync();
        }

        public async Task<bool> CheckIfCakeAlreadyExists(string cakeName)
            => await this.cakeRepo.AllAsNoTracking()
                            .AnyAsync(c => c.Name == cakeName);

        public IQueryable<Cake> GettAllCakesInSale(string orderCriteria)
        {
            IQueryable<Cake> cakes = this.cakeRepo.AllAsNoTracking();

            if (string.IsNullOrEmpty(orderCriteria))
            {
                return cakes.OrderBy(c => c.Name);
            }

            IQueryable<Cake> orderedCakes = this.OrderByCriteria(orderCriteria, cakes);

            return orderedCakes;
        }

        private IQueryable<Cake> OrderByCriteria(string orderCriteria, IQueryable<Cake> cakes)
        {
            if (orderCriteria == OrderCriteria.PriceAscending)
            {
                return cakes.OrderByDescending(c => c.Price)
                            .ThenBy(c => c.Name);
            }
            else if (orderCriteria == OrderCriteria.PriceDescending)
            {
                return cakes.OrderByDescending(c => c.Price)
                            .ThenBy(c => c.Name);
            }
            else if (orderCriteria == OrderCriteria.Pieces)
            {
                return cakes.OrderBy(c => c.Pieces)
                            .ThenBy(c => c.Name);
            }
            else if (orderCriteria == OrderCriteria.Recent)
            {
                return cakes.OrderByDescending(c => c.CreatedOn)
                            .ThenBy(c => c.Name);
            }

            return cakes.OrderBy(c => c.Name);
        }
    }
}
