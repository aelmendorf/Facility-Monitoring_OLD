using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Model {
    public class FacilityContext:DbContext {

        private static Func<FacilityContext, string, QueryTrackingBehavior, Task<H2Generator>> _getGeneratorAsync =
                EF.CompileAsyncQuery((FacilityContext context, string identifier,QueryTrackingBehavior tracking ) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<H2Generator>()
                    .Include(e => e.Registers)
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, H2Generator> _getGenerator =
                EF.CompileQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<H2Generator>()
                    .Include(e => e.Registers)
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, Task<AmmoniaController>> _getNHControllerAsync =
                EF.CompileAsyncQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<AmmoniaController>()
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, AmmoniaController> _getNHController =
                EF.CompileQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<AmmoniaController>()
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, Task<GenericMonitorBox>> _getMonitorBoxAsync =
                EF.CompileAsyncQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<GenericMonitorBox>()
                    .Include(e=>e.Registers)
                        .ThenInclude(e=>e.SensorType)
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, GenericMonitorBox> _getMonitorBox =
                EF.CompileQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<GenericMonitorBox>()
                    .SingleOrDefault(e => e.Identifier == identifier));


        public DbSet<ModbusDevice> ModbusDevices { get; set; }
        public DbSet<GenericBoxReading> GenericBoxReadings { get; set; }
        public DbSet<GenericMonitorBoxAlert> GenericBoxAlerts { get; set; }
        public DbSet<H2GenReading> H2GenReadings { get; set; }
        public DbSet<AmmoniaControllerReading> AmmoniaControllerReadings { get; set; }
        public DbSet<AmmoniaControllerAlert> AmmoniaControllerAlerts { get; set; }
        public DbSet<GeneratorSystemError> GeneratorSystemErrors { get; set; }
        public DbSet<GeneratorSystemWarning> GeneratorSystemWarnings { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Category> Categories { get; set; }



        public FacilityContext(DbContextOptions<FacilityContext> options):base(options) {
        
        }

        public FacilityContext():base() {

        }


        public async Task<H2Generator> GetGeneratorAsync(string identifier,bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await FacilityContext._getGeneratorAsync(this, identifier,option);
        }

        public H2Generator GetGenerator(string identifier,bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return FacilityContext._getGenerator(this, identifier, option);
        }

        public async Task<AmmoniaController> GetNHControllerAsync(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await FacilityContext._getNHControllerAsync(this, identifier, option);
        }

        public AmmoniaController GetNHController(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return FacilityContext._getNHController(this, identifier, option);
        }

        public async Task<GenericMonitorBox> GetMonitorBoxAsync(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await FacilityContext._getMonitorBoxAsync(this, identifier, option);
        }

        public GenericMonitorBox GetMonitorBox(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return FacilityContext._getMonitorBox(this, identifier, option);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("server=172.20.4.20;database=facilitymonitoring;User Id=aelmendorf;Password=Drizzle123!;");
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

            builder.Entity<GenericBoxReading>()
                .HasOne(e => e.GenericMonitorBoxAlert)
                .WithOne(e => e.GenericMonitorBoxReading)
                .HasForeignKey<GenericMonitorBoxAlert>(e => e.GenericBoxReadingId);

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

            builder.Entity<AmmoniaControllerReading>()
                .HasOne(e => e.AmmoniaControllerAlert)
                .WithOne(e => e.AmmoniaControllerReading)
                .HasForeignKey<AmmoniaControllerAlert>(e => e.AmmoniaControllerReadingId);

            builder.Entity<Register>()
                .HasOne(e => e.Device)
                .WithMany(e => e.Registers)
                .HasForeignKey(e => e.DeviceId)
                .IsRequired(true);

            //builder.Entity<Register>()
            //    .HasOne(e => e.SensorType)
            //    .WithMany(e => e.Registers)
            //    .HasForeignKey(e => e.SensorTypeId)
            //    .IsRequired(false);

            builder.Entity<SensorType>()
                .HasMany(e => e.Registers)
                .WithOne(e => e.SensorType)
                .HasForeignKey(e=>e.SensorTypeId)
                .IsRequired(false);
        }
    }
}
