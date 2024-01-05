namespace SladkarnicaHvarchilo.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Common.Repositories;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Data.Contracts;

    public class CakePiecesInfoService : IDessertPiecesInfoService
    {
        private readonly IDeletableEntityRepository<PriceInfo> cakePiecesInfoRepo;

        public CakePiecesInfoService(IDeletableEntityRepository<PriceInfo> cakePiecesInfoRepo)
            => this.cakePiecesInfoRepo = cakePiecesInfoRepo;

        public async Task AddDesserrtPiecesInfo(List<PriceInfo> piecesInfo, string id)
        {
            foreach (var currentPicesInfo in piecesInfo)
            {
                if (currentPicesInfo.Pieces > 0 && currentPicesInfo.Price > 0.00m)
                {
                    await this.cakePiecesInfoRepo.AddAsync(currentPicesInfo);
                }
            }

            await this.cakePiecesInfoRepo.SaveChangesAsync();
        }
    }
}
