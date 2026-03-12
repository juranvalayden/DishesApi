using System.ComponentModel.DataAnnotations;

namespace DishApi.Application.Dtos.Ingredients;

public class IngredientDto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200), MinLength(3)]
    public required string Name { get; set; }

    public Guid DishId { get; set; }
}