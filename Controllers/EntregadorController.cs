using Microsoft.AspNetCore.Mvc;
using Motos.Entities;
using Motos.Services;

namespace Motos.Controllers
{
    [ApiController]
    [Route("entregadores")]
    [Produces("application/json")]
    public class EntregadorController : ControllerBase
    {

        private readonly EntregadorService _entregadorService;

        public EntregadorController(EntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }

        /// <summary>
        /// Cadastrar entregador
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddEntregador([FromBody] Entregador entregador)
        {
            var newEntregador = await _entregadorService.AddEntregadorAsync(entregador);
            return Ok(newEntregador);
        }

        /// <summary>
        /// Enviar foto da CNH
        /// </summary>
        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> UploadCnh(int entregadorId, IFormFile file)
        {
            if (file == null || (file.ContentType != "image/png" && file.ContentType != "image/bmp"))
            {
                return BadRequest("Formato de arquivo inválido. Apenas PNG e BMP são permitidos.");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CNHImages");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, $"{entregadorId}_{file.FileName}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _entregadorService.UploadFotoAsync(entregadorId, file);
            return Ok(new { Message = "CNH enviada com sucesso", FilePath = filePath });
        }

    }

}
