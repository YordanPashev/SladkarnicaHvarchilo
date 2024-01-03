namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;
    using System.Collections.Generic;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;
    using SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo;

    public class CreateCakeViewModel : CakeDataViewModel, IMapTo<Cake>
    {
        public CreateCakeViewModel()
        {
            this.NutritionInfo = new CreateNutritionInfoViewModel();
            this.Id = Guid.NewGuid().ToString();
            this.Category = FoodTastingCategory.Sweet;
            this.PriceInfo = new List<PriceInfoViewModel>()
            {
                new PriceInfoViewModel(),
                new PriceInfoViewModel(),
            };
        }
    }
}
