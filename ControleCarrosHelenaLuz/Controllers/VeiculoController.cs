using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;
using ControleCarrosHelenaLuz.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleCarrosHelenaLuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _service;
        public VeiculoController(IVeiculoService service)
        {
            _service = service;
        }

        [HttpPut("checkout/{placa}")]
        public async Task<IActionResult> CheckOut(string placa)
        {
            try
            {
                await _service.CheckOut(placa);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(DTOVeiculo veiculo)
        {
            try
            {
                await _service.CriarVeiculo(veiculo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{placa}")]
        public async Task<IActionResult> Delete(string placa)
        {
            try
            {
                await _service.DeletarVeiculo(placa);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutVeiculo(int Id, DTOVeiculoPut veiculo)
        {
            try
            {
                await _service.EditarVeiculo(Id, veiculo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DTOVeiculoGet>> GetVeiculoById(int Id)
        {
            try
            {
                var result = await _service.VerVeiculoPorId(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("placa/{placa}")]
        public async Task<ActionResult<Veiculo>> GetVeiculoByPlaca(string placa)
        {
            try
            {
                var result = await _service.VerVeiculoPorPlaca(placa);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Veiculo>>> GetVeiculo()
        {
            try
            {
                var result = await _service.VerVeiculos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("estacionados")]
        public async Task<ActionResult<List<Veiculo>>> Estacionados()
        {
            try
            {
                var result = await _service.Estacionados();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
