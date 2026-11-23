using SiphelelePortfolio.Data.DataAccess;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Repositories.Interfaces;

namespace SiphelelePortfolio.Data.Repositories.Implementations;

public class ProjectRepository : IProjectRepository
{
    private readonly ISqlDataAccess _db;
    public ProjectRepository(ISqlDataAccess db) => _db = db;

    public async Task<IEnumerable<Project>> GetAllAsync()
        => await _db.QueryAsync<Project>("usp_Project_GetAll");

    public async Task<Project?> GetByIdAsync(int id)
        => await _db.QuerySingleAsync<Project>("usp_Project_GetById", new { ProjectId = id });

    public async Task<int> InsertAsync(Project p)
    => await _db.ExecuteScalarAsync<int>("usp_Project_Insert", new
    {
        p.Title,
        p.Type,
        p.ImagePath,
        p.Tags,
        p.Problem,
        p.Solution,
        p.Result,
        p.DemoVideoUrl,
        p.LiveUrl,
        p.RequirementsPdfPath,
        p.DisplayOrder
    });

    public async Task UpdateAsync(Project p)
        => await _db.ExecuteAsync("usp_Project_Update", new
        {
            p.ProjectId,
            p.Title,
            p.Type,
            p.ImagePath,
            p.Tags,
            p.Problem,
            p.Solution,
            p.Result,
            p.DemoVideoUrl,
            p.LiveUrl,
            p.RequirementsPdfPath,
            p.DisplayOrder,
            p.IsActive
        });

    public async Task DeleteAsync(int id)
        => await _db.ExecuteAsync("usp_Project_Delete", new { ProjectId = id });
}