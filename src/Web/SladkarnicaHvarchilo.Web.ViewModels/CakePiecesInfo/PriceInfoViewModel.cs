namespace SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class PriceInfoViewModel : IMapTo<PriceInfo>
    {
        public PriceInfoViewModel() => this.Id = Guid.NewGuid().ToString();

        public string Id { get; set; }

        public string PastryId { get; set; }

        [Range(GlobalConstants.PastryValidationConstants.PiecesMinValue, GlobalConstants.PastryValidationConstants.PiecesMaxValue)]
        public int? Pieces { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Range(GlobalConstants.PastryValidationConstants.PriceMinValue, GlobalConstants.PastryValidationConstants.PriceMaxValue)]
        public decimal? Price { get; set; }
    }
}
