using Microsoft.EntityFrameworkCore;
using WebApplication17.Core.Entities;
using WebApplication17.Core.Interfaces.Repositories;
using WebApplication17.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication17.Infrastructure.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly DataContext _dbContext;

        public QueueRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddMessage(Message message)
        {
            _dbContext.Messages.Add(message);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<Message> GetUnhandledEmailMessage()
        {
            var eMessage = await _dbContext.Messages.Where(x => !x.Handled && x.Type == "email").OrderBy(x => x.AddedAt).FirstOrDefaultAsync();
            return eMessage;
        }

        public async Task<Message> GetUnhandledLoggingMessage()
        {
            var logMessage = await _dbContext.Messages.Where(x => !x.Handled && x.Type == "log").OrderBy(x => x.AddedAt).FirstOrDefaultAsync();
            return logMessage;
        }

        public async Task<bool> SetHandled(Guid messageId)
        {
            var handledMessage = _dbContext.Messages.Where(x => x.Id == messageId).FirstOrDefault();
            if (handledMessage == null)
                return false;
            handledMessage.Handled = true;
            handledMessage.HandledAt = DateTime.Now;
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
