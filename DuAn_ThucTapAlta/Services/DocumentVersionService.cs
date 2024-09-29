using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAn_ThucTapAlta.Services
{
    public class DocumentVersionService : IDocumentVersionService
    {
        private readonly ApplicationDBContext _context;

        public DocumentVersionService(ApplicationDBContext context)    
        {
            _context = context;
        }

        public async Task<DocumentVersion> GetDocumentVersionByIdAsync(int versionId)
        {
            return await _context.DocumentVersions.FindAsync(versionId);
        }

        public async Task<IEnumerable<DocumentVersion>> GetAllDocumentVersionsAsync()
        {
            return await _context.DocumentVersions.ToListAsync();
        }

        public async Task<DocumentVersion> CreateDocumentVersionAsync(DocumentVersion documentVersion)
        {
            _context.DocumentVersions.Add(documentVersion);
            await _context.SaveChangesAsync();
            return documentVersion;
        }

        public async Task<DocumentVersion> UpdateDocumentVersionAsync(DocumentVersion documentVersion)
        {
            _context.Entry(documentVersion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return documentVersion;
        }
    }
}
