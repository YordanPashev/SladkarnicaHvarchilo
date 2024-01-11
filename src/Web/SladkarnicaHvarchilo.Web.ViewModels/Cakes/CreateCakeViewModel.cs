namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;
    using SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo;

    public class CreateCakeViewModel : CakeFullDataViewModel, IMapTo<Dessert>
    {
        public CreateCakeViewModel()
        {
            this.NutritionInfo = new CreateNutritionInfoViewModel();
            this.Id = Guid.NewGuid().ToString();
            this.Category = FoodTastingCategory.Sweet;
            this.Type = DessertType.Cake;
            this.PriceInfo = new List<PriceInfoViewModel>()
            {
                new PriceInfoViewModel(),
                new PriceInfoViewModel(),
            };
        }

        [Required]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
