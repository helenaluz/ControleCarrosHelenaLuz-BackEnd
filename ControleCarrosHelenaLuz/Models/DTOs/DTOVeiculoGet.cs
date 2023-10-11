namespace ControleCarrosHelenaLuz.Models.DTOs
{
    public class DTOVeiculoGet
    {
        public string Placa { get; set; } = string.Empty;
        public DateTime DataEntrada { get; set; }
        public TimeSpan? Duracao { get; set; }
    }
}
