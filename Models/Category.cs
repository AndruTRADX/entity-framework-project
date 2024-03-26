using System.ComponentModel.DataAnnotations;

namespace ProjectEF.Models;

public class Category
{
  [Key] // We Specify this is the ID of the table
  public Guid CategoryID { get; set; }

  [Required] // We specify this is required
  [MaxLength(150)] // Do not exceed the 150 characters 
  public string Name { get; set; }

  [Required] // We specify this is required
  [MaxLength(1000)] // Do not exceed the 150 characters 
  public string Description { get; set; }

  public virtual ICollection<Task> Tasks { get; set; }
}