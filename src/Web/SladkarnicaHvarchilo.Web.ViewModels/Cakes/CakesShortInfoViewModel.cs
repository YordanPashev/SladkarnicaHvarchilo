namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;
    using System.Collections.Generic;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;

    public class CakesShortInfoViewModel : IMapFrom<Dessert>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Pieces { get; set; }

        public string ImageFileDirectoryPath { get; set; }

        public List<PriceInfoViewModel> PirceInfo { get; set; }
    }
}
