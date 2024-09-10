using Microsoft.EntityFrameworkCore;
using Motos.Data;
using Motos.Entities;

namespace Motos.Services
{
    public class MotoService : IMotoService
    {
        private readonly AppDbContext _context;

        public MotoService(AppDbContext context)
        {
            _context = context;
        }

        // 1. Incluir uma nova moto
        public async Task<Moto> AddMotoAsync(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        // 2. Consultar todas as motos
        public async Task<IEnumerable<Moto>> GetAllMotosAsync()
        {
            return await _context.Motos.ToListAsync();
        }

        // 3. Consultar moto por ID
        public async Task<Moto> GetMotoByIdAsync(Guid id)
        {
            return await _context.Motos.FindAsync(id);
        }

        // 4. Modificar a placa da moto por ID
        public async Task<bool> UpdateMotoPlacaAsync(Guid id, string novaPlaca)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return false;

            moto.Placa = novaPlaca;
            await _context.SaveChangesAsync();
            return true;
        }

        // 5. Deletar moto por ID
        public async Task<bool> DeleteMotoByIdAsync(Guid id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return false;

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
