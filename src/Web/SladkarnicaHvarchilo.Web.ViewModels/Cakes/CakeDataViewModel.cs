namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Web.ViewModels.CakePiecesInfo;
    using SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo;

    public class CakeDataViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.PastryValidationConstants.NameMinLength)]
        [MaxLength(GlobalConstants.PastryValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.PastryValidationConstants.DescriptionMinLength)]
        [MaxLength(GlobalConstants.PastryValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MinLength(GlobalConstants.PastryValidationConstants.IngredientsMinLength)]
        [MaxLength(GlobalConstants.PastryValidationConstants.IngredientsMaxLength)]
        public string Ingredients { get; set; }

        [Required]
        public List<PriceInfoViewModel> PriceInfo { get; set; }

        [MinLength(GlobalConstants.PastryValidationConstants.AllergensMinLength)]
        [MaxLength(GlobalConstants.PastryValidationConstants.AllergensMaxLength)]
        public string Allergens { get; set; }

        [Required]
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [MinLength(GlobalConstants.PastryValidationConstants.ImageFileDirectoryPathMinLength)]
        [MaxLength(GlobalConstants.PastryValidationConstants.ImageFileDirectoryPathMaxLength)]
        public string ImageFileName { get; set; }

        [Required]
        public FoodTastingCategory Category { get; set; }

        [Required]
        public NutritionInfoDataViewModel NutritionInfo { get; set; }
    }
}
