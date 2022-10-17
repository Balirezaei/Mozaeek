using Microsoft.EntityFrameworkCore;
using Mozaeek.Notification.Domain.Entity;
using Mozaeek.Notification.Domain.Models;
using System;

namespace Mozaeek.Notification.Persistence
{
    public class NotificationDbContext:DbContext
    {
        public NotificationDbContext()
        { }

        public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmsMessage>().HasKey(s => s.Id);
            modelBuilder.Entity<PushNotification>().HasKey(s => s.Id);
            modelBuilder.Entity<Application>().HasKey(s => s.Id);
        }
        public DbSet<SmsMessage> SmsMessages { get; set; }
        public DbSet<Application> Application{ get; set; }
        public DbSet<PushNotification> PushNotification { get; set; }
        public DbSet<SmsProvider> SmsProviders { get; set; }
    }
}
