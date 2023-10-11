using ControleCarrosHelenaLuz.Models;
using ControleCarrosHelenaLuz.Models.DTOs;
using ControleCarrosHelenaLuz.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleCarrosHelenaLuz.Service
{
    public class PrecoService : IPrecoService
    {
        private readonly Context _con;

        public PrecoService(Context context)
        {
            _con = context;
        }

        public async Task CriarPreco(DTOPreco Preco)
        {
            var resultado = new Preco 
            { 
                ValidadeInicio = Preco.ValidadeInicio,
                ValidadeFim = Preco.ValidadeFim,
                ValorHora = Preco.ValorHora,
                ValorAdicional = Preco.ValorAdicional
            };


            await _con.Precos.AddAsync(resultado);
            await _con.SaveChangesAsync();
        }

        public async Task DeletarPreco(int Id)
        {
            var deletar = _con.Precos.FirstOrDefault(x => x.Id == Id);
            if (deletar == null) return;
             _con.Precos.Remove(deletar);
            await _con.SaveChangesAsync();
        }

        public async Task EditarPreco(int Id, Preco request)
        {
            var result = _con.Precos.FirstOrDefault(x => x.Id == Id);
            if (result == null || result.Id != request.Id ) throw new ArgumentException("IDs diferentes!");
            result.ValidadeInicio = request.ValidadeInicio;
            result.ValidadeFim = request.ValidadeFim;
            result.ValorHora = request.ValorHora;
            result.ValorAdicional = request.ValorAdicional;

            await _con.SaveChangesAsync();
        }

        public async Task<List<Preco>> VerPreco()
        {
            return await _con.Precos.ToListAsync();
        }

        public async Task<Preco> VerPrecoPorId(int Id)
        {
            var result = await _con.Precos.FirstOrDefaultAsync(x => x.Id == Id);
            return result;
        }
    }
}
