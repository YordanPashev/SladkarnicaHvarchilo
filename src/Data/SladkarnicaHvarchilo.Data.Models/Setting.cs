namespace SladkarnicaHvarchilo.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SladkarnicaHvarchilo.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
