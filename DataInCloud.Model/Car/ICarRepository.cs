namespace DataInCloud.Model.Car;

public interface ICarRepository
{
    Task<List<Car>> GetAllAsync();
    Task<Car> CreateAsync(Car entityToCreate);
    Task<Car> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task<Car> UpdateAsync(Car inputCar);
}
