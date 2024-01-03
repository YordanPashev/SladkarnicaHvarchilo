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
                cakeBeforeEdit.Ingredients == userIputCakeData.Ingredients && cakeBeforeEdit.Allergens == userIputCakeData.Allergens &&
                cakeBeforeEdit.ImageFileDirectoryPath == userIputCakeData.ImageFileDirectoryPath)
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

        public IQueryable<Cake> GetAllCakesInSale()
            => this.cakeRepo.AllAsNoTracking().OrderBy(c => c.Name);

        public async Task<Cake> GetCakeByIdAsync(string id)
            => await this.cakeRepo.AllAsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Cake> GetCakeByIdForEditAsync(string id)
            => await this.cakeRepo.All()
                            .FirstOrDefaultAsync(c => c.Id == id);

        public IQueryable<Cake> GetCakesAccoringToFilters(string selectedOrderCriteria, string searchQuery)
            => this.GetCakesByOrderCriteria(selectedOrderCriteria)
                        .Where(c => c.Name.ToUpper().Contains(searchQuery));

        public IQueryable<Cake> GetCakesByOrderCriteria(string selectedOrderCriteria)
        {
            if (selectedOrderCriteria == OrderCriteria.PriceAscending)
            {
                return this.cakeRepo.AllAsNoTracking()
                            //.OrderBy(c => c.Price)
                            .OrderBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.PriceDescending)
            {
                return this.cakeRepo.AllAsNoTracking()
                            //.OrderByDescending(c => c.Price)
                            .OrderBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.Pieces)
            {
                return this.cakeRepo.AllAsNoTracking()
                            //.OrderByDescending(c => c.Pieces)
                            .OrderBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.Recent)
            {
                return this.cakeRepo.AllAsNoTracking()
                            .OrderByDescending(c => c.CreatedOn)
                            .ThenBy(c => c.Name);
            }

            return this.cakeRepo.AllAsNoTracking().OrderBy(c => c.Name);
        }

        public IQueryable<Cake> GetSearchedCakes(string searchQuery)
            => this.cakeRepo.AllAsNoTracking()
                            .Where(c => c.Name.ToUpper().Contains(searchQuery))
                            .OrderBy(c => c.Name);

        public async Task UpdateCakeDataAsync(Cake cakeBeforeEdit, Cake userIputCakeData)
        {
            cakeBeforeEdit.Name = userIputCakeData.Name;
            cakeBeforeEdit.Description = userIputCakeData.Description;
            cakeBeforeEdit.Ingredients = userIputCakeData.Ingredients;
            cakeBeforeEdit.Allergens = userIputCakeData.Allergens;
            cakeBeforeEdit.ImageFileDirectoryPath = userIputCakeData.ImageFileDirectoryPath;

            await this.cakeRepo.SaveChangesAsync();
        }
    }
}
