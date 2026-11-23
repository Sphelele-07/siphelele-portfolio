using SiphelelePortfolio.Data.DataAccess;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Repositories.Interfaces;

namespace SiphelelePortfolio.Data.Repositories.Implementations;

public class EngagementRepository : IEngagementRepository
{
    private readonly ISqlDataAccess _db;
    public EngagementRepository(ISqlDataAccess db) => _db = db;

    public async Task LogAsync(Engagement e)
        => await _db.ExecuteAsync("usp_Engagement_Insert", new
        {
            e.EventType,
            e.TargetId,
            e.TargetLabel,
            e.UserAgent,
            e.IpAddress
        });

    public async Task<IEnumerable<Engagement>> GetAllAsync()
        => await _db.QueryAsync<Engagement>("usp_Engagement_GetAll");

    public async Task<IEnumerable<(string YearMonth, int TotalEngagements, int Clicks)>> GetStatsByMonthAsync(int monthsBack = 6)
    {
        var rows = await _db.QueryAsync<dynamic>("usp_Engagement_GetStatsByMonth", new { MonthsBack = monthsBack });
        return rows.Select(r => ((string)r.YearMonth, (int)r.TotalEngagements, (int)r.Clicks)).ToList();
    }
}