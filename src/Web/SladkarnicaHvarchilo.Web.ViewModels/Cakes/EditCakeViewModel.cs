namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class EditCakeViewModel : CakeDataViewModel, IMapFrom<Cake>, IMapTo<Cake>
    {
        public EditCakeViewModel() => this.Category = FoodTastingCategory.Sweet;
    }
}
