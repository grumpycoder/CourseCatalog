namespace CourseCatalog.Domain.Entities
{
    public class Certificate
    {
        //public int CertificateId { get; set; }
        public string TchNumber { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string FullName { get; set; }
        public string EndorseCode { get; set; }
        public int EndorsementId { get; set; }
    }
}
