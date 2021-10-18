using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;
using AuditSeverityMicroService.RepositoryLayer;

namespace AuditSeverityMicroService.ServiceLayer
{
    interface IAuditSeverityService
    {
        public string GenerateAuditId();
        public List<string> AuditResponseCalculation(int NoCount, string AuditType, List<AuditBenchmarkClass> benchMarkList);
        public void CreateRepo(AuditRequest auditRequest, AuditResponse auditResponse, int projectId);
        //public int GetProjectIdCount(int projectId);
        public AuditResponse ReadAuditResponse(int projectId);
        public int GetProjectId(string managerName);

    }
}
