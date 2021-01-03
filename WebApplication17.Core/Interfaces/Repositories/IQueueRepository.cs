using WebApplication17.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication17.Core.Interfaces.Repositories
{
    public interface IQueueRepository
    {
        Task<bool> AddMessage(Message message);
        Task<bool> SetHandled(Guid messageId);
        Task<Message> GetUnhandledEmailMessage();
        Task<Message> GetUnhandledLoggingMessage();
    }
}
