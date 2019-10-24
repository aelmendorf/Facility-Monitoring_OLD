using FacilityMonitoring.Common.Data.Context;
using FacilityMonitoring.Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.Data {

    public interface IAddDeviceReading {
        Task<bool> AddReadingAsync(ModbusDevice device);
        bool AddReading(ModbusDevice device);
    }

    public interface IAddGeneratorReading {
        Task<bool> AddReadingAsync(H2Generator generator);
        bool AddReading(H2Generator generator);
    }

    public interface IAddMonitorBoxReading {
        Task<bool> AddReadingAsync(MonitorBox monitorBox);
        bool AddReading(MonitorBox monitorBox);
    }

    public interface IAddTankScaleReading {
        Task<bool> AddReadingAsync(TankScale controller);
        bool AddReading(TankScale controller);
    }

    public class AddDeviceReading : IAddDeviceReading {

        private IAddGeneratorReading _addGenReading;
        private IAddMonitorBoxReading _addMonitorBoxReading; 
        private IAddTankScaleReading _addNHReading;

        public AddDeviceReading(IAddGeneratorReading addGenReading, IAddMonitorBoxReading addMonitorBoxReading, IAddTankScaleReading addNHReading) {
            this._addGenReading = addGenReading;
            this._addMonitorBoxReading = addMonitorBoxReading;
            this._addNHReading = addNHReading;
        }

        public async Task<bool> AddReadingAsync(ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(MonitorBox)) {
                return await this._addMonitorBoxReading.AddReadingAsync((MonitorBox)device);
            } else if (type == typeof(H2Generator)) {
                return await this._addGenReading.AddReadingAsync((H2Generator)device);
            } else if (type == typeof(TankScale)) {
                return await this._addNHReading.AddReadingAsync((TankScale)device);
            } else {
                return false;
            }
        }

        public bool AddReading(ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(MonitorBox)) {
                return this._addMonitorBoxReading.AddReading((MonitorBox)device);
            } else if (type == typeof(H2Generator)) {
                return this._addGenReading.AddReading((H2Generator)device);
            } else if (type == typeof(TankScale)) {
                return this._addNHReading.AddReading((TankScale)device);
            } else {
                return false;
            }
        }
    }

    public class AddTankScaleReading : IAddTankScaleReading {

        private readonly ILogger<IAddTankScaleReading> _logger;
        private readonly FacilityContext _context;
        public AddTankScaleReading(FacilityContext context, ILogger<IAddTankScaleReading> logger) {
            this._context = context;
            this._logger = logger;
        }


        public bool AddReading(TankScale controller) {
            //using var this._context = new FacilityContext();
            var device = this._context.ModbusDevices.Find(controller.Id);
            if (device != null) {
                controller.LastRead.AmmoniaControllerId = controller.Id;
                this._context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                this._context.TankScaleReadings.Add(controller.LastRead);
            } else {
                return false;
            }
            try {
                this._context.SaveChanges();

                return true;
            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", controller.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }
                return false;
            }

        }

        public async Task<bool> AddReadingAsync(TankScale controller) {
            try {
                //using var this._context = new FacilityContext();
                var device = await this._context.ModbusDevices.FindAsync(controller.Id);
                if (device != null) {
                    controller.LastRead.AmmoniaControllerId = controller.Id;

                    this._context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    this._context.TankScaleReadings.Add(controller.LastRead);
                    this._context.TankScaleAlerts.Add(controller.LastRead.AmmoniaControllerAlert);
                    await this._context.SaveChangesAsync();
                    return true;
                } else {
                    return false;
                }
            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", controller.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }
                return false;
            }
        }
    }

    //public class AddMonitorBoxReading : IAddMonitorBoxReading {
    //    private readonly ILogger<IAddMonitorBoxReading> _logger;
    //    private readonly FacilityContext _context;
    //    public AddMonitorBoxReading(FacilityContext context, ILogger<IAddMonitorBoxReading> logger) {
    //        this._context = context;
    //        this._logger = logger;
    //    }

    //    public bool AddReading(MonitorBox monitorBox) {
    //        try {
    //            //using var this._context = new FacilityContext();
    //            var device = this._context.ModbusDevices.Find(monitorBox.Id);
    //            if (device != null) {
    //                monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
    //                this._context.MonitorBoxAlerts.Add(monitorBox.LastRead.MonitorBoxAlert);
    //                this._context.MonitorBoxReadings.Add(monitorBox.LastRead);
    //                this._context.Entry<ModbusDevice>(device).State = EntityState.Modified;
    //                this._context.SaveChanges();
    //                this._logger.LogInformation("{0} Save Succeeded, In-Memory Read: {1}", monitorBox.Identifier, monitorBox.BoxReadings.Count);
    //                return true;
    //            } else {
    //                this._logger.LogError("{0} Device Not Found", monitorBox.Identifier);
    //                return false;
    //            }

    //        } catch (Exception e) {
    //            StringBuilder builder = new StringBuilder();
    //            builder.AppendFormat("{0} Save Failed", monitorBox.Identifier)
    //                .AppendFormat("Exception: {0}", e.Message).AppendLine();
    //            if (e.InnerException != null) {
    //                builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
    //            }

    //            this._logger.LogError(builder.ToString());
    //            return false;
    //        }
    //    }

    //    public async Task<bool> AddReadingAsync(MonitorBox monitorBox) {
    //        try {
    //            //using var this._context = new FacilityContext();
    //            var device = await this._context.ModbusDevices.FindAsync(monitorBox.Id);
    //            if (device != null) {
    //                monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
    //                this._context.MonitorBoxAlerts.Add(monitorBox.LastRead.MonitorBoxAlert);
    //                this._context.MonitorBoxReadings.Add(monitorBox.LastRead);
    //                this._context.Entry<ModbusDevice>(device).State = EntityState.Modified;
    //                await this._context.SaveChangesAsync();
    //                this._logger.LogInformation("{0} Save Succeeded, In-Memory Read: {1}", monitorBox.Identifier, monitorBox.BoxReadings.Count);
    //                return true;
    //            } else {
    //                this._logger.LogError("{0} Device Not Found", monitorBox.Identifier);
    //                return false;
    //            }

    //        } catch (Exception e) {
    //            StringBuilder builder = new StringBuilder();
    //            builder.AppendFormat("{0} Save Failed", monitorBox.Identifier)
    //                .AppendFormat("Exception: {0}", e.Message).AppendLine();
    //            if (e.InnerException != null) {
    //                builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
    //            }

    //            this._logger.LogError(builder.ToString());
    //            return false;
    //        }
    //    }
    //}

    public class AddMonitorBoxReading : IAddMonitorBoxReading {

        private readonly FacilityContext _context;
        private readonly ILogger<IAddMonitorBoxReading> _logger;
        
        public AddMonitorBoxReading(FacilityContext context,ILogger<IAddMonitorBoxReading> logger) {
            this._context = context;
            this._logger = logger;
        }

        public bool AddReading(MonitorBox monitorBox) {
            try {
                //using var this._context = new FacilityContext();
                var device = this._context.ModbusDevices.Find(monitorBox.Id);
                if (device != null) {
                    monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
                    this._context.MonitorBoxAlerts.Add(monitorBox.LastRead.MonitorBoxAlert);
                    this._context.MonitorBoxReadings.Add(monitorBox.LastRead);
                    this._context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    this._context.SaveChanges();

                    return true;
                } else {

                    return false;
                }

            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", monitorBox.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }
                return false;
            }
        }

        public async Task<bool> AddReadingAsync(MonitorBox monitorBox) {
            try {
                //using var this._context = new FacilityContext();
                var device = await this._context.ModbusDevices.FindAsync(monitorBox.Id);
                if (device != null) {
                    monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
                    this._context.MonitorBoxAlerts.Add(monitorBox.LastRead.MonitorBoxAlert);
                    this._context.MonitorBoxReadings.Add(monitorBox.LastRead);
                    this._context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    await this._context.SaveChangesAsync();

                    return true;
                } else {

                    return false;
                }

            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", monitorBox.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }
                return false;
            }
        }
    }

    public class AddGeneratorReading : IAddGeneratorReading {

        private readonly FacilityContext _context;
        private readonly ILogger<AddGeneratorReading> _logger;

        public AddGeneratorReading(FacilityContext context, ILogger<AddGeneratorReading> logger) {
            //this._context = new FacilityContext();
            this._context = context;
            this._logger = logger;
        }

        public async Task<bool> AddReadingAsync(H2Generator generator) {
            try {
                //this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                //this._context.ChangeTracker.LazyLoadingEnabled = false;
                //this._context.ChangeTracker.AutoDetectChangesEnabled = false;
                var device = await this._context.ModbusDevices.FindAsync(generator.Id) as H2Generator;
                if (device != null) {
                    generator.LastRead.GeneratorId = generator.Id;
                    this._context.H2GenReadings.Add(generator.LastRead);
                    this._context.GeneratorSystemErrors.Add(generator.LastRead.AllSystemErrors);
                    this._context.GeneratorSystemWarnings.Add(generator.LastRead.AllSystemWarnings);
                    device.SystemState = generator.LastRead.SystemState;
                    device.OperationMode = generator.LastRead.OperationMode;
                    generator.SystemState = generator.LastRead.SystemState;
                    generator.OperationMode = generator.LastRead.OperationMode;
                    this._context.Entry(device).State = EntityState.Modified;
                    await this._context.SaveChangesAsync();
                    this._context.Entry(generator).State = EntityState.Detached;
                    //GC.Collect();
                    return true;
                } else {
                    return false;
                }
            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", generator.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }
                return false;
            }
        }

        public bool AddReading(H2Generator generator) {
            try {
                //using var this._context = new FacilityContext();
                var device = this._context.ModbusDevices.Find(generator.Id);
                if (device != null) {
                    generator.LastRead.GeneratorId = generator.Id;
                    this._context.GeneratorSystemErrors.Add(generator.LastRead.AllSystemErrors);
                    this._context.GeneratorSystemWarnings.Add(generator.LastRead.AllSystemWarnings);
                    generator.SystemState = generator.LastRead.SystemState;
                    generator.OperationMode = generator.LastRead.OperationMode;
                    this._context.Entry(device).State = EntityState.Modified;
                    this._context.H2GenReadings.Add(generator.LastRead);
                    this._context.SaveChanges();
                    //GC.Collect();
                    return true;
                } else {
                    return false;
                }
            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", generator.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }
                return false;
            }
        }
    }




}
