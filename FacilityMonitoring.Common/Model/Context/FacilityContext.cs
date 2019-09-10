using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Model {
    public class FacilityContext:DbContext {

        public DbSet<ModbusDevice> ModbusDevices { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<Register> Channels { get; set; }
        public DbSet<Category> Categories { get; set; }


        public FacilityContext(DbContextOptions<FacilityContext> options):base(options) {

        }

        public FacilityContext():base() {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("server=172.20.4.20;database=MonitorTesting;User Id=aelmendorf;Password=Drizzle123!;");
        }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<GenericMonitorBox>().HasBaseType<ModbusDevice>();

            builder.Entity<AmmoniaBoxReading>().HasBaseType<Reading>();
            builder.Entity<H2GenReading>().HasBaseType<Reading>();
            builder.Entity<GenericBoxReading>().HasBaseType<Reading>();

            builder.Entity<AnalogChannel>().HasBaseType<Register>();
            builder.Entity<DigitalInputChannel>().HasBaseType<Register>();
            builder.Entity<DigitalOutputChannel>().HasBaseType<Register>();

            builder.Entity<SensorType>().HasBaseType<Category>();


            builder.Entity<ModbusDevice>()
                .HasMany(e => e.Readings)
                .WithOne(e => e.ModbusDevice)
                .HasForeignKey(e => e.ModbusDeviceId);

            builder.Entity<Register>()
                .HasOne(e => e.GenericMonitorBox)
                .WithMany(e => e.Channels)
                .HasForeignKey(e => e.GenericMonitorBoxId)
                .IsRequired(true);

            builder.Entity<SensorType>()
                .HasMany(e => e.AnalogChannels)
                .WithOne(e => e.SensorType)
                .HasForeignKey(e=>e.SensorTypeId)
                .IsRequired(false);
        }
    }
}
