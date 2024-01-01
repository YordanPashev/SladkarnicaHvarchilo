namespace SladkarnicaHvarchilo.Web.ViewModels.NutritionInfo
{
    using System.ComponentModel.DataAnnotations;

    using SladkarnicaHvarchilo.Data.Models;
    using SladkarnicaHvarchilo.Services.Mapping;

    public class NutritionInfoViewModel : IMapTo<NutritionInfo>
    {
        [Key]
        public string Id { get; set; }

        public double Fats { get; set; }

        public double Carbs { get; set; }

        public double Sugar { get; set; }

        public double Protein { get; set; }

        public double Salt { get; set; }
    }
}
