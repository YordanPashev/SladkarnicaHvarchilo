namespace SladkarnicaHvarchilo.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

        public async Task DeteleCake(Dessert cake)
        {
            this.dessertRepo.Delete(cake);

            await this.dessertRepo.SaveChangesAsync();
        }

        public IQueryable<Dessert> GetAllCakesInSale()
            => this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .OrderBy(c => c.Name);

        public async Task<Dessert> GetCakeByIdAsync(string id)
            => await this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .Include(c => c.NutritionInfo)
                            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Dessert> GetCakeByIdForEditAsync(string id)
            => await this.dessertRepo.All()
                            .Include(c => c.PriceInfo)
                            .Include(c => c.NutritionInfo)
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
                            .Include(c => c.PriceInfo)
                            .OrderBy(c => c.PriceInfo.OrderBy(c => c.Price)
                                                     .Take(1)
                                                     .Max(pi => pi.Price))
                            .ThenBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.PriceDescending)
            {
                return this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .OrderByDescending(c => c.PriceInfo.OrderByDescending(c => c.Price)
                                                     .Take(1)
                                                     .Max(pi => pi.Price))
                            .ThenBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.PiecesAscending)
            {
                return this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .OrderBy(c => c.PriceInfo.OrderBy(c => c.Pieces)
                                                     .Take(1)
                                                     .Max(pi => pi.Pieces))
                            .ThenBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.PiecesDescending)
            {
                return this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .OrderByDescending(c => c.PriceInfo.OrderByDescending(c => c.Pieces)
                                                     .Take(1)
                                                     .Max(pi => pi.Pieces))
                            .ThenBy(c => c.Name);
            }
            else if (selectedOrderCriteria == OrderCriteria.Recent)
            {
                return this.dessertRepo.AllAsNoTracking()
                            .Include(c => c.PriceInfo)
                            .OrderByDescending(c => c.CreatedOn)
                            .ThenBy(c => c.Name);
            }

            return this.dessertRepo.AllAsNoTracking().OrderBy(c => c.Name);
        }

        public IQueryable<Dessert> GetSearchedCakes(string searchQuery)
            => this.dessertRepo.AllAsNoTracking()
                            .Where(c => c.Name.ToUpper().Contains(searchQuery))
                            .OrderBy(c => c.Name);

        public async Task UpdateCakeDataAsync(Dessert originalCake, Dessert userIputCakeData)
        {
            originalCake.Name = userIputCakeData.Name;
            originalCake.Description = userIputCakeData.Description;
            originalCake.Ingredients = userIputCakeData.Ingredients;
            originalCake.Allergens = userIputCakeData.Allergens;
            originalCake.ImageFileDirectoryPath = userIputCakeData.ImageFileDirectoryPath;
            originalCake.NutritionInfo.Fat = userIputCakeData.NutritionInfo.Fat;
            originalCake.NutritionInfo.Carbs = userIputCakeData.NutritionInfo.Carbs;
            originalCake.NutritionInfo.Sugar = userIputCakeData.NutritionInfo.Sugar;
            originalCake.NutritionInfo.Protein = userIputCakeData.NutritionInfo.Protein;
            originalCake.NutritionInfo.Salt = userIputCakeData.NutritionInfo.Salt;

            this.UpdatePriceInfo(originalCake, userIputCakeData);

            await this.dessertRepo.SaveChangesAsync();
        }

        private void UpdatePriceInfo(Dessert originalCake, Dessert userIputCakeData)
        {
            List<PriceInfo> originalCakePriceInfo = originalCake.PriceInfo.ToList();

            List<PriceInfo> userInputCakePriceInfo = userIputCakeData.PriceInfo.ToList();
            int priceInfoCount = originalCakePriceInfo.Count;

            if (userInputCakePriceInfo.Count > originalCakePriceInfo.Count)
            {
                priceInfoCount = userInputCakePriceInfo.Count;
            }

            for (int priceInfIndex = 0; priceInfIndex < priceInfoCount; priceInfIndex++)
            {
                if (userInputCakePriceInfo.Count < priceInfIndex + 1)
                {
                    originalCakePriceInfo[priceInfIndex].IsDeleted = true;
                    originalCakePriceInfo[priceInfIndex].DeletedOn = DateTime.Now;
                }
                else
                {
                    if (originalCakePriceInfo.Count < priceInfIndex + 1)
                    {
                        PriceInfo priceInfo = new PriceInfo()
                        {
                            Id = userInputCakePriceInfo[priceInfIndex].Id,
                            DessertId = originalCake.Id,
                            Price = userInputCakePriceInfo[priceInfIndex].Price,
                            Pieces = userInputCakePriceInfo[priceInfIndex].Pieces,
                        };

                        originalCakePriceInfo.Add(priceInfo);
                    }
                    else
                    {
                        originalCakePriceInfo[priceInfIndex].Price = userInputCakePriceInfo[priceInfIndex].Price;
                        originalCakePriceInfo[priceInfIndex].Pieces = userInputCakePriceInfo[priceInfIndex].Pieces;
                    }
                }
            }

            originalCake.PriceInfo = originalCakePriceInfo;
        }
    }
}
