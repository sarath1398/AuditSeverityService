using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;

namespace AuditSeverityMicroService.RepositoryLayer
{
    interface IAuditSeverityRepo
    {
        public void CreateAuditResponse(AuditRequest auditRequest, AuditResponse auditResponse, int projectId);
        public int GetProjectCount(int projectId);
        public AuditManagement ReadAuditManagement();//int projectId);
    }
}
