using DataInCloud.Model.Car;
using DataInCloud.Orchestrators.Exception;

namespace DataInCloud.Orchestrators.Car;

public class CarOrchestrator : ICarOrchestrator
{
    private readonly ICarRepository _carRepository;

    public CarOrchestrator(
        ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<List<Model.Car.Car>> GetAllAsync()
    {
        return await _carRepository.GetAllAsync();
    }

    public async Task<Model.Car.Car> CreateAsync(Model.Car.Car entityToCreate)
    {
        return await _carRepository.CreateAsync(entityToCreate);
    }

    public async Task<Model.Car.Car> GetByIdAsync(int id)
    {
        var existingCar = await _carRepository.GetByIdAsync(id);

        if (existingCar == null)
        {
            throw new ResourceNotFoundException($"No car found with id = {id}");
        }

        return existingCar;
    }

    public async Task<Model.Car.Car> DeleteAsync(int id)
    {
        var existingCar = await GetByIdAsync(id);

        await _carRepository.DeleteAsync(id);

        return existingCar;
    }

    public async Task<Model.Car.Car> UpdateAsync(Model.Car.Car car)
    {
        var existingCar = await GetByIdAsync(car.Id);

        existingCar.Name = car.Name;

        var modifiedCar = await _carRepository.UpdateAsync(existingCar);

        return modifiedCar;
    }
}
