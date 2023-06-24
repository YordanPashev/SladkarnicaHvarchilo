namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class CakeDetailsViewModel : IMapFrom<Cake>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public int Pieces { get; set; }

        public string Allergens { get; set; }

        public decimal Price { get; set; }

        public string ImageFileName { get; set; }

        public FoodTastingCategory Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
