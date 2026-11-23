using SiphelelePortfolio.Data.DataAccess;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Repositories.Interfaces;

namespace SiphelelePortfolio.Data.Repositories.Implementations;

public class CertificateRepository : ICertificateRepository
{
    private readonly ISqlDataAccess _db;
    public CertificateRepository(ISqlDataAccess db) => _db = db;

    public async Task<IEnumerable<Certificate>> GetAllAsync()
        => await _db.QueryAsync<Certificate>("usp_Certificate_GetAll");

    public async Task<Certificate?> GetByIdAsync(int id)
        => await _db.QuerySingleAsync<Certificate>("usp_Certificate_GetById", new { CertificateId = id });

    public async Task<int> InsertAsync(Certificate c)
        => await _db.ExecuteScalarAsync<int>("usp_Certificate_Insert", new
        {
            c.Title,
            c.IssuingOrganization,
            c.IssueDate,
            c.CertificateUrl
        });

    public async Task UpdateAsync(Certificate c)
        => await _db.ExecuteAsync("usp_Certificate_Update", new
        {
            c.CertificateId,
            c.Title,
            c.IssuingOrganization,
            c.IssueDate,
            c.CertificateUrl,
            c.IsActive
        });

    public async Task DeleteAsync(int id)
        => await _db.ExecuteAsync("usp_Certificate_Delete", new { CertificateId = id });
}