namespace SladkarnicaHvarchilo.Data.Models.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Models.Enums;

    public interface IPastry
    {
        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.IngredientsMaxLength)]
        public string Ingredients { get; set; }

        [MaxLength(GlobalConstants.DessertsValidationConstants.AllergensMaxLength)]
        public string Allergens { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(GlobalConstants.DessertsValidationConstants.PriceMinValue, GlobalConstants.DessertsValidationConstants.PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.ImageFileDirectoryPathMaxLength)]
        public string ImageFileDirectoryPath { get; set; }

        [Required]
        public FoodTastingCategory Category { get; set; }

        [Required]
        [ForeignKey(nameof(NutritionInfo))]
        public string NutritionInfoId { get; set; }

        public NutritionInfo NutritionInfo { get; set; }
    }
}
