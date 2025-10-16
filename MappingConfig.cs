using AutoMapper;
using TransactionWebAPI.Models;
using TransactionWebAPI.Models.Dto;

namespace TransactionWebAPI
{
    public class MappingConfig:Profile
    {


        public MappingConfig()
        {

            // Category
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
          
            // Transaction
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(dest => dest.Date, opt =>
                     opt.MapFrom(src =>
                        src.UpdatedAt > src.CreatedAt ? src.UpdatedAt : src.CreatedAt)).ReverseMap();

            CreateMap<Transaction, TransactionCreateDTO>().ReverseMap();
            CreateMap<Transaction, TransactionUpdateDTO>().ReverseMap();




        }




    }
}
