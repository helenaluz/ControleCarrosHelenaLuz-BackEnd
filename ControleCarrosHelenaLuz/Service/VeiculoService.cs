using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;
using ControleCarrosHelenaLuz.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleCarrosHelenaLuz.Service
{
    public class VeiculoService : IVeiculoService
    {
        private readonly Context _con;

        public VeiculoService(Context context)
        {
            _con = context;
        }
        public  decimal CalculoValorFinal(int Id)
        {
            var now = DateTime.Now;
            Preco preco =  _con.Precos.FirstOrDefault(p => p.ValidadeFim >  now);
            decimal valorHora = preco.ValorHora;
            decimal valorAdicional = preco.ValorAdicional;

            Veiculo veiculo = _con.Veiculos.FirstOrDefault(v => v.Id == Id);

            DateTime entrada = veiculo.DataEntrada;
            DateTime? saida = veiculo.DataSaida;

            if (!saida.HasValue) throw new Exception("Erro no codigo, saida não tem valor, olhar metodo CheckOut de VeiculoService");

            var diferenca = saida.Value - entrada;
            int minutos = (int)diferenca.TotalMinutes;
            int horas = (int)diferenca.TotalHours;

            int regrinha = minutos - (horas*60);

            if (horas == 0 && minutos <= 30)  return valorHora / 2;
            
            if (horas > 0)
            {
                decimal valorTotalHoras = valorHora * horas;
                if (regrinha <= 10) return valorTotalHoras;
                else
                {
                    decimal X = valorAdicional * (horas - 1);
                    return valorTotalHoras + valorAdicional;
                }
            }

            return valorHora;
            
        }

        public async Task CheckOut(string placa)
        {
            Veiculo veiculo = await VerVeiculoPorPlaca(placa);
            veiculo.DataSaida = DateTime.Now;
            veiculo.ValorFinal = CalculoValorFinal(veiculo.Id);
            await _con.SaveChangesAsync();
        }

        public async Task CriarVeiculo(DTOVeiculo veiculo)
        {
            var result = new Veiculo
            {
                Placa = veiculo.Placa,
                DataEntrada = veiculo.DataEntrada,
            };

            await _con.Veiculos.AddAsync(result);
            await _con.SaveChangesAsync();

        }

        public async Task DeletarVeiculo(string placa)
        {
            var result = await VerVeiculoPorPlaca(placa);
            if (result == null) return;
            _con.Veiculos.Remove(result);
            await _con.SaveChangesAsync();
        }

        public async Task EditarVeiculo(int Id, DTOVeiculo request)
        {
            var result = _con.Veiculos.FirstOrDefault(v => v.Id == Id);
            if (result == null) return;
            result.Placa = request.Placa;
            result.DataEntrada = request.DataEntrada;
            await _con.SaveChangesAsync();
        }

        public async Task<Veiculo> VerVeiculoPorId(int Id)
        {
            var result = await _con.Veiculos.FirstOrDefaultAsync(x => x.Id == Id);
            return result;
        }

        public async Task<Veiculo> VerVeiculoPorPlaca(string Placa)
        {
            var result = await _con.Veiculos.FirstOrDefaultAsync(x => x.Placa == Placa);
            return result;
        }

        public async Task<List<Veiculo>> VerVeiculos()
        {
            return await _con.Veiculos.ToListAsync();
        }
    }
}
