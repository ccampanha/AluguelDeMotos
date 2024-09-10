using Microsoft.EntityFrameworkCore;
using Motos.Data;
using Motos.Entities;

namespace Motos.Services
{
    public class EntregadorService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EntregadorService(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // Método para incluir um novo entregador
        public async Task<Entregador> AddEntregadorAsync(Entregador entregador)
        {
            _context.Entregadores.Add(entregador);
            await _context.SaveChangesAsync();
            return entregador;
        }

        // Método para fazer o upload da foto da CNH
        public async Task<bool> UploadFotoAsync(int id, IFormFile foto)
        {
            var entregador = await _context.Entregadores.FindAsync(id);
            if (entregador == null) return false;

            // Salvar o arquivo de foto da CNH
            var fileName = $"{entregador.Id}_CNH_{Path.GetExtension(foto.FileName)}";
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Atualizar o campo de foto da CNH
            entregador.ImagemCnh = fileName;
            _context.Entregadores.Update(entregador);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
