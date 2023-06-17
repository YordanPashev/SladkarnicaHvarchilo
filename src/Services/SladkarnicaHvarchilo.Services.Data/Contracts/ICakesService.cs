namespace SladkarnicaHvarchilo.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Models;

    public interface ICakesService
    {
        Task AddNewCake(Cake cake);

        Task<bool> CheckIfCakeAlreadyExists(string cakeName);

        IQueryable<Cake> GettAllCakesInSale(string orderCriteria);
    }
}
