using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);

            if (document == null)
            {
                return NotFound("Tài liệu không tồn tại!");
            }

            return Ok(document);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (document == null)
            {
                return BadRequest("Tài liệu không hợp lệ!");
            }

            var createDocument = await _documentService.CreateDocumentAsync(document);

            return CreatedAtAction(nameof(GetDocument), new { id = createDocument.DocumentId }, createDocument);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != document.DocumentId)
            {
                return BadRequest("ID không hợp lệ!");
            }

            var updateDocument = await _documentService.UpdateDocumentAsync(document);

            if (updateDocument == null)
            {
                return NotFound("Tài liệu không tồn tại!");
            }

            return Ok(updateDocument);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var isDelete = await _documentService.DeleteDocumentAsync(id);

            if (!isDelete)
            {
                return NotFound("Tài liệu không tồn tại!");
            }

            return NoContent();
        }
    }
}
