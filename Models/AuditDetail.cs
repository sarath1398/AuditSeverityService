using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroService.Models
{
    public class AuditDetail
    {
        public string AuditType { get; set; }
        public DateTime AuditDate { get; set; }
        public string[] ListOfQuestions { get; set; }
    }
}
