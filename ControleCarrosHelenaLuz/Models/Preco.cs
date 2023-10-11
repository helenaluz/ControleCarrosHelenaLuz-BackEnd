using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleCarrosHelenaLuz.Models
{
    public class Preco
    {
        public int Id { get; set; }

        public DateTime ValidadeInicio { get; set; }

        public DateTime ValidadeFim { get; set; }
        public double ValorHora { get; set; }
        public double ValorAdicional { get; set; }
    }
}
