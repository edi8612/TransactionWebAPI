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
            //CreateMap<CategoryDTO, CategoryCreateDTO>().ReverseMap();
            //CreateMap<CategoryDTO, CategoryUpdateDTO>().ReverseMap();

            // Transaction
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<Transaction, TransactionCreateDTO>().ReverseMap();
            CreateMap<Transaction, TransactionUpdateDTO>().ReverseMap();




        }




    }
}
