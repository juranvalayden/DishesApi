using System.ComponentModel.DataAnnotations;

namespace DishesClient.Application.Dtos.Dishes;

public class DishForUpdateDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }
}