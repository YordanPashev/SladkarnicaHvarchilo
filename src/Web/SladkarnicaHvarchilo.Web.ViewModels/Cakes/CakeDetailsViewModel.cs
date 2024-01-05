namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo;

    public class CakeDetailsViewModel : IMapFrom<Dessert>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public int Pieces { get; set; }

        public string Allergens { get; set; }

        public decimal Price { get; set; }

        public string ImageFileDirectoryPath { get; set; }

        public FoodTastingCategory Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public NutritionInfoViewModel NutritionInfo { get; set; }
    }
}
