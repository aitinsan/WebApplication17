using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.Core.DTO;
using WebApplication17.Core.Entities;
using WebApplication17.Core.Interfaces.Repositories;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IQueueRepository _queueRepository;

        public QueueController(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMessage(MessageDTO message)
        {
            Message msg = new Message()
            {
                Id = Guid.NewGuid(),
                Type = message.Type,
                Handled = false,
                JsonContent = message.JsonContent,
                AddedAt = DateTime.Now
            };

            if (await _queueRepository.AddMessage(msg))
            {
                return Ok("A new message was added successfully!");
            }

            return BadRequest("Oops, something went wrong!");
        }

        [HttpGet("handled/{message_id}")]
        public async Task<IActionResult> SetHandled(Guid message_id)
        {
            if (await _queueRepository.SetHandled(message_id))
            {
                return Ok("Requested message was set to handled state successfully!");
            }

            return BadRequest("Oops, something went wrong during check of handled state!");
        }

        [HttpGet("retrieve/email")]
        public async Task<IActionResult> GetUnhandledEmail()
        {
            var eMessages = await _queueRepository.GetUnhandledEmailMessage();
            return Ok(eMessages);
        }

        [HttpGet("retrieve/log")]
        public async Task<IActionResult> GetUnhandledLog()
        {
            var logMessages = await _queueRepository.GetUnhandledLoggingMessage();
            return Ok(logMessages);
        }
    }
}
