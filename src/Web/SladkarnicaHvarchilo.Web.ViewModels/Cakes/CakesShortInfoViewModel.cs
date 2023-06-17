namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class CakesShortInfoViewModel : IMapFrom<Cake>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Pieces { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
