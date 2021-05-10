using AutoMapper;
using Given.DataContext.IMSEntities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Given.Repositories.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        { 
            CreateMap<Inventory, InventoryModel>().ReverseMap();
            CreateMap<InventoryModel, Inventory>().ReverseMap();             
        }
    }
}
