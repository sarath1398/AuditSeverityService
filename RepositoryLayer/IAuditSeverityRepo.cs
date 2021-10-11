using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;

namespace AuditSeverityMicroService.RepositoryLayer
{
    interface IAuditSeverityRepo
    {
        public Task<bool> UpdateAuditResponse(AuditRequest auditRequest, AuditResponse auditResponse, int projectId);

    }
}
