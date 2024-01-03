namespace SladkarnicaHvarchilo.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SladkarnicaHvarchilo.Data.Common.Repositories;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Data.Contracts;

    public class CakePiecesInfoService : ICakePiecesInfoService
    {
        private readonly IDeletableEntityRepository<PriceInfo> cakePiecesInfoRepo;

        public CakePiecesInfoService(IDeletableEntityRepository<PriceInfo> cakePiecesInfoRepo)
            => this.cakePiecesInfoRepo = cakePiecesInfoRepo;

        public async Task AddCakePiecesInfo(List<PriceInfo> piecesInfo)
        {
            foreach (var currentPicesInfo in piecesInfo)
            {
                await this.cakePiecesInfoRepo.AddAsync(currentPicesInfo);
            }

            await this.cakePiecesInfoRepo.SaveChangesAsync();
        }
    }
}
