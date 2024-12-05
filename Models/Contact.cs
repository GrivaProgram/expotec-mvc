using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expotec_mvc.Models
{
    [Table("contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(200)]
        [Column("name")]
        public string? Name { get; set; }

        [MaxLength(15)]
        [Column("dni")]
        public string? Dni { get; set; }

        [MaxLength(20)]
        [Column("phone")]
        public string? Phone { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        [Column("email")]
        public string? Email { get; set; }

        [MaxLength(200)]
        [Column("category")]
        public string? Category { get; set; }

        [MaxLength(100)]
        [Column("position")]
        public string? Position { get; set; }

        [MaxLength(200)]
        [Column("company_name")]
        public string? CompanyName { get; set; }

        [MaxLength(200)]
        [Column("area")]
        public string? Area { get; set; }

        [Column("cocktail_candidate")]
        public bool? CocktailCandidate { get; set; }

        [MaxLength(200)]
        [Column("source")]
        public string? Source { get; set; }

        [MaxLength(20)]
        [Column("ruc")]
        public string? Ruc { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [MaxLength(100)]
        [Column("district")]
        public string? District { get; set; }

        [Column("attended_expotec")]
        public bool? AttendedExpotec { get; set; }

        [Column("attended_cocktail")]
        public bool? AttendedCocktail { get; set; }

        [MaxLength(200)]
        [Column("coordinator")]
        public string? Coordinator { get; set; }

        [MaxLength(200)]
        [Column("seller")]
        public string? Seller { get; set; }

        [Column("comments")]
        public string? Comments { get; set; }
    }
}
