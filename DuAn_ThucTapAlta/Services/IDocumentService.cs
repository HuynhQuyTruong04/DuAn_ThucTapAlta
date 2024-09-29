using DuAn_ThucTapAlta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAn_ThucTapAlta.Services
{
    public interface IDocumentService
    {
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<Document> CreateDocumentAsync(Document document);
        Task<Document> UpdateDocumentAsync(Document document);
    }
}
