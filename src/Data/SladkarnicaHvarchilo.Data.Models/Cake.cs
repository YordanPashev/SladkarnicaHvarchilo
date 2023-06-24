namespace SladkarnicaHvarchilo.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Common.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;

    public class Cake : BaseDeletableModel<int>
    {
        public Cake() => this.Id = Guid.NewGuid().ToString();

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.IngredientsMaxLength)]
        public string Ingredients { get; set; }

        [Required]
        public int Pieces { get; set; }

        [MaxLength(GlobalConstants.DessertsValidationConstants.AllergensMaxLength)]
        public string Allergens { get; set; }

        [Required]
        [Range(GlobalConstants.DessertsValidationConstants.PriceMinValue, GlobalConstants.DessertsValidationConstants.PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DessertsValidationConstants.ImageFileDirectoryPathMaxLength)]
        public string ImageFileName { get; set; }

        [Required]
        public FoodTastingCategory Category { get; set; }
    }
}
