using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;
using ControleCarrosHelenaLuz.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Numerics;

namespace ControleCarrosHelenaLuz.Service
{
    public class VeiculoService : IVeiculoService
    {
        private readonly Context _con;

        public VeiculoService(Context context)
        {
            _con = context;
        }

        public  double CalculoValorFinal(int Id)
        {
            var now = DateTime.Now;
            Preco preco = _con.Precos.FirstOrDefault(p => p.ValidadeFim > now);
            double valorHora = preco.ValorHora;
            double valorAdicional = preco.ValorAdicional;

            Veiculo veiculo = _con.Veiculos.FirstOrDefault(v => v.Id == Id);

            DateTime entrada = veiculo.DataEntrada;
            DateTime? saida = veiculo.DataSaida;

            if (!saida.HasValue) throw new Exception("Erro no codigo, saida não tem valor, olhar metodo CheckOut de VeiculoService");

            var diferenca = saida.Value - entrada;
            int minutos = (int)diferenca.TotalMinutes;

            int horas = (int) Math.Ceiling(minutos / 60.0);

            int regrinha = minutos - (horas * 60);

            if(regrinha < 0) regrinha *= (-1);

            if(regrinha <= 10) horas--;

            if(minutos <= 30) return valorHora / 2;

            return valorHora + (horas*valorAdicional);
        }

        public async Task CheckOut(string placa)
        {
            Veiculo veiculo = await VerVeiculoPorPlaca(placa);
            if (veiculo.DataSaida != null) throw new Exception("Veiculo já saiu!");
            veiculo.DataSaida = DateTime.Now;
            veiculo.ValorFinal = CalculoValorFinal(veiculo.Id);
            await _con.SaveChangesAsync();
        }

        public async Task CriarVeiculo(DTOVeiculo veiculo)
        {
            var yey =  _con.Veiculos.Where(v => v.Placa == veiculo.Placa).ToList();

            foreach(var v in yey)
            {
                if (!v.DataSaida.HasValue && !v.ValorFinal.HasValue) throw new Exception("Esse veiculo ainda esta estacionado");
            }

            var result = new Veiculo
            {
                Placa = veiculo.Placa,
                DataEntrada = veiculo.DataEntrada,
            };

            await _con.Veiculos.AddAsync(result);
            await _con.SaveChangesAsync();

        }

        public async Task<List<Veiculo>> Estacionados()
        {
            return _con.Veiculos.Where(v => v.DataSaida  == null).ToList(); 
        }

        public async Task DeletarVeiculo(string placa)
        {
            var result = await VerVeiculoPorPlaca(placa);
            if (result == null) return;
            _con.Veiculos.Remove(result);
            await _con.SaveChangesAsync();
        }

        public async Task EditarVeiculo(int Id, DTOVeiculoPut request)
        {
            var result = _con.Veiculos.FirstOrDefault(v => v.Id == Id);
            if (result == null || result.Id != request.Id) throw new Exception("IDs diferentes!");
            result.Placa = request.Placa;
            result.DataEntrada = request.DataEntrada;
            await _con.SaveChangesAsync();
        }

        public async Task<DTOVeiculoGet> VerVeiculoPorId(int Id)
        {
            var resulto = await _con.Veiculos.FirstOrDefaultAsync(x => x.Id == Id);

            if (resulto == null) return null;

            if (resulto.DataSaida != null) return null;

            TimeSpan? tempo = DateTime.Now - resulto.DataEntrada;

            DTOVeiculoGet result = new DTOVeiculoGet
            {
                Placa = resulto.Placa,
                Duracao = tempo,
                DataEntrada = resulto.DataEntrada
            };

            return result;
        }

        public async Task<Veiculo> VerVeiculoPorPlaca(string Placa)
        {
            var resulto = await _con.Veiculos.FirstOrDefaultAsync(x => x.Placa == Placa);

            if (resulto == null)  return null; 
            
            
            return resulto;
        }

        public async Task<List<Veiculo>> VerVeiculos()
        {
            return await _con.Veiculos.ToListAsync();
        }
    }
}
