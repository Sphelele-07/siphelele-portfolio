using System.ComponentModel.DataAnnotations;

namespace SiphelelePortfolio.Data.Models.ViewModels;

public class CollaborationVM
{
    [Required(ErrorMessage = "Please enter your full name.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Name must be between 2 and 100 characters.")]
    [RegularExpression(@"^[A-Za-zÀ-ÿ'\- ]+$",
        ErrorMessage = "Name may only contain letters, spaces, hyphens and apostrophes.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(150)]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a subject.")]
    [StringLength(200, MinimumLength = 5,
        ErrorMessage = "Subject must be between 5 and 200 characters.")]
    [Display(Name = "Idea / Subject")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a message.")]
    [StringLength(1000, MinimumLength = 10,
        ErrorMessage = "Message must be between 10 and 1000 characters.")]
    [Display(Name = "Message")]
    public string Message { get; set; } = string.Empty;
}