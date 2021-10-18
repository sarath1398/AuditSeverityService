using AuditSeverityMicroService.Models;
using AuditSeverityMicroService.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuditSeverityMicroService.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuditSeverityController : ControllerBase
    {
        AuditSeverityService auditSeverityService=new AuditSeverityService();
        
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        AuditRequest auditRequest = new AuditRequest();
        
        //[AllowAnonymous]
        [Authorize]
        [HttpPost("ProjectExecutionStatus")]
        public async Task<ActionResult> ProjectExecutionStatus([FromBody] AuditRequest request)//string[] request)
        {
            _log4net.Info("In AuditSeverityController");
            /*var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {*/
                        this.auditRequest = request;
                        _log4net.Info(request.ProjectManagerName);
                        List<AuditBenchmarkClass> BenchMark = await auditSeverityService.ReadBenchmark();
                        if (BenchMark != null)
                        {
                            _log4net.Info("Benchmark api data is sucessfully read");
                        }
                        else
                        {
                            _log4net.Error("Benchmark api server error");
                            return new BadRequestResult();//500);
                        }
                        int NoCount = auditRequest.auditDetail.ListOfQuestions.Select(x => x).Where(x => x == "No").Count();
                        AuditResponse auditResponse = new AuditResponse();

                        auditResponse.AuditId = auditSeverityService.GenerateAuditId();
                        List<string> response = auditSeverityService.AuditResponseCalculation(NoCount, auditRequest.auditDetail.AuditType, BenchMark);
                        auditResponse.ProjectExecutionStatus = response[0];
                        auditResponse.RemedialActionDuration = response[1];
                        int ProjectId = auditSeverityService.GetProjectId(auditRequest.ProjectManagerName);

                        auditSeverityService.CreateRepo(auditRequest, auditResponse, ProjectId);
                        _log4net.Info("Audit Response calculation successful");
                        return Ok(auditSeverityService.ReadAuditResponse(ProjectId));

            /*}
            else
            {
                _log4net.Error("Model state is invalid");
                return new BadRequestResult();//auditRequest.ProjectId));
            }
        }
        catch (Exception e)
        {
            _log4net.Error(e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
        //return Ok(request);
    }
    else
    {
        return Unauthorized("User is unauthorized");
    }*/

        }
    }
}

