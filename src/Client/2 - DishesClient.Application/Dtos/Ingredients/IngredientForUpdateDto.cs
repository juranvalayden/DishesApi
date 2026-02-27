using System.ComponentModel.DataAnnotations;

namespace DishesClient.Application.Dtos.Ingredients;

public class IngredientForUpdateDto
{
    [Required]
    public Guid DishId { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }
}