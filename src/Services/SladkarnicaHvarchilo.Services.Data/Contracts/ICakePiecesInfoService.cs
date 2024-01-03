namespace SladkarnicaHvarchilo.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Models;

    public interface ICakePiecesInfoService
    {
        Task AddCakePiecesInfo(List<PriceInfo> piecesInfo);
    }
}
