namespace DataInCloud.Model.Car;

public interface ICarOrchestrator
{
    Task<List<Car>> GetAllAsync();
    Task<Car> CreateAsync(Car entityToCreate);
    Task<Car> GetByIdAsync(int id);
    Task<Car> DeleteAsync(int id);
    Task<Car> UpdateAsync(Car car);
}
