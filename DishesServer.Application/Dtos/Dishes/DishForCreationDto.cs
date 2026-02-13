using System.ComponentModel.DataAnnotations;

namespace DishesServer.Application.Dtos.Dishes;

public class DishForCreationDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }
}