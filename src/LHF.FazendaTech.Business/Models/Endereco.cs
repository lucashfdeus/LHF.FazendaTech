using LHF.FazendaTech.Business.Models.Base;

namespace LHF.FazendaTech.Business.Models
{
    public class Endereco : Entity
    {
        public Guid FazendaId { get; set; }
        public string? Logradouro { get; set; } = string.Empty;
        public string? Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
        public string? Cep { get; set; } = string.Empty;
        public string? Bairro { get; set; } = string.Empty;
        public string? Cidade { get; set; } = string.Empty;
        public string? Estado { get; set; } = string.Empty;

        /* EF Relation */
        public Fazenda Fazenda { get; set; }
    }
}
