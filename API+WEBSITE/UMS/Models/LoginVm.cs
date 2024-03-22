
using System.ComponentModel.DataAnnotations;

namespace UMS.Models;

public class LoginVm
{
    public Guid UserId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}