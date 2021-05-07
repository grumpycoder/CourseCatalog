using System;

namespace CourseCatalog.Domain.Entities
{
    public class ErrorLogDetail
    {
        public int LogId { get; set; }
        public DateTime? Timestamp { get; set; }

        public string Message { get; set; }
        public string Level { get; set; }
        public string LogEvent { get; set; }

        // WHERE
        public string Location { get; set; }
        public string Hostname { get; set; }

        // WHO
        public string UserName { get; set; }

        // EVERYTHING ELSE
        //public decimal? ElapsedMilliseconds { get; set; } // only for performance entries

        public string Exception { get; set; } // the exception for error logging

        ////public CustomException CustomException { get; set; }
        public string CorrelationId { get; set; } // exception shielding from server to client
        ////public Dictionary<string, object> AdditionalInfo { get; set; }  // everything else
    }
}