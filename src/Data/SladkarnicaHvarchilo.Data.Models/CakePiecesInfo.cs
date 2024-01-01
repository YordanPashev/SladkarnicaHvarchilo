namespace SladkarnicaHvarchilo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SladkarnicaHvarchilo.Data.Common.Models;
    using SladkarnicaHvarchilo.Data.Models.Contracts;

    public class CakePiecesInfo : BaseDeletableModel<int>
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [ForeignKey(nameof(Cake))]
        [Required]
        public string CakeId { get; set; }

        public Cake Cake { get; set; }

        [Required]
        public int Piece { get; set; }
    }
}
