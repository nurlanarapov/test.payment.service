using AutoMapper;
using Payment.Service.Application.Dto.Payment;
using domain = Payment.Service.Domain.Entities;

namespace Payment.Service.Application.Mapper
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<PaymentDto, domain.Payment>().ReverseMap();
        }
    }
}
