namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Services.Mapping;
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;

    public class EditCakeViewModel : CakeFullDataViewModel, IMapFrom<Dessert>, IMapTo<Dessert>
    {
        public EditCakeViewModel()
        {
            this.Category = FoodTastingCategory.Sweet;
            this.Type = DessertType.Cake;
            this.PriceInfo = new List<PriceInfoViewModel>()
            {
                new PriceInfoViewModel(),
                new PriceInfoViewModel(),
            };
        }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
