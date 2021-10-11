using System;
using System.Collections.Generic;

#nullable disable

namespace AuditSeverityMicroService.RepositoryLayer
{
    public partial class AuditManagement
    {
        public int ProjectId { get; set; }
        public string ProjectManagerName { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationOwnerName { get; set; }
        public string AuditType { get; set; }
        public DateTime? AuditDate { get; set; }
        public string AuditId { get; set; }
        public string ProjectExecutionStatus { get; set; }
        public string RemedialActionDuration { get; set; }
    }
}
