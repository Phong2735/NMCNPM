using System.ComponentModel.DataAnnotations;
namespace NMCNPM.Models
{
    public class Admin
{
    public int AdminID { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    public string Email { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Now;
}
}
