using System;

namespace CourseCatalog.Domain.Entities
{
    public class PerformanceLogDetail
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        // WHERE
        public string Application { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }

        // WHO
        public string UserId { get; set; }
        public string UserName { get; set; }

        // EVERYTHING ELSE
        public long? ElapsedMilliseconds { get; set; } // only for performance entries

        //public Exception Exception { get; set; }  // the exception for error logging
        //public CustomException CustomException { get; set; }
        public string CorrelationId { get; set; } // exception shielding from server to client
        public string AdditionalInfo { get; set; }
    }
}