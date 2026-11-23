using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Repositories.Interfaces;

public interface ICollaborationRepository
{
    Task<int> InsertAsync(Collaboration c);
    Task<IEnumerable<Collaboration>> GetAllAsync();
    Task<int> GetUnreadCountAsync();
    Task MarkReadAsync(int id);
    Task<IEnumerable<(string YearMonth, int TotalCollabs)>> GetMonthlyStatsAsync(int monthsBack = 6);
}