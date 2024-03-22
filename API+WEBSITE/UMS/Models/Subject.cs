using System.ComponentModel.DataAnnotations;

namespace UMS.Models;

public class Subject
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

}