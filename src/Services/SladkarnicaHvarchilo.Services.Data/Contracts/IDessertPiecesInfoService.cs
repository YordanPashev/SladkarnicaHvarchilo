namespace SladkarnicaHvarchilo.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Models;

    public interface IDessertPiecesInfoService
    {
        Task AddDesserrtPiecesInfo(List<PriceInfo> cakePiecesInfo, string id);
    }
}
