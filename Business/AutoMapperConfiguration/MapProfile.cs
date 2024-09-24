using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapperConfiguration
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book, BookUpdateDTO>().ReverseMap();
        }
    }
}
