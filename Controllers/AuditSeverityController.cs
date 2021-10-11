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

        [HttpGet]
        public async Task<ActionResult> ProjectExecutionStatus()//[FromBody] AuditRequest auditRequest)
        {
            _log4net.Info("Project Execution status invoked");
            AuditRequest auditRequest = new AuditRequest()
            {
                ProjectId = 15,
                ProjectName = "Bank",
                ProjectManagerName = "Divya",
                ApplicationOwnerName = "Akshaya",
                auditDetail = new AuditDetail()
                {
                    AuditType = "SOX",
                    AuditDate = DateTime.Parse("09/10/2021"),
                    AuditQuestions = new List<string>() { "yes", "yes", "yes", "yes", "no" }
                },
            };
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            List<AuditBenchmarkClass> BenchMark = new List<AuditBenchmarkClass>();
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:44354/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("api/AuditBenchmark");
                if (response.IsSuccessStatusCode)
                {
                    BenchMark = await response.Content.ReadAsAsync<List<AuditBenchmarkClass>>();
                    //Console.WriteLine(BenchMark.Count);
                    _log4net.Info(BenchMark.Count);
                }
                else
                {
                    _log4net.Error("Internal server Error");

                }
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    _log4net.Error("Bad Request");
                    return new BadRequestObjectResult(ModelState);
                }
                else
                {
                    int NoCount = auditRequest.auditDetail.AuditQuestions.Select(x => x).Where(x => x == "no").Count();
                    AuditResponse auditResponse = new AuditResponse();
                    AuditSeverityService auditSeverityService = new AuditSeverityService();
                    auditResponse.AuditId = auditSeverityService.GenerateAuditId();
                    List<string> response = auditSeverityService.AuditResponseCalculation(NoCount, auditRequest.auditDetail.AuditType, BenchMark);
                    auditResponse.ProjectExecutionStatus = response[0];
                    auditResponse.RemedialActionDuration = response[1];
                    if (await auditSeverityService.UpdateRepo(auditRequest, auditResponse, auditRequest.ProjectId))
                    {
                        _log4net.Info("Audit Response calculation successful");
                        return Ok(auditResponse);
                    }
                    else
                    {
                        _log4net.Debug("Error occureed while updating database");
                        return StatusCode(404);
                    }
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
