using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;
using System.Reflection;

namespace ControleCarrosHelenaLuz.Service.Interfaces
{
    public interface IVeiculoService
    {
        public Task CriarVeiculo(DTOVeiculo veiculo);
        public Task<List<Veiculo>> VerVeiculos();
        public Task<DTOVeiculoGet> VerVeiculoPorId(int Id);
        Task<Veiculo> VerVeiculoPorPlaca(string Placa);
        public Task DeletarVeiculo(string placa);
        public Task EditarVeiculo(int Id, DTOVeiculoPut request);
        Task<List<Veiculo>> Estacionados();
        public Task CheckOut(string placa);
        public double CalculoValorFinal(int Id);
    }
}
