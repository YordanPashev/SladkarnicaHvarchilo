namespace SladkarnicaHvarchilo.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Models;

    public interface ICakesService
    {
        Task AddNewCake(Cake cake);

        Task<bool> CheckIfCakeAlreadyExists(string cakeName);

        bool CheckIfCakeHasBeenEdited(Cake cakeBeforeEdit, Cake userIputCakeData);

        Task DeteleCake(Cake cake);

        IQueryable<Cake> GetAllCakesInSale();

        Task<Cake> GetCakeByIdAsync(string id);

        Task<Cake> GetCakeByIdForEditAsync(string id);

        IQueryable<Cake> GetCakesAccoringToFilters(string selectedOrderCriteria, string searchQuery);

        IQueryable<Cake> GetCakesByOrderCriteria(string selectedOrderCriteria);

        IQueryable<Cake> GetSearchedCakes(string searchQuery);

        Task UpdateCakeDataAsync(Cake cakeBeforeEdit, Cake userIputCakeData);
    }
}
