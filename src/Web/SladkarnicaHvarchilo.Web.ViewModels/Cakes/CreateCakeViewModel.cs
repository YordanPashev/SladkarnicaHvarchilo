namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo;

    public class CreateCakeViewModel : CakeDataViewModel, IMapTo<Cake>
    {
        public CreateCakeViewModel()
        {
            this.NutritionInfo = new CreateNutritionInfoViewModel();
            this.Id = Guid.NewGuid().ToString();
            this.Category = FoodTastingCategory.Sweet;
        }
    }
}
