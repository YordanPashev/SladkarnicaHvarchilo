namespace SladkarnicaHvarchilo.Web.ViewModels.Cakes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models.Enums;
    using SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo;

    public class CakeDataViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.DessertsValidationConstants.NameMinLength)]
        [MaxLength(GlobalConstants.DessertsValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.DessertsValidationConstants.DescriptionMinLength)]
        [MaxLength(GlobalConstants.DessertsValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MinLength(GlobalConstants.DessertsValidationConstants.IngredientsMinLength)]
        [MaxLength(GlobalConstants.DessertsValidationConstants.IngredientsMaxLength)]
        public string Ingredients { get; set; }

        [Required]
        public int Pieces1 { get; set; }

        [Required]
        public int Pieces2 { get; set; }

        [MinLength(GlobalConstants.DessertsValidationConstants.AllergensMinLength)]
        [MaxLength(GlobalConstants.DessertsValidationConstants.AllergensMaxLength)]
        public string Allergens { get; set; }

        [Required]
        [Range(GlobalConstants.DessertsValidationConstants.PriceMinValue, GlobalConstants.DessertsValidationConstants.PriceMaxValue)]
        public decimal Price1 { get; set; }

        [Required]
        [Range(GlobalConstants.DessertsValidationConstants.PriceMinValue, GlobalConstants.DessertsValidationConstants.PriceMaxValue)]
        public decimal Price2 { get; set; }

        [Required]
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [MinLength(GlobalConstants.DessertsValidationConstants.ImageFileDirectoryPathMinLength)]
        [MaxLength(GlobalConstants.DessertsValidationConstants.ImageFileDirectoryPathMaxLength)]
        public string ImageFileName { get; set; }

        [Required]
        public FoodTastingCategory Category { get; set; }

        [Required]
        public NutritionInfoDataViewModel NutritionInfo { get; set; }
    }
}
