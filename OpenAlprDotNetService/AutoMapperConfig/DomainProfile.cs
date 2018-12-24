using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OpenAlprDotNetService.Dto;
using OpenAlprDotNetService.Model;

namespace OpenAlprDotNetService.AutoMapperProfile
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<OpelAlprResult, IEnumerable<RecognitionResult>>()
                .ConstructUsing(m => m.Results.Select(r => new RecognitionResult
                {
                    PlateNumber = r.Plate,
                    Confidence = r.Confidence,
                    TotalProcessingTimeMs = m.ProcessingTimeMs,
                    PlatePoints = r.Coordinates.Select(c => new PlatePoint
                    {
                        X = c.X,
                        Y = c.Y
                    })
                }));
        }
    }
}
