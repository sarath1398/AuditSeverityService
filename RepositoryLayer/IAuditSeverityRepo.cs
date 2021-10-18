using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;

namespace AuditSeverityMicroService.RepositoryLayer
{
    interface IAuditSeverityRepo
    {
        public bool CreateAuditResponse(AuditRequest auditRequest, AuditResponse auditResponse, int projectId);
        public AuditManagement ReadAuditManagement(int projectId);
        public int ReadProjectId(string managerName);
    }
}
