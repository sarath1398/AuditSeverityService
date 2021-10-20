using AuditSeverityMicroService.Models;
using AuditSeverityMicroService.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        IAuditSeverityService auditSeverityService;
        IConfiguration iconfiguration;
        public AuditSeverityController(IAuditSeverityService _service,IConfiguration _iconfiguration)
        {
            auditSeverityService = _service;
            iconfiguration = _iconfiguration;
        }
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        AuditRequest auditRequest = new AuditRequest();
        [Authorize]
        [HttpPost("ProjectExecutionStatus")]
        public async Task<ActionResult> ProjectExecutionStatus([FromBody] AuditRequest request)
        {
            _log4net.Info("In AuditSeverityController");
            this.auditRequest = request;
            _log4net.Info(request.ProjectManagerName);
            string benchmarkurl = iconfiguration["benchmarkurl"];
            List<AuditBenchmarkClass> BenchMark = await auditSeverityService.ReadBenchmark(benchmarkurl);
            if (BenchMark != null)
            {
                _log4net.Info("Benchmark api data is sucessfully read");
            }
            else
            {
                _log4net.Error("Benchmark api server error");
                return new BadRequestResult();
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

            
        }
    }
}

