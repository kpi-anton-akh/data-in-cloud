using System.ComponentModel.DataAnnotations;

namespace DataInCloud.Orchestrators.Car.Contract;

public class EditCar
{
    [MaxLength(256, ErrorMessage = "Should not be greater that 256 chars")]
    public string Name { get; set; }
}
