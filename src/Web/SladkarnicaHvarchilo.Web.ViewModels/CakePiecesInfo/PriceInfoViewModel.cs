namespace SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class PriceInfoViewModel : IMapTo<PriceInfo>, IMapFrom<PriceInfo>
    {
        public PriceInfoViewModel() => this.Id = Guid.NewGuid().ToString();

        public string Id { get; set; }

        public string DessertId { get; set; }

        [Range(GlobalConstants.PastryValidationConstants.PiecesMinValue, GlobalConstants.PastryValidationConstants.PiecesMaxValue)]
        public int? Pieces { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Range(GlobalConstants.PastryValidationConstants.PriceMinValue, GlobalConstants.PastryValidationConstants.PriceMaxValue)]
        public decimal? Price { get; set; }

        public string FormatedPrice => this.FormatPrice();

        private string FormatPrice()
        {
            int parsedPrice = 0;

            if (int.TryParse(this.Price.ToString().Trim('0').Trim('.'), out parsedPrice))
            {
                return parsedPrice.ToString();
            }

            return this.Price.ToString().Replace('.', ',');
        }
    }
}
