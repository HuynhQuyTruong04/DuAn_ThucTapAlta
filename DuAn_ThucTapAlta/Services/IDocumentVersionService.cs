using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Services
{
    public interface IDocumentVersionService
    {
        Task<DocumentVersion> GetDocumentVersionByIdAsync(int versionId);
        Task<IEnumerable<DocumentVersion>> GetAllDocumentVersionsAsync();
        Task<DocumentVersion> CreateDocumentVersionAsync(DocumentVersion documentVersion);
        Task<DocumentVersion> UpdateDocumentVersionAsync(DocumentVersion documentVersion);
    }
}
