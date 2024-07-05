using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Tax", Schema = "Common")]
public class Tax : BaseEntity
{
    [MaxLength(26)]
    public string Code { get; set; }
    public int? CategoryId { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    [MaxLength(53)]
    public double? Amount { get; set; }
    public virtual Category Category { get; set; }
    // Factory methods
    public static Tax Create(int id, string code)
    {
        return new Tax
        {
            Id = id,
            Code = code
        };
    }

    public void Update(string code, int? category, string description, double? amount)
    {
        Code = code;
        CategoryId = category;
        Description = description;
        Amount = amount;
    }
}
