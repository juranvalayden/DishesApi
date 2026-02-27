using System.ComponentModel.DataAnnotations;

namespace DishesClient.Application.Dtos.Dishes;

public class DishDto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }
}