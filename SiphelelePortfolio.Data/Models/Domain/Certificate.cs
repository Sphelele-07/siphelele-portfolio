using System.ComponentModel.DataAnnotations;

namespace SiphelelePortfolio.Data.Models.Domain;

public class Certificate
{
    public int CertificateId { get; set; }

    [Required, StringLength(200, MinimumLength = 3)]
    [Display(Name = "Certificate Title")]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(150, MinimumLength = 2)]
    [Display(Name = "Issuing Organization")]
    public string IssuingOrganization { get; set; } = string.Empty;

    [Required, DataType(DataType.Date)]
    [Display(Name = "Issue Date")]
    public DateTime IssueDate { get; set; } = DateTime.Today;

    [StringLength(300)]
    [Display(Name = "Certificate PDF URL")]
    public string? CertificateUrl { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}