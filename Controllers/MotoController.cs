using Microsoft.AspNetCore.Mvc;
using Motos.Entities;
using Motos.Services;

namespace Motos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        // 1. Incluir moto (POST) - "Cadastrar uma nova moto"
        /// <summary>
        /// Cadastrar uma nova moto
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddMoto([FromBody] Moto moto)
        {
            var novaMoto = await _motoService.AddMotoAsync(moto);
            return CreatedAtAction(nameof(GetMotoById), new { id = novaMoto.Id }, novaMoto);
        }

        // 2. Consultar todas as motos (GET) - "Consultar motos existentes"
        /// <summary>
        /// Consultar motos existentes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllMotos()
        {
            var motos = await _motoService.GetAllMotosAsync();
            return Ok(motos);
        }

        // 3. Consultar moto por ID (GET {ID}) - "Consultar motos por Id"
        /// <summary>
        /// Consultar motos por Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotoById(Guid id)
        {
            var moto = await _motoService.GetMotoByIdAsync(id);
            if (moto == null)
            {
                return NotFound("Moto não encontrada.");
            }
            return Ok(moto);
        }

        // 4. Modificar placa por ID (PUT {ID}) - "Modificar a placa de uma moto"
        /// <summary>
        /// Modificar a placa de uma moto
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMotoPlaca(Guid id, [FromBody] string novaPlaca)
        {
            var sucesso = await _motoService.UpdateMotoPlacaAsync(id, novaPlaca);
            if (!sucesso)
            {
                return NotFound("Moto não encontrada.");
            }
            return NoContent();
        }

        // 5. Deletar moto por ID (DELETE {ID}) - "Remover uma moto"
        /// <summary>
        /// Remover uma moto
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotoById(Guid id)
        {
            var sucesso = await _motoService.DeleteMotoByIdAsync(id);
            if (!sucesso)
            {
                return NotFound("Moto não encontrada.");
            }
            return NoContent();
        }
    }
}
