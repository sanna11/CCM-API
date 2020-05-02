using AutoMapper;
using CCM.Core.Constants;
using CCM.Data.Models;
using CCM.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.Mapping
{
    public class TransformationDataMappingProfile : Profile
    {
        public TransformationDataMappingProfile()
        {
            CreateMap<Movie, MovieViewModel>()
               .ForMember(d => d.MovieLanguage, opt => opt.MapFrom(src =>
                  LanguageConstants.GetString(src.Language)))
               .ReverseMap()
               ;

            CreateMap<Theatre, TheatreViewModel>()
               .ReverseMap()
               ;

            CreateMap<TheatreHall, TheatreHallViewModel>()
                .ForMember(d => d.TheatreName, opt => opt.MapFrom(src =>
                    src.TheatreInfo != null ? src.TheatreInfo.Name : ""))
                .ReverseMap()
                .ForMember(d => d.TheatreInfo, opt => opt.Ignore())
                ;

            CreateMap<TheatreSession, TheatreSessionViewModel>()
              .ReverseMap()
              ;

            CreateMap<Ticket, TicketViewModel>()
             .ReverseMap()
             ;
        }
    }
}
