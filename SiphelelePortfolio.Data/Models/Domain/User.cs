using System.ComponentModel.DataAnnotations;

namespace SiphelelePortfolio.Data.Models.Domain;

public class User
{
    public int UserId { get; set; }

    [Required, EmailAddress, StringLength(150)]
    [Display(Name = "Email")]
    public string Username { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [StringLength(100)]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
}