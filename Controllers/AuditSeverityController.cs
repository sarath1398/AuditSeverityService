using AuditSeverityMicroService.Models;
using AuditSeverityMicroService.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditSeverityMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditSeverityController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        //Dictionary<string, int> BenchMarkDict = new() { { "Internal", 3 }, { "SOX", 1 } };
        AuditRequest auditRequest = new AuditRequest();
        AuditSeverityService auditSeverityService = new AuditSeverityService();

        [HttpPost("Auditrequest")]
        public async Task<ActionResult> Auditrequest([FromBody] AuditRequest request)//string[] request)
        {
            this.auditRequest = request;
            _log4net.Info(request.ProjectId);
            List<AuditBenchmarkClass> BenchMark = new List<AuditBenchmarkClass>();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage httpresponse = await client.GetAsync("api/AuditBenchmark");
                if (httpresponse.IsSuccessStatusCode)
                {
                    BenchMark = await httpresponse.Content.ReadAsAsync<List<AuditBenchmarkClass>>();
                    //Console.WriteLine(BenchMark.Count);
                    _log4net.Info("Bechmark api is called");
                }
                else
                {
                    _log4net.Error("Internal server Error");

                }
            }

            int NoCount = auditRequest.auditDetail.ListOfQuestions.Select(x => x).Where(x => x == "no").Count();
            AuditResponse auditResponse = new AuditResponse();
            
            auditResponse.AuditId = auditSeverityService.GenerateAuditId();
            List<string> response = auditSeverityService.AuditResponseCalculation(NoCount, auditRequest.auditDetail.AuditType, BenchMark);
            auditResponse.ProjectExecutionStatus = response[0];
            auditResponse.RemedialActionDuration = response[1];
            if (auditSeverityService.GetProjectIdCount(auditRequest.ProjectId) == 0)//CreateRepo(auditRequest, auditResponse, auditRequest.ProjectId))
            {
                auditSeverityService.CreateRepo(auditRequest, auditResponse, auditRequest.ProjectId);
                _log4net.Info("Audit Response calculation successful");
                return Ok(auditResponse);
            }
            else
            _log4net.Debug("Existing audit response is read");
            return Ok(auditSeverityService.ReadAuditResponse());//auditRequest.ProjectId));
            //return Ok(request);
        }

        [HttpGet("ProjectAuditresult")]
        public async Task<ActionResult> ProjectExecutionStatus()//[FromBody] AuditRequest auditRequest)
        {
            _log4net.Info("Project Execution status invoked");
            try
            {
                if (!ModelState.IsValid)
                {
                    _log4net.Error("Bad Request");
                    return new BadRequestObjectResult(ModelState);
                }
                else
                {
                    _log4net.Debug("Existing audit response is read");
                    return Ok(auditSeverityService.ReadAuditResponse());//auditRequest.ProjectId));
                }


            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }
        
    }
}
