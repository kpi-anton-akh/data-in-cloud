using AutoMapper;
using DataInCloud.Model.Car;
using DataInCloud.Orchestrators.Car.Contract;
using Microsoft.AspNetCore.Mvc;

namespace DataOnCloud.Api.Car
{
    [ApiController]
    [Route("api/v1/cars")]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarOrchestrator _carOrchestrator;

        public CarsController(
            IMapper mapper,
            ICarOrchestrator carOrchestrator)
        {
            _mapper = mapper;
            _carOrchestrator = carOrchestrator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var entities = await _carOrchestrator.GetAllAsync();

            var cars = _mapper.Map<List<DataInCloud.Orchestrators.Car.Contract.Car>>(entities);

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var carModel = await _carOrchestrator.GetByIdAsync(id);

            return Ok(_mapper.Map<DataInCloud.Orchestrators.Car.Contract.Car>(carModel ));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateCar model)
        {
            var entityToCreate = _mapper.Map<DataInCloud.Model.Car.Car>(model);

            var createdEntity = await _carOrchestrator.CreateAsync(entityToCreate);

            return Ok(_mapper.Map<DataInCloud.Orchestrators.Car.Contract.Car>(createdEntity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedCar = await _carOrchestrator.DeleteAsync(id);

            return Ok(_mapper.Map<DataInCloud.Orchestrators.Car.Contract.Car>(deletedCar));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, EditCar editCar)
        {
            var car = _mapper.Map<DataInCloud.Model.Car.Car>(editCar);
            car.Id = id;
            var editedCar = await _carOrchestrator.UpdateAsync(car);

            return Ok(_mapper.Map<DataInCloud.Orchestrators.Car.Contract.Car>(editedCar));
        }
    }
}
