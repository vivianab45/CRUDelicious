#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required(ErrorMessage="Must inlude Dish Name")]
    public string Name { get; set; } 
    [Required(ErrorMessage="Must inlude Dish Name")]
    public string Chef { get; set; }
    [Required]
    [Range(1,5)]
    public int Tastiness { get; set; }
    [Required (ErrorMessage="Calories must be at least 0.")]
    [Range(0,int.MaxValue)]
    public int Calories { get; set; }
    [Required]
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}