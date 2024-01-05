namespace SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo
{
    using System;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class CreateNutritionInfoViewModel : NutritionInfoDataViewModel, IMapTo<NutritionInfo>
    {
        public CreateNutritionInfoViewModel() => this.Id = Guid.NewGuid().ToString();
    }
}
