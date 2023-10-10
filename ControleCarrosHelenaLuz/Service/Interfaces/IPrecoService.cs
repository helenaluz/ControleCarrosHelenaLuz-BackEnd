using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;

namespace ControleCarrosHelenaLuz.Service.Interfaces
{
    public interface IPrecoService
    {
        public Task CriarPreco(DTOPreco Preco);
        public Task<List<Preco>> VerPreco();
        public Task<Preco> VerPrecoPorId(int Id);
        public Task DeletarPreco(int Id);
        public Task EditarPreco(int Id, DTOPreco request);
    }
}

