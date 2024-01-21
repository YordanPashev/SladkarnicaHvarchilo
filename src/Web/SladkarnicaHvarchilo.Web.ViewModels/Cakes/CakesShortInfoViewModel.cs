namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System.Collections.Generic;

    using AutoMapper;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;

    public class CakesShortInfoViewModel : IMapFrom<Dessert>, IMapTo<Dessert>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageFileDirectoryPath { get; set; }

        public List<PriceInfoViewModel> PriceInfo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<PriceInfo, PriceInfoViewModel>();
    }
}
