using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;

namespace AuditSeverityMicroService.ServiceLayer
{
    interface IAuditSeverityService
    {
        public string GenerateAuditId();
        public List<string> AuditResponseCalculation(int NoCount, string AuditType, List<AuditBenchmarkClass> benchMarkList);
        public Task<bool> UpdateRepo(AuditRequest auditRequest, AuditResponse auditResponse, int projectId);

    }
}
