using System.ComponentModel.DataAnnotations;
using DishApi.Application.Dtos.Ingredients;

namespace DishApi.Application.Dtos.Dishes;

public record DishDto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    public IEnumerable<IngredientDto> Ingredients { get; set; } = [];
}