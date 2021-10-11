using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;
using AuditSeverityMicroService.RepositoryLayer;

namespace AuditSeverityMicroService.ServiceLayer
{
    public class AuditSeverityService
    {
        public string GenerateAuditId()
        {
            string auditId = "";
            Random random = new Random();
            auditId += "A" + random.Next(1000, 9999).ToString();
            return auditId;
        }
        public List<string> AuditResponseCalculation(int NoCount, string AuditType, List<AuditBenchmarkClass> benchMarkList)
        {
            string AuditResult;
            string RemedialActionDuration;
            List<string> auditResponse = new List<string>();
            if (AuditType == "Internal")
            {
                if (NoCount > benchMarkList[0].BenchmarkNoAnswers)
                {
                    AuditResult = "RED";
                    RemedialActionDuration = "Action to be taken in 2 weeks";
                }
                else
                {
                    AuditResult = "GREEN";
                    RemedialActionDuration = "No action needed";
                }
            }
            else
            {
                if (NoCount > benchMarkList[1].BenchmarkNoAnswers)
                {
                    AuditResult = "RED";
                    RemedialActionDuration = "Action to be taken in 1 week";
                }
                else
                {
                    AuditResult = "GREEN";
                    RemedialActionDuration = "No action needed";
                }
            }
            auditResponse.Add(AuditResult);
            auditResponse.Add(RemedialActionDuration);
            return auditResponse;
        }
        public async Task<bool> UpdateRepo(AuditRequest auditRequest, AuditResponse auditResponse, int projectId)
        {
            AuditSeverityRepo repo = new AuditSeverityRepo();
            return await repo.UpdateAuditResponse(auditRequest, auditResponse, projectId);
        }
    }
}
