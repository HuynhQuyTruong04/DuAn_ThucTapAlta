using DuAn_ThucTapAlta.DTO.Documents;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Mappers
{
    public static class DocumentMappers
    {
        public static DocumentDTO ToDocumentDTO(this Document documentModel)
        {
            return new DocumentDTO
            {
                DocumentId = documentModel.DocumentId,
                DocumentName = documentModel.DocumentName,
                DocumentType = documentModel.DocumentType,
                CreateDate = documentModel.CreateDate,
                Creator = documentModel.Creator,
                Status = documentModel.Status,
                LastedVersion = documentModel.LastedVersion,
                UserId = documentModel.UserId,
                FlightId = documentModel.FlightId              
            };
        }

        public static Document ToDocumentFromCreateDTO(this CreateDocumentRequestDTO documentDto)
        {
            return new Document
            {
                DocumentId = documentDto.DocumentId,
                DocumentName = documentDto.DocumentName,
                DocumentType = documentDto.DocumentType,
                CreateDate = documentDto.CreateDate,
                Creator = documentDto.Creator,
                Status = documentDto.Status,
                LastedVersion = documentDto.LastedVersion,
                UserId = documentDto.UserId,
                FlightId = documentDto.FlightId
            };
        }
    }
}
