using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Subscription", Schema = "Common")]
public class Subscription : BaseEntity
{
    [MaxLength(50)]
    public string Code { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }

    // Factory methods
    public static Subscription Create(int id, string code, string name)
    {
        return new Subscription
        {
            Id = id,
            Code = code,
            Name = name
        };
    }

    public void Update(string code, string name, string description)
    {
        Code = code;
        Name = name;
        Description = description;
    }
}
