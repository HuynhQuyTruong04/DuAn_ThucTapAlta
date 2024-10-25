using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Documents;
using DuAn_ThucTapAlta.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAn_ThucTapAlta.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDBContext _context;
        public DocumentService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync(s => s.DocumentId == id);
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> UpdateDocumentAsync(int id, UpdateDocumentRequestDTO updateDto)
        {
            var existingDocument = await _context.Documents.FirstOrDefaultAsync(x => x.DocumentId == id);

            if (existingDocument == null)
            {
                return null;
            }

            existingDocument.DocumentName = updateDto.DocumentName;
            existingDocument.DocumentType = updateDto.DocumentType;
            existingDocument.CreateDate = updateDto.CreateDate;
            existingDocument.Creator = updateDto.Creator;
            existingDocument.Status = updateDto.Status;
            existingDocument.LastedVersion = updateDto.LastedVersion;
            existingDocument.UserId = updateDto.UserId;
            existingDocument.FlightId = updateDto.FlightId;

            await _context.SaveChangesAsync();
            return existingDocument;
        }

        public async Task<bool> DeactivateDocumentAsync(int id)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(x => x.DocumentId == id);
            if (document == null)
            {
                return false;
            }

            document.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivateDocumentAsync(int id)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(x => x.DocumentId == id);
            if (document == null || document.IsActive) 
            {
                return false;
            }

            document.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Document>> GetInactiveDocumentsAsync()
        {
            return await _context.Documents
                                 .Where(d => !d.IsActive)
                                 .ToListAsync();
        }
    }
}
