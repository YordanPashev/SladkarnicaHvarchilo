namespace SladkarnicaHvarchilo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SladkarnicaHvarchilo.Data.Common.Models;

    public class PriceInfo : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [ForeignKey(nameof(Dessert))]
        [Required]
        public string DessertId { get; set; }

        public Dessert Dessert { get; set; }

        [Required]
        public int Pieces { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
