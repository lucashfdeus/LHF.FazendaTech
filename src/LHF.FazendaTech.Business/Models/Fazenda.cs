using LHF.FazendaTech.Business.Models.Base;

namespace LHF.FazendaTech.Business.Models
{
    public class Fazenda : Entity
    {
        public string? Nome { get; set; }
        public string? Documento { get; set; }
        public TipoProprietario TipoProprietario { get; set; }

        public string? NomeFazenda { get; set; }
        public Endereco? Endereco { get; set; }
        public bool Ativo { get; set; }       
    }
}
