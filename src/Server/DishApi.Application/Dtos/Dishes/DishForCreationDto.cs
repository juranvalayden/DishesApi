using System.ComponentModel.DataAnnotations;

namespace DishApi.Application.Dtos.Dishes;

public class DishForCreationDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; init; }
}