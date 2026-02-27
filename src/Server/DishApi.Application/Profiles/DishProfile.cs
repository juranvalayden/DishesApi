using AutoMapper;
using DishApi.Application.Dtos.Dishes;
using DishesApi.Domain.Entities;

namespace DishApi.Application.Profiles;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDto>().ReverseMap();
        CreateMap<Dish, DishForCreationDto>().ReverseMap();
        CreateMap<Dish, DishForUpdateDto>().ReverseMap();
    }
}