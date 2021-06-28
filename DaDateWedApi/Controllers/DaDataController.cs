using DaDataConsoleApp;
using DaDataWebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaDataWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DaDataController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHubContext<DaDataHub> hubContext;

        public DaDataController(IConfiguration configuration, IHubContext<DaDataHub> hubContext)
        {
            this.configuration = configuration;
            this.hubContext = hubContext;
        }

        [HttpGet]
        [Route("inn")]
        public async Task<ActionResult<string>> GetCompanyName(string inn)
        {
            if (String.IsNullOrEmpty(inn))
            {
                return BadRequest("Empty inn");
            }
            CompanyService service = new CompanyService(configuration["DaDataToken"]);
            var name = await service.GetCompanyNameByINN(inn);
            if (name == null)
            {
                return NotFound();
            }
            await hubContext.Clients.All.SendAsync("GetCompanyName", name);
            return Ok(name);
        }
    }
}
