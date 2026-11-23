using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<int> InsertAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(int id);
}