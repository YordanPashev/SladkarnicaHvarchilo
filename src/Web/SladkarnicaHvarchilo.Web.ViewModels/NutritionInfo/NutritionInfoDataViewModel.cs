namespace SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo
{
    using System.ComponentModel.DataAnnotations;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;

    using static SladkarnicaHvarchilo.Common.GlobalConstants;

    public class NutritionInfoDataViewModel : IMapTo<NutritionInfo>
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [Range(NutritionInfoConstants.MacrosMinValue, NutritionInfoConstants.MacrosMaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Fats { get; set; }

        [Required]
        [Range(NutritionInfoConstants.MacrosMinValue, NutritionInfoConstants.MacrosMaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Carbs { get; set; }

        [Required]
        [Range(NutritionInfoConstants.MacrosMinValue, NutritionInfoConstants.MacrosMaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Sugar { get; set; }

        [Required]
        [Range(NutritionInfoConstants.MacrosMinValue, NutritionInfoConstants.MacrosMaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Protein { get; set; }

        [Range(NutritionInfoConstants.MacrosMinValue, NutritionInfoConstants.MacrosMaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double Salt { get; set; }
    }
}
