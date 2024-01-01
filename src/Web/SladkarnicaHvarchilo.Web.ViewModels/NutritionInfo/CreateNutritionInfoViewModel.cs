namespace SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo
{
    using System;

    public class CreateNutritionInfoViewModel : NutritionInfoDataViewModel
    {
        public CreateNutritionInfoViewModel() => this.Id = Guid.NewGuid().ToString();
    }
}
