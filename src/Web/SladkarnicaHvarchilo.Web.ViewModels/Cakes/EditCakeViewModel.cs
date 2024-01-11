namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System.Collections.Generic;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;

    public class EditCakeViewModel : CakeFullDataViewModel, IMapFrom<Dessert>, IMapTo<Dessert>
    {
        public EditCakeViewModel()
        {
            this.Category = FoodTastingCategory.Sweet;
            this.PriceInfo = new List<PriceInfoViewModel>()
            {
                new PriceInfoViewModel(),
                new PriceInfoViewModel(),
            };
        }
    }
}
