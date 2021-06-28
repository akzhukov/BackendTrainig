using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocketsShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebSocketsService.Hubs;
using WebSocketsService.Mapper;

namespace WebSocketsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubsController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;

        public HubsController(IServiceProvider serviceProvider, IMapper mapper)
        {
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public ActionResult<IList<HubDto>> GetAll()
        {
            List<BaseHub> channels = new();
            var hubTypes = Assembly.GetAssembly(GetType()).GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Hub)) && !type.IsAbstract)
                .ToList();
            foreach (var type in hubTypes)
            {
                var channel = serviceProvider.GetService(type) as BaseHub;
                channels.Add(channel);
            }
            return Ok(channels.Map()); 
        }

        [HttpPost("hook")]
        public IActionResult GetNextMessage(string hubName)
        {
            var hubType = Assembly.GetAssembly(GetType()).GetTypes()
               .Where(type => type.IsSubclassOf(typeof(Hub)) && !type.IsAbstract)
               .FirstOrDefault(hub => hub.Name == hubName);
            var hub = serviceProvider.GetService(hubType) as BaseHub;
            hub.Send();
            return Ok();
        }
    }
}
