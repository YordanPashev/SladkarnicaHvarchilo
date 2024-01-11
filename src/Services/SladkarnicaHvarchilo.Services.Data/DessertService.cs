namespace SladkarnicaHvarchilo.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SladkarnicaHvarchilo.Data.Common.Repositories;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Data.Contracts;

    using static SladkarnicaHvarchilo.Common.GlobalConstants;

    public class DessertService : IDessertService
    {
        private readonly IDeletableEntityRepository<Dessert> dessertRepo;

        public DessertService(IDeletableEntityRepository<Dessert> cakeRepo)
            => this.dessertRepo = cakeRepo;

        public async Task AddNewCake(Dessert cake)
        {
            await this.dessertRepo.AddAsync(cake);
            await this.dessertRepo.SaveChangesAsync();
        }

        public async Task<bool> CheckIfCakeAlreadyExists(string cakeName)
            => await this.dessertRepo.AllAsNoTracking()
                            .AnyAsync(c => c.Name == cakeName);

        public bool CheckIfCakeHasBeenEdited(Dessert cakeBeforeEdit, Dessert userIputCakeData)
        {
            if (cakeBeforeEdit.Name == userIputCakeData.Name && cakeBeforeEdit.Description == userIputCakeData.Description &&
                cakeBeforeEdit.Ingredients == userIputCakeData.Ingredients && cakeBeforeEdit.Allergens == userIputCakeData.Allergens &&
                cakeBeforeEdit.ImageFileDirectoryPath == userIputCakeData.ImageFileDirectoryPath)
            {
                return false;
            }

            return true;
        }

        public async Task DeteleCake(Dessert cake)
        {
            this.dessertRepo.Delete(cake);

            await this.dessertRepo.SaveChangesAsync();
        }

        public IQueryable<Dessert> GetAllCakesInSale()
            => this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .Include(c => c.NutritionInfo)
                            .OrderBy(c => c.Name);

        public async Task<Dessert> GetCakeByIdAsync(string id)
            => await this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .Include(c => c.NutritionInfo)
                            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Dessert> GetCakeByIdForEditAsync(string id)
            => await this.dessertRepo.All()
                            .FirstOrDefaultAsync(c => c.Id == id);

        public IQueryable<Dessert> GetCakesAccoringToFilters(string selectedOrderCriteria, string searchQuery)
            => this.GetCakesByOrderCriteria(selectedOrderCriteria)
                        .Include(c => c.PriceInfo)
                        .Include(c => c.NutritionInfo)
                        .Where(c => c.Name.ToUpper().Contains(searchQuery));

        public IQueryable<Dessert> GetCakesByOrderCriteria(string selectedOrderCriteria)
        {
            if (selectedOrderCriteria == OrderCriteria.PriceAscending)
            {
                return this.dessertRepo.AllAsNoTracking()
                            //.OrderBy(c => c.Price)
                            .OrderBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.PriceDescending)
            {
                return this.dessertRepo.AllAsNoTracking()
                            //.OrderByDescending(c => c.Price)
                            .OrderBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.Pieces)
            {
                return this.dessertRepo.AllAsNoTracking()
                            //.OrderByDescending(c => c.Pieces)
                            .OrderBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.Recent)
            {
                return this.dessertRepo.AllAsNoTracking()
                            .OrderByDescending(c => c.CreatedOn)
                            .ThenBy(c => c.Name);
            }

            return this.dessertRepo.AllAsNoTracking().OrderBy(c => c.Name);
        }

        public IQueryable<Dessert> GetSearchedCakes(string searchQuery)
            => this.dessertRepo.AllAsNoTracking()
                            .Where(c => c.Name.ToUpper().Contains(searchQuery))
                            .OrderBy(c => c.Name);

        public async Task UpdateCakeDataAsync(Dessert cakeBeforeEdit, Dessert userIputCakeData)
        {
            cakeBeforeEdit.Name = userIputCakeData.Name;
            cakeBeforeEdit.Description = userIputCakeData.Description;
            cakeBeforeEdit.Ingredients = userIputCakeData.Ingredients;
            cakeBeforeEdit.Allergens = userIputCakeData.Allergens;
            cakeBeforeEdit.ImageFileDirectoryPath = userIputCakeData.ImageFileDirectoryPath;

            await this.dessertRepo.SaveChangesAsync();
        }
    }
}
