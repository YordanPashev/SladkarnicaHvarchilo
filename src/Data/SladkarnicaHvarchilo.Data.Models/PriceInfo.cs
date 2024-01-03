namespace SladkarnicaHvarchilo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SladkarnicaHvarchilo.Data.Common.Models;
    using SladkarnicaHvarchilo.Data.Models.Contracts;

    public class PriceInfo : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string PastryId { get; set; }

        [Required]
        public int Pieces { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
