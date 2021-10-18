using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroService.Models
{
    public class AuditRequest
    {
        //public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManagerName { get; set; }
        public string ApplicationOwnerName { get; set; }
        public AuditDetail auditDetail { get; set; }
    }
}
