using Commandor.Models;
using Microsoft.EntityFrameworkCore;

namespace Commandor.Data
{
    public class CommandorContext : DbContext
    {
        public CommandorContext(DbContextOptions<CommandorContext> opt) : base(opt)
        {

        }

        public DbSet<Command>? Commands { get; set; }

    }
}