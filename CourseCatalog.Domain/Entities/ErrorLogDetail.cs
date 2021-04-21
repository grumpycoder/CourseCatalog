using System;

namespace CourseCatalog.Domain.Entities
{
    public class ErrorLogDetail
    {
        public int LogId { get; set; }
        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        // WHERE
        //public string Application { get; set; }
        //public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }

        // WHO
        public string AlsdeId { get; set; }

        public string UserName { get; set; }

        // EVERYTHING ELSE
        public long? ElapsedMilliseconds { get; set; } // only for performance entries

        public string Exception { get; set; } // the exception for error logging

        //public CustomException CustomException { get; set; }
        public string CorrelationId { get; set; } // exception shielding from server to client
        //public Dictionary<string, object> AdditionalInfo { get; set; }  // everything else
    }
}