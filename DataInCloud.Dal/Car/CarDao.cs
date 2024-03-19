using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataInCloud.Dal.Car;

[Table("Cars")]
public class CarDao
{
    [Column("id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public int DoorsCount { get; set; }
    public bool IsBuyEnable { get; set; }
}
