namespace SladkarnicaHvarchilo.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SladkarnicaHvarchilo.Data.Common;
    using SladkarnicaHvarchilo.Data.Common.Models;
    using SladkarnicaHvarchilo.Data.Models.Enums;

    public class Cake : BaseDeletableModel<int>
    {
        public Cake() => this.Id = Guid.NewGuid().ToString();

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CakeValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CakeValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CakeValidationConstants.IngredientsMaxLength)]
        public string Ingredients { get; set; }

        [Required]
        public int Pieces { get; set; }

        [MaxLength(ValidationConstants.CakeValidationConstants.AllergensMaxLength)]
        public string Allergens { get; set; }

        [Required]
        [Range(4, 2)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CakeValidationConstants.ImageFileDirectoryPathMaxLength)]
        public string ImageFileDirectoryPath { get; set; }

        [Required]
        public FoodTastingCategory Category { get; set; }
    }
}
