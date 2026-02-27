using AutoMapper;
using DishApi.Application.Dtos.Ingredients;
using DishesApi.Domain.Entities;

namespace DishApi.Application.Profiles;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<Ingredient, IngredientDto>()
            .ForMember(
                d => d.Id,
                memberOption => memberOption.MapFrom(s => s.Dishes.First().Id))
            .ReverseMap();

        CreateMap<Ingredient, IngredientForCreationDto>()
            .ForMember(
                d => d.DishId,
                memberOption => memberOption.MapFrom(s => s.Dishes.First().Id))
            .ReverseMap();

        CreateMap<Ingredient, IngredientForUpdateDto>().ForMember(
            d => d.DishId,
            memberOption => memberOption.MapFrom(s => s.Dishes.First().Id)).ReverseMap();
    }
}