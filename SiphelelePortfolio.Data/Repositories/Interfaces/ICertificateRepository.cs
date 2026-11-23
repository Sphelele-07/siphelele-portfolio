using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Repositories.Interfaces;

public interface ICertificateRepository
{
    Task<IEnumerable<Certificate>> GetAllAsync();
    Task<Certificate?> GetByIdAsync(int id);
    Task<int> InsertAsync(Certificate cert);
    Task UpdateAsync(Certificate cert);
    Task DeleteAsync(int id);
}