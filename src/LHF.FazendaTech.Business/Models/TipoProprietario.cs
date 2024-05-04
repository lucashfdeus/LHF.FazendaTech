using System.Runtime.Serialization;

namespace LHF.FazendaTech.Business.Models
{
    public enum TipoProprietario
    {
        //PessoaFisica = 1,
        //PessoaJuridica

        [EnumMember(Value = "Pessoa Física")]
        PessoaFisica = 1,
        [EnumMember(Value = "Pessoa Jurídica")]
        PessoaJuridica = 2
    }
}
