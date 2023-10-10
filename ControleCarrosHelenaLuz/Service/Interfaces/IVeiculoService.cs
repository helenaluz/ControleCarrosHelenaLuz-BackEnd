using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;
using System.Reflection;

namespace ControleCarrosHelenaLuz.Service.Interfaces
{
    public interface IVeiculoService
    {
        public Task CriarVeiculo(DTOVeiculo veiculo);
        public Task<List<Veiculo>> VerVeiculos();
        public Task<Veiculo> VerVeiculoPorId(int Id);
        Task<Veiculo> VerVeiculoPorPlaca(string Placa);
        public Task DeletarVeiculo(string placa);
        public Task EditarVeiculo(int Id, DTOVeiculo request);
        public Task CheckOut(string placa);
        public decimal CalculoValorFinal(int Id);
    }
}
