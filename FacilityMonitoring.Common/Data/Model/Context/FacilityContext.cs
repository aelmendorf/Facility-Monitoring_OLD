using FacilityMonitoring.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FacilityMonitoring.Common.Data.Context {
    public class FacilityContext:DbContext {
        public DbSet<ModbusDevice> ModbusDevices { get; set; }
        public DbSet<MonitorBoxReading> MonitorBoxReadings { get; set; }
        public DbSet<MonitorBoxAlert> MonitorBoxAlerts { get; set; }
        public DbSet<H2GenReading> H2GenReadings { get; set; }
        public DbSet<TankScaleReading> TankScaleReadings { get; set; }
        public DbSet<TankScalAlertReading> TankScaleAlerts { get; set; }
        public DbSet<GeneratorSystemError> GeneratorSystemErrors { get; set; }
        public DbSet<GeneratorSystemWarning> GeneratorSystemWarnings { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AlertSetting> AlertSettings { get; set; }


        public FacilityContext(DbContextOptions<FacilityContext> options):base(options) {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public FacilityContext() :base() {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseLazyLoadingProxies(false);
            optionsBuilder.UseSqlServer("server=172.20.4.20;database=facilitymonitoring;User Id=aelmendorf;Password=Drizzle123!;");
            //optionsBuilder.UseSqlServer("server=172.20.4.20;database=monitoring_dev;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer(Microsoft.Extensions.Configuration.GetConnectionString("FacilityConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<MonitorBox>().HasBaseType<ModbusDevice>();
            builder.Entity<H2Generator>().HasBaseType<ModbusDevice>();
            builder.Entity<TankScale>().HasBaseType<ModbusDevice>();

            builder.Entity<AnalogChannel>().HasBaseType<Register>();
            builder.Entity<DigitalInputChannel>().HasBaseType<Register>();
            builder.Entity<DigitalOutputChannel>().HasBaseType<Register>();
            builder.Entity<GeneratorRegister>().HasBaseType<Register>();

            builder.Entity<SensorType>().HasBaseType<Category>();

            builder.Entity<MonitorBox>()
                .HasMany(e => e.BoxReadings)
                .WithOne(e => e.GenericMonitorBox)
                .HasForeignKey(e => e.GenericMonitorBoxId)
                .IsRequired(true);

            builder.Entity<MonitorBoxReading>()
                .HasOne(e => e.MonitorBoxAlert)
                .WithOne(e => e.MonitorBoxReading)
                .HasForeignKey<MonitorBoxAlert>(e => e.MonitorBoxReadingId);

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

            builder.Entity<TankScale>()
                .HasMany(e => e.Readings)
                .WithOne(e => e.AmmoniaController)
                .HasForeignKey(e => e.AmmoniaControllerId)
                .IsRequired(true);

            builder.Entity<TankScaleReading>()
                .HasOne(e => e.AmmoniaController)
                .WithMany(e => e.Readings)
                .HasForeignKey(e => e.AmmoniaControllerId)
                .IsRequired(true);

            builder.Entity<TankScaleReading>()
                .HasOne(e => e.AmmoniaControllerAlert)
                .WithOne(e => e.AmmoniaControllerReading)
                .HasForeignKey<TankScalAlertReading>(e => e.AmmoniaControllerReadingId);

            builder.Entity<Register>()
                .HasOne(e => e.Device)
                .WithMany(e => e.Registers)
                .HasForeignKey(e => e.DeviceId)
                .IsRequired(true);

            builder.Entity<SensorType>()
                .HasMany(e => e.Registers)
                .WithOne(e => e.SensorType)
                .HasForeignKey(e=>e.SensorTypeId)
                .IsRequired(false);
           
        }

        private static Func<FacilityContext, string, QueryTrackingBehavior, Task<H2Generator>> _getGeneratorAsync =
        EF.CompileAsyncQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
            context.ModbusDevices
            .AsNoTracking()
            .OfType<H2Generator>()
            .Include(e => e.Registers)
            .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, Task<List<H2Generator>>> _getGeneratorsAsync =
        EF.CompileAsyncQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
            context.ModbusDevices
            .AsNoTracking()
            .OfType<H2Generator>()
            .Include(e => e.Registers)
            .ToList());

        private static Func<FacilityContext, List<AlertSetting>> _getAlertSettins =
            EF.CompileQuery((FacilityContext context) =>context.AlertSettings.AsNoTracking().ToList());

        private static Func<FacilityContext, Task<List<AlertSetting>>> _getAlertSettinsAsync =
            EF.CompileAsyncQuery((FacilityContext context) => context.AlertSettings.AsNoTracking().ToList());

        private static Func<FacilityContext, string, QueryTrackingBehavior, H2Generator> _getGenerator =
                EF.CompileQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<H2Generator>()
                    .Include(e => e.Registers)
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, Task<TankScale>> _getNHControllerAsync =
                EF.CompileAsyncQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<TankScale>()
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, TankScale> _getNHController =
                EF.CompileQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<TankScale>()
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, Task<MonitorBox>> _getMonitorBoxAsync =
                EF.CompileAsyncQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<MonitorBox>()
                    .Include(e => e.Registers)
                        .ThenInclude(e => e.SensorType)
                    .SingleOrDefault(e => e.Identifier == identifier));

        private static Func<FacilityContext, string, QueryTrackingBehavior, MonitorBox> _getMonitorBox =
                EF.CompileQuery((FacilityContext context, string identifier, QueryTrackingBehavior tracking) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<MonitorBox>()
                        .Include(e=>e.Registers)
                            .ThenInclude(x=>x.SensorType)
                    .SingleOrDefault(e => e.Identifier == identifier));


        private static Func<FacilityContext, Task<List<H2Generator>>> _getAllGeneratorsAsync =
                EF.CompileAsyncQuery((FacilityContext context) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<H2Generator>()
                    .Include(e => e.Registers).ToList());

        private static Func<FacilityContext, List<H2Generator>> _getAllGenerators =
                EF.CompileQuery((FacilityContext context) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<H2Generator>()
                    .Include(e => e.Registers).ToList());


        private static Func<FacilityContext, Task<List<TankScale>>> _getAllNHControllersAsync =
        EF.CompileAsyncQuery((FacilityContext context) =>
            context.ModbusDevices
            .AsNoTracking()
            .OfType<TankScale>().ToList());

        private static Func<FacilityContext, List<TankScale>> _getAllNHControllers =
                EF.CompileQuery((FacilityContext context) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<TankScale>().ToList());

        private static Func<FacilityContext, Task<List<MonitorBox>>> _getAllMonitorBoxsAsync =
                EF.CompileAsyncQuery((FacilityContext context) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<MonitorBox>()
                    .Include(e => e.Registers)
                        .ThenInclude(e => e.SensorType).ToList());

        private static Func<FacilityContext, List<MonitorBox>> _getAllMonitorBoxs =
                EF.CompileQuery((FacilityContext context) =>
                    context.ModbusDevices
                    .AsNoTracking()
                    .OfType<MonitorBox>().ToList());

        private static Func<FacilityContext, Task<List<ModbusDevice>>> _getAllDevicesAsync =
            EF.CompileAsyncQuery((FacilityContext context) =>
                context.ModbusDevices.ToList());

        private static Func<FacilityContext, List<ModbusDevice>> _getAllDevices =
            EF.CompileQuery((FacilityContext context) =>
                context.ModbusDevices.ToList());

        public async Task<H2Generator> GetGeneratorAsync(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await FacilityContext._getGeneratorAsync(this, identifier, option);
        }

        public H2Generator GetGenerator(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return FacilityContext._getGenerator(this, identifier, option);
        }

        public async Task<TankScale> GetNHControllerAsync(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await FacilityContext._getNHControllerAsync(this, identifier, option);
        }

        public TankScale GetNHController(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return FacilityContext._getNHController(this, identifier, option);
        }

        public async Task<MonitorBox> GetMonitorBoxAsync(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return await FacilityContext._getMonitorBoxAsync(this, identifier, option);
        }

        public MonitorBox GetMonitorBox(string identifier, bool tracking) {
            var option = (tracking) ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            return FacilityContext._getMonitorBox(this, identifier, option);
        }


        public async Task<List<H2Generator>> GetAllGeneratorsAsync() {
            return await FacilityContext._getAllGeneratorsAsync(this);
        }

        public List<H2Generator> GetAllGenerators() {
            return FacilityContext._getAllGenerators(this);
        }

        public async Task<List<MonitorBox>> GetAllMonitorBoxsAsync() {
            return await FacilityContext._getAllMonitorBoxsAsync(this);
        }

        public List<MonitorBox> GetAllMonitorBoxs() {
            return FacilityContext._getAllMonitorBoxs(this);
        }

        public async Task<List<TankScale>> GetAllNHControllersAsync() {
            return await FacilityContext._getAllNHControllersAsync(this);
        }

        public List<TankScale> GetAllNHControllers() {
            return FacilityContext._getAllNHControllers(this);
        }

        public async Task<List<ModbusDevice>> GetAllDevicesAsync() {
            return await FacilityContext._getAllDevicesAsync(this);
        }

        public List<ModbusDevice> GetAllDevices() {
            return FacilityContext._getAllDevices(this);
        }

        public List<AlertSetting> GetAlertSettings() {
            return FacilityContext._getAlertSettins(this);
        }

        public async Task<List<AlertSetting>> GetAlertSettingsAsync() {
            return await FacilityContext._getAlertSettinsAsync(this);
        }
    }
}
