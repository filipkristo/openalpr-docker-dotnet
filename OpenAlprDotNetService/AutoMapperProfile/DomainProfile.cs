using System;
using AutoMapper;
using OpenAlprDotNetService.Dto;
using OpenAlprDotNetService.Model;

namespace OpenAlprDotNetService.AutoMapperProfile
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<OpelAlprResult, RecognitionResult>();
        }
    }
}
