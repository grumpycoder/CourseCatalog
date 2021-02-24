using CourseCatalog.Domain.Common;

namespace CourseCatalog.Domain.Entities
{
    public class DraftEndorsement: AuditableEntity
    {
        public int DraftEndorsementId { get; set; }
        public int EndorsementId { get; set; }
        public int DraftId { get; set; }
        public Endorsement Endorsement { get; set; }

    }
}