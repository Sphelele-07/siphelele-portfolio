using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Repositories.Interfaces;

public interface IEngagementRepository
{
    Task LogAsync(Engagement e);
    Task<IEnumerable<Engagement>> GetAllAsync();
    Task<IEnumerable<(string YearMonth, int TotalEngagements, int Clicks)>> GetStatsByMonthAsync(int monthsBack = 6);
}