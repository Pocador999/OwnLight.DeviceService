using DeviceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceAction> DeviceActions { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Device>()
            .HasOne(d => d.DeviceType)
            .WithMany(dt => dt.Devices)
            .HasForeignKey(d => d.DeviceTypeId);

        modelBuilder
            .Entity<DeviceAction>()
            .HasOne<Device>()
            .WithMany(d => d.DeviceActions)
            .HasForeignKey(da => da.DeviceId);

        base.OnModelCreating(modelBuilder);
    }
}
