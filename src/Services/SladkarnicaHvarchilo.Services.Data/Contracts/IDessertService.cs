namespace SladkarnicaHvarchilo.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Models;

    public interface IDessertService
    {
        Task AddNewCake(Dessert cake);

        Task<bool> CheckIfCakeAlreadyExists(string cakeName);

        Task DeteleCake(Dessert cake);

        IQueryable<Dessert> GetAllCakesInSale();

        Task<Dessert> GetCakeByIdAsync(string id);

        Task<Dessert> GetCakeByIdForEditAsync(string id);

        IQueryable<Dessert> GetCakesAccoringToFilters(string selectedOrderCriteria, string searchQuery);

        IQueryable<Dessert> GetCakesByOrderCriteria(string selectedOrderCriteria);

        IQueryable<Dessert> GetSearchedCakes(string searchQuery);

        Task UpdateCakeDataAsync(Dessert originalCake, Dessert userIputCakeData);
    }
}
