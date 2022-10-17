using Microsoft.EntityFrameworkCore;
using MozaeekCore.Core.MessagePublisher;

namespace MozaeekCore.OutBoxManagement
{
    public class EventStoreContext:DbContext
    {
        public EventStoreContext()
        {

        }

        public EventStoreContext(DbContextOptions<EventStoreContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutboxMessage>().ToTable("OutboxMessage");
            base.OnModelCreating(modelBuilder);
        }
    }
}