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

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return false;
            }
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
