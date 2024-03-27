using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectEF.Models;

public class Task
{
  // [Key]
  public Guid TaskID { get; set; }

  // [ForeignKey("CategoryID")] // This is an ID from another Model
  public Guid CategoryID { get; set; }

  // [Required]
  // [MaxLength(200)]
  public string Title { get; set; }

  // [MaxLength(1000)]
  public string Description { get; set; }
  public Priority TaskPriority { get; set; }
  public DateTime CreationDate { get; set; }
  public virtual Category Category { get; set; }

  // [NotMapped] // Do not create this on the database, instead, create it in a dynamic form
  public string Summary { get; set; }
}

public enum Priority
{
  Low,
  Medium,
  High,
}