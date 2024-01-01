namespace SladkarnicaHvarchilo.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SladkarnicaHvarchilo.Data.Common.Models;

    public class NutritionInfo : BaseDeletableModel<int>
    {
        public NutritionInfo() => this.Id = Guid.NewGuid().ToString();

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public double Fats { get; set; }

        [Required]
        public double Carbs { get; set; }

        [Required]
        public double Sugar { get; set; }

        [Required]
        public double Protein { get; set; }

        public double Salt { get; set; }
    }
}
