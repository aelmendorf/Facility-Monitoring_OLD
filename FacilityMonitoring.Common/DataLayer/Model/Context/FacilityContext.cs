using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Model {
    public class FacilityContext:DbContext {

        public DbSet<ModbusDevice> ModbusDevices { get; set; }
        public DbSet<GenericBoxReading> GenericBoxReadings { get; set; }
        public DbSet<H2GenReading> H2GenReadings { get; set; }
        public DbSet<AmmoniaControllerReading> AmmoniaControllerReadings { get; set; }
        public DbSet<GeneratorSystemError> GeneratorSystemErrors { get; set; }
        public DbSet<GeneratorSystemWarning> GeneratorSystemWarnings { get; set; }
        public DbSet<Register> Registers { get; set; }
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
            builder.Entity<H2Generator>().HasBaseType<ModbusDevice>();
            builder.Entity<AmmoniaController>().HasBaseType<ModbusDevice>();

            builder.Entity<AnalogChannel>().HasBaseType<Register>();
            builder.Entity<DigitalInputChannel>().HasBaseType<Register>();
            builder.Entity<DigitalOutputChannel>().HasBaseType<Register>();
            builder.Entity<GeneratorRegister>().HasBaseType<Register>();

            builder.Entity<SensorType>().HasBaseType<Category>();

            builder.Entity<GenericMonitorBox>()
                .HasMany(e => e.BoxReadings)
                .WithOne(e => e.GenericMonitorBox)
                .HasForeignKey(e => e.GenericMonitorBoxId)
                .IsRequired(true);

            builder.Entity<H2Generator>()
                .HasMany(e => e.H2Readings)
                .WithOne(e => e.H2Generator)
                .HasForeignKey(e => e.GeneratorId)
                .IsRequired(true);

            builder.Entity<H2GenReading>()
                .HasOne(e => e.AllSystemWarnings)
                .WithOne(e => e.H2GenReading)
                .HasForeignKey<GeneratorSystemWarning>(e => e.H2GenReadingId);

            builder.Entity<H2GenReading>()
                .HasOne(e => e.AllSystemErrors)
                .WithOne(e => e.H2GenReading)
                .HasForeignKey<GeneratorSystemError>(e => e.H2GenReadingId);

            builder.Entity<AmmoniaController>()
                .HasMany(e => e.Readings)
                .WithOne(e => e.AmmoniaController)
                .HasForeignKey(e => e.AmmoniaControllerId)
                .IsRequired(true);

            builder.Entity<AmmoniaControllerReading>()
                .HasOne(e => e.AmmoniaController)
                .WithMany(e => e.Readings)
                .HasForeignKey(e => e.AmmoniaControllerId)
                .IsRequired(true);

            builder.Entity<Register>()
                .HasOne(e => e.Device)
                .WithMany(e => e.Registers)
                .HasForeignKey(e => e.DeviceId)
                .IsRequired(true);

            builder.Entity<SensorType>()
                .HasMany(e => e.AnalogChannels)
                .WithOne(e => e.SensorType)
                .HasForeignKey(e=>e.SensorTypeId)
                .IsRequired(false);
        }
    }
}
