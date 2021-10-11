using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;
namespace AuditSeverityMicroService.RepositoryLayer
{
    public class AuditSeverityRepo
    {
        AuditManagementSystemContext context;
        public AuditSeverityRepo()
        {

        }
        public AuditSeverityRepo(AuditManagementSystemContext _context)
        {
            context = _context;
        }
        public async Task<bool> UpdateAuditResponse(AuditRequest auditRequest, AuditResponse auditResponse, int projectId)
        {
            using (context = new AuditManagementSystemContext())
            {
                int project_count = context.AuditManagements.Where(a => a.ProjectId == projectId).Count();
                AuditManagement manager = new AuditManagement();
                if (project_count==0)
                {
                    manager.ProjectId = auditRequest.ProjectId;
                    manager.ProjectManagerName = auditRequest.ProjectManagerName;
                    manager.ProjectName = auditRequest.ProjectName;
                    manager.ApplicationOwnerName = auditRequest.ApplicationOwnerName;
                    manager.AuditType = auditRequest.auditDetail.AuditType;
                    manager.AuditDate = auditRequest.auditDetail.AuditDate;
                    manager.AuditId = auditResponse.AuditId;
                    manager.ProjectExecutionStatus = auditResponse.ProjectExecutionStatus;
                    manager.RemedialActionDuration = auditResponse.RemedialActionDuration;
                    context.AuditManagements.Add(manager);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
