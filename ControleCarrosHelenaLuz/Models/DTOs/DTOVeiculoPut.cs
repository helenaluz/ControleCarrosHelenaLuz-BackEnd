namespace ControleCarrosHelenaLuz.Models.DTOs
{
    public class DTOVeiculoPut
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public DateTime DataEntrada { get; set; }
    }
}
