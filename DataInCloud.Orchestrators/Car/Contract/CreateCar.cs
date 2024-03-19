using System.ComponentModel.DataAnnotations;

namespace DataInCloud.Orchestrators.Car.Contract;

public class CreateCar
{
    [MaxLength(256, ErrorMessage = "Should not be greater that 256 chars")]
    public string Name { get; set; }
    [Range(1, 100, ErrorMessage = "Doors must be from 1 too 100")]
    public int DoorsCount { get; set; }
    public bool IsBuyEnable { get; set; }
}
