using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Lead
{
    [Key]
    public int ID { get; set; }
    [MaxLength(100)]
    public string ContactFirstName { get; set; }

    [MaxLength(200)]
    public string ContactFullName { get; set; }

    [MaxLength(20)]
    public string ContactPhoneNumber { get; set; }

    [MaxLength(250)]
    public string ContactEmail { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.Now;

    public string Suburb { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public StatusEnum Status { get; set; }
}