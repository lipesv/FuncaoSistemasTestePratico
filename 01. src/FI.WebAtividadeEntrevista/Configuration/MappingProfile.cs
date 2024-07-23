using AutoMapper;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DML.Extensions;
using FI.WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento entre DML e ViewModel
            CreateMap<Cliente, ClienteModel>();

            CreateMap<Beneficiario, BeneficiarioModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.AplicaMascaraCpf()))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente));

            // Mapeamento entre ViewModel e DML (se necessário)
            CreateMap<ClienteModel, Cliente>();

            CreateMap<BeneficiarioModel, Beneficiario>();
        }
    }
}