using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Documents;
using DuAn_ThucTapAlta.Mappers;
using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService, ApplicationDBContext context)
        {
            _context = context;
            _documentService = documentService;
        }

        [Authorize]
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Pilot,Manager, Stewardess")]
        public async Task<IActionResult> GetDocument(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var document = await _documentService.GetDocumentByIdAsync(id);

            if (document == null)
            {
                return NotFound("Không tìm thấy Document!");
            }

            return Ok(document.ToDocumentDTO());
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin,Pilot,Manager, Stewardess")]
        public async Task<IActionResult> GetAllDocuments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documents = await _documentService.GetAllDocumentsAsync();

            var documentDto = documents.Select(s => s.ToDocumentDTO()).ToList();

            return Ok(documentDto);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin,Pilot,Manager, Stewardess")]
        public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentRequestDTO documentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (documentDto == null)
            {
                return BadRequest("Tài liệu không hợp lệ!");
            }

            var documentModel = documentDto.ToDocumentFromCreateDTO();

            await _documentService.CreateDocumentAsync(documentModel);

            return CreatedAtAction(nameof(GetDocument), new { id = documentModel.DocumentId }, documentModel.ToDocumentDTO());
        }

        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Roles = "Admin,Pilot,Manager, Stewardess")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] UpdateDocumentRequestDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentModel = await _documentService.UpdateDocumentAsync(id, updateDto);

            if (documentModel == null)
            {
                return NotFound("Tài liệu không tồn tại.");
            }

            return Ok(documentModel.ToDocumentDTO());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var result = await _documentService.DeactivateDocumentAsync(id);

            if (!result)
            {
                return NotFound("Tài liệu không tồn tại hoặc đã bị vô hiệu hóa.");
            }

            return Ok("Tài liệu đã được vô hiệu hóa.");
        }

        [HttpPut("{id}/activate")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ActivateDocument(int id)
        {
            var result = await _documentService.ActivateDocumentAsync(id);

            if (!result)
            {
                return NotFound("Tài liệu không tồn tại hoặc đã đang hoạt động.");
            }

            return Ok("Tài liệu đã được kích hoạt.");
        }

        [HttpGet("inactive")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetInactiveDocuments()
        {
            var inactiveDocuments = await _documentService.GetInactiveDocumentsAsync();

            if (!inactiveDocuments.Any())
            {
                return NotFound("Không có tài liệu nào bị vô hiệu hóa.");
            }

            var documentDtos = inactiveDocuments.Select(d => d.ToDocumentDTO()).ToList();
            return Ok(documentDtos);
        }
    }
}
