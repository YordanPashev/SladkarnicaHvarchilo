namespace SladkarnicaHvarchilo.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;

    public interface IDessertService
    {
        Task AddNewDessert(Dessert cake);

        Task<bool> CheckIfDessertAlreadyExists(string cakeName, DessertType type);

        Task DeleteDessert(Dessert cake);

        IQueryable<Dessert> GetAllDesserInSaleByType(DessertType type);

        Task<Dessert> GetDessertByIdAsync(string id, DessertType type);

        Task<Dessert> GetDessertByIdForEditAsync(string id, DessertType type);

        IQueryable<Dessert> GetDessertsAccoringToFilters(string selectedOrderCriteria, string searchQuery, DessertType type);

        IQueryable<Dessert> GetDessertByOrderCriteria(string selectedOrderCriteria, DessertType type);

        IQueryable<Dessert> GetDessertsContainingTheQuery(string searchQuery, DessertType type);

        Task UpdateDessertDataAsync(Dessert originalCake, Dessert userIputCakeData);
    }
}
