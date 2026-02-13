using System.ComponentModel.DataAnnotations;

namespace DishesServer.Application.Dtos.Ingredients;

public class IngredientForCreationDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Required] 
    public Guid DishId { get; set; }
}