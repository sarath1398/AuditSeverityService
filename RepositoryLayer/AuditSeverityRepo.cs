using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroService.Models;
namespace AuditSeverityMicroService.RepositoryLayer
{
    public class AuditSeverityRepo:IAuditSeverityRepo
    {
        AuditManagementSystemContext context;
        public AuditSeverityRepo()
        {

        }
        public AuditSeverityRepo(AuditManagementSystemContext _context)
        {
            context = _context;
        }
        public int GetProjectCount(int projectId)
        {
            using (context = new AuditManagementSystemContext())
            {
                int cnt = context.AuditManagements.Where(a => a.ProjectId == projectId).Count();
                return cnt;
            }
        }
        public void  CreateAuditResponse(AuditRequest auditRequest, AuditResponse auditResponse, int projectId)
        {
            using (context = new AuditManagementSystemContext())
            {
                AuditManagement manager = new AuditManagement();
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
                context.SaveChanges();
            }
        }
        public AuditManagement ReadAuditManagement()//int projectId)
        {
            using (context = new AuditManagementSystemContext())
            {
                return context.AuditManagements.ToList()[context.AuditManagements.ToList().Count-1];//context.AuditManagements.Where(a => a.ProjectId == projectId).FirstOrDefault();
            }
        }

    }
}
