namespace CourseCatalog.Domain.Entities
{
    public class DraftEndorsement
    {
        public int DraftEndorsementId { get; set; }
        public int EndorsementId { get; set; }
        public int DraftId { get; set; }
        public Endorsement Endorsement { get; set; }

    }
}