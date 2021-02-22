namespace CourseCatalog.Domain.Entities
{
    public class Endorsement
    {
        public int EndorseId { get; set; }
        public string EndorseCode { get; set; }
        public string Description { get; set; }
        public bool? IsStillIssued { get; set; }
    }
}