namespace DuAn_ThucTapAlta.DTO.WorkGroup
{
    public class CreateWorkGroupRequestDTO
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Member { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
