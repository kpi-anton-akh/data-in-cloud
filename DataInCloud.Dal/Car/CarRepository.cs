using AutoMapper;
using DataInCloud.Model.Car;
using Microsoft.EntityFrameworkCore;

namespace DataInCloud.Dal.Car;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CarRepository(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Model.Car.Car>> GetAllAsync()
    {
        var entities = await _context.Cars.AsNoTracking().ToListAsync();

        var cars = _mapper.Map<List<Model.Car.Car>>(entities);

        return cars;
    }

    public async Task<Model.Car.Car> CreateAsync(Model.Car.Car entityToCreate)
    {
        var entity = _mapper.Map<CarDao>(entityToCreate);

        var createdEntity = await _context.Cars.AddAsync(entity);

        await _context.SaveChangesAsync();

        return _mapper.Map<Model.Car.Car>(createdEntity.Entity);
    }

    public async Task<Model.Car.Car> GetByIdAsync(int id)
    {
        var entity = await _context.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<Model.Car.Car>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        _context.Cars.Remove(_context.Cars.AsNoTracking().First(c => c.Id == id));
        await _context.SaveChangesAsync();
    }

    public async Task<Model.Car.Car> UpdateAsync(Model.Car.Car inputCar)
    {
        var carEntity = await _context.Cars.FirstAsync(c => c.Id == inputCar.Id);

        carEntity.Name = inputCar.Name;

        await _context.SaveChangesAsync();

        return _mapper.Map<Model.Car.Car>(carEntity);
    }
}
