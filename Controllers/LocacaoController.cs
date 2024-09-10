using Microsoft.AspNetCore.Mvc;
using Motos.Entities;
using Motos.Services;

namespace Motos.Controllers
{
    [ApiController]
    [Route("Locação")]
    [Produces("application/json")]
    public class LocacaoController : ControllerBase
    {
        private readonly LocacaoService _locacaoService;

        public LocacaoController(LocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        /// <summary>
        /// Alugar uma moto
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddLocacao([FromBody] Locacao locacao)
        {
            var newLocacao = await _locacaoService.AddLocacaoAsync(locacao);
            return Ok(newLocacao);
        }

        /// <summary>
        /// Consultar locação por Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocacaoById(int id)
        {
            var locacao = await _locacaoService.GetLocacaoByIdAsync(id);
            return locacao != null ? Ok(locacao) : NotFound();
        }

        /// <summary>
        /// Informar data de devolução e calcular o valor
        /// </summary>
        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> AtualizarDevolucao(int id, [FromBody] DateTime dataDevolucao)
        {
            var locacaoAtualizada = await _locacaoService.AtualizarDevolucaoAsync(id, dataDevolucao);
            if (locacaoAtualizada == null)
            {
                return NotFound("Locação não encontrada.");
            }
            return Ok(locacaoAtualizada);
        }
    }
}
