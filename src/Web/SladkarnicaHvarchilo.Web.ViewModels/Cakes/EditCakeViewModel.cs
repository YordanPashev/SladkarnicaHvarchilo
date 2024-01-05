namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class EditCakeViewModel : CakeDataViewModel, IMapFrom<Dessert>, IMapTo<Dessert>
    {
        public EditCakeViewModel() => this.Category = FoodTastingCategory.Sweet;
    }
}
