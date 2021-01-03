using Microsoft.EntityFrameworkCore;
using WebApplication17.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication17.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Message> Messages { get; set; }
    }
}
