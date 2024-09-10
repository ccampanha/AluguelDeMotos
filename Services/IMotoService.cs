using Motos.Entities;

namespace Motos.Services
{
    public interface IMotoService
    {
        Task<Moto> AddMotoAsync(Moto moto);
        Task<IEnumerable<Moto>> GetAllMotosAsync();
        Task<Moto> GetMotoByIdAsync(Guid id);
        Task<bool> UpdateMotoPlacaAsync(Guid id, string novaPlaca);
        Task<bool> DeleteMotoByIdAsync(Guid id);
    }
}
