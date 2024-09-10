using Microsoft.EntityFrameworkCore;
using Motos.Data;
using Motos.Entities;

namespace Motos.Services
{
    public class LocacaoService
    {
        private readonly AppDbContext _context;

        public LocacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Locacao> AddLocacaoAsync(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();
            return locacao;
        }

        public async Task<Locacao> GetLocacaoByIdAsync(int id)
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Entregador)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Locacao> AtualizarDevolucaoAsync(int locacaoId, DateTime dataDevolucao)
        {
            var locacao = await _context.Locacoes.FindAsync(locacaoId);
            if (locacao != null)
            {
                locacao.DataTermino = dataDevolucao;
                // Atualiza o valor da multa se necessário
                await _context.SaveChangesAsync();
            }
            return locacao;
        }
    }
}
