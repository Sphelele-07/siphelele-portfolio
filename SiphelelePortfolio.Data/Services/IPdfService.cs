using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Services;

public interface IPdfService
{
    byte[] GenerateRequirementsPdf(Project project);
}