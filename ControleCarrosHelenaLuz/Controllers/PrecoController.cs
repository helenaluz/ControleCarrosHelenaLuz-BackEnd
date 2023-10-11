using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;
using ControleCarrosHelenaLuz.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleCarrosHelenaLuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoController : ControllerBase
    {
        private readonly IPrecoService _service;
        public PrecoController(IPrecoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostPreco(DTOPreco preco)
        {
            try
            {
                await _service.CriarPreco(preco);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePreco(int Id)
        {
            try
            {
                await _service.DeletarPreco(Id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Id")]
        public async Task<IActionResult> PutPreco(int Id, Preco preco)
        {
            try
            {
                await _service.EditarPreco(Id, preco);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Preco>>> GetPrecos()
        {
            try
            {
                var result = await _service.VerPreco();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Id")]
        public async Task<ActionResult<Preco>> GetPrecoById(int Id)
        {
            try
            {
                var result = await _service.VerPrecoPorId(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }
    }
}
