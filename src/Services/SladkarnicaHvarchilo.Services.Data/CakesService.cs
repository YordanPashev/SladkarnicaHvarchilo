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

        public bool CheckIfCakeHasBeenEdited(Cake cakeBeforeEdit, Cake userIputCakeData)
        {
            if (cakeBeforeEdit.Name == userIputCakeData.Name && cakeBeforeEdit.Description == userIputCakeData.Description &&
                cakeBeforeEdit.Ingredients == userIputCakeData.Ingredients && cakeBeforeEdit.Pieces == userIputCakeData.Pieces &&
                cakeBeforeEdit.Allergens == userIputCakeData.Allergens && cakeBeforeEdit.Price == userIputCakeData.Price &&
                cakeBeforeEdit.ImageFileName == userIputCakeData.ImageFileName)
            {
                return false;
            }

            return true;
        }

        public async Task DeteleCake(Cake cake)
        {
            this.cakeRepo.Delete(cake);

            await this.cakeRepo.SaveChangesAsync();
        }

        public IQueryable<Cake> GetAllCakesInSale(string orderCriteria)
        {
            IQueryable<Cake> cakes = this.cakeRepo.AllAsNoTracking();
            IQueryable<Cake> orderedCakes = this.OrderByCriteria(orderCriteria, cakes);

            return orderedCakes;
        }

        public async Task<Cake> GetCakeByIdAsync(string id)
            => await this.cakeRepo.AllAsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Cake> GetCakeByIdForEditAsync(string id)
            => await this.cakeRepo.All()
                            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task UpdateCakeDataAsync(Cake cakeBeforeEdit, Cake userIputCakeData)
        {
            cakeBeforeEdit.Name = userIputCakeData.Name;
            cakeBeforeEdit.Description = userIputCakeData.Description;
            cakeBeforeEdit.Ingredients = userIputCakeData.Ingredients;
            cakeBeforeEdit.Pieces = userIputCakeData.Pieces;
            cakeBeforeEdit.Allergens = userIputCakeData.Allergens;
            cakeBeforeEdit.Price = userIputCakeData.Price;
            cakeBeforeEdit.ImageFileName = userIputCakeData.ImageFileName;

            await this.cakeRepo.SaveChangesAsync();
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
