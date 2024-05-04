using AutoMapper;
using LHF.FazendaTech.Api.ViewModels;
using LHF.FazendaTech.Business.Models;

namespace LHF.FazendaTech.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Fazenda, FazendaViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        }
    }
}
