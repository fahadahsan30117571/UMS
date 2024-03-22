using System.ComponentModel.DataAnnotations;

namespace UMS.Models;

public class AppUser
{
    public Guid Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string Password { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsAdmin { get; set; }
}