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
        
        public virtual bool  CreateAuditResponse(AuditRequest auditRequest, AuditResponse auditResponse, int projectId)
        {
            using (context = new AuditManagementSystemContext())
            {
                AuditManagement manager = new AuditManagement();
                manager.ProjectId = projectId;
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
                return true;
            }
        }
        public virtual AuditManagement ReadAuditManagement(int projectId)
        {
            using (context = new AuditManagementSystemContext())
            {
                return context.AuditManagements.Where(a => a.ProjectId == projectId).OrderByDescending(a=>a.ManagementId).FirstOrDefault();
            }
        }

        public virtual int ReadProjectId(string managerName)
        {
            using (context = new AuditManagementSystemContext())
            {
                return context.Logindetails.Where(a => a.UserName == managerName).FirstOrDefault().ProjectId;
            }
        }
    }
}
