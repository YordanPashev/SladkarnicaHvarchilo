namespace SladkarnicaHvarchilo.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SladkarnicaHvarchilo.Common;
    using SladkarnicaHvarchilo.Data.Common.Models;
    using SladkarnicaHvarchilo.Data.Models.Contracts;
    using SladkarnicaHvarchilo.Data.Models.Enums;

    public class Dessert : BaseDeletableModel<int>, IPastry
    {
        public Dessert() => this.Id = Guid.NewGuid().ToString();

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PastryValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PastryValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PastryValidationConstants.IngredientsMaxLength)]
        public string Ingredients { get; set; }

        [MaxLength(GlobalConstants.PastryValidationConstants.AllergensMaxLength)]
        public string Allergens { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PastryValidationConstants.ImageFileDirectoryPathMaxLength)]
        public string ImageFileDirectoryPath { get; set; }

        [Required]
        public FoodTastingCategory Category { get; set; }

        [Required]
        [ForeignKey(nameof(NutritionInfo))]
        public string NutritionInfoId { get; set; }

        public virtual NutritionInfo NutritionInfo { get; set; }
    }
}
