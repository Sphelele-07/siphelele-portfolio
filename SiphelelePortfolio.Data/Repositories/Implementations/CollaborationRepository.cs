using SiphelelePortfolio.Data.DataAccess;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Repositories.Interfaces;

namespace SiphelelePortfolio.Data.Repositories.Implementations;

public class CollaborationRepository : ICollaborationRepository
{
    private readonly ISqlDataAccess _db;
    public CollaborationRepository(ISqlDataAccess db) => _db = db;

    public async Task<int> InsertAsync(Collaboration c)
    => await _db.ExecuteScalarAsync<int>("usp_Collaboration_Insert", new
    {
        c.FullName,
        c.Email,
        c.Subject,
        c.Message
    });

    public async Task<IEnumerable<Collaboration>> GetAllAsync()
        => await _db.QueryAsync<Collaboration>("usp_Collaboration_GetAll");

    public async Task<int> GetUnreadCountAsync()
    => await _db.ExecuteScalarAsync<int>("usp_Collaboration_GetUnreadCount");

    public async Task MarkReadAsync(int id)
        => await _db.ExecuteAsync("usp_Collaboration_MarkRead", new { CollaborationId = id });

    public async Task<IEnumerable<(string YearMonth, int TotalCollabs)>> GetMonthlyStatsAsync(int monthsBack = 6)
    {
        var rows = await _db.QueryAsync<dynamic>("usp_Engagement_GetMonthlyCollaborations", new { MonthsBack = monthsBack });
        return rows.Select(r => ((string)r.YearMonth, (int)r.TotalCollabs)).ToList();
    }
}