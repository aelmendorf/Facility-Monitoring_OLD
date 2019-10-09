using FacilityMonitoring.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FacilityMonitoring.Common.DataLayer {

    public interface IAddDeviceReading {
        Task<bool> AddReadingAsync(ModbusDevice device);
        bool AddReading(ModbusDevice device);
    }

    public interface IAddGeneratorReading {
        Task<bool> AddReadingAsync(H2Generator generator);
        bool AddReading(H2Generator generator);
    }

    public interface IAddMonitorBoxReading {
        Task<bool> AddReadingAsync(GenericMonitorBox monitorBox);
        bool AddReading(GenericMonitorBox monitorBox);
    }

    public interface IAddNHControllerReading {
        Task<bool> AddReadingAsync(AmmoniaController controller);
        bool AddReading(AmmoniaController controller);
    }

    public class AddDeviceReading : IAddDeviceReading {

        private IAddGeneratorReading _addGenReading;
        private IAddMonitorBoxReading _addMonitorBoxReading; 
        private IAddNHControllerReading _addNHReading;

        public AddDeviceReading(IAddGeneratorReading addGenReading, IAddMonitorBoxReading addMonitorBoxReading, IAddNHControllerReading addNHReading) {
            this._addGenReading = addGenReading;
            this._addMonitorBoxReading = addMonitorBoxReading;
            this._addNHReading = addNHReading;
        }

        public async Task<bool> AddReadingAsync(ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(GenericMonitorBox)) {
                return await this._addMonitorBoxReading.AddReadingAsync((GenericMonitorBox)device);
            } else if (type == typeof(H2Generator)) {
                return await this._addGenReading.AddReadingAsync((H2Generator)device);
            } else if (type == typeof(AmmoniaController)) {
                return await this._addNHReading.AddReadingAsync((AmmoniaController)device);
            } else {
                return false;
            }
        }

        public bool AddReading(ModbusDevice device) {
            Type type = device.GetType();
            if (type == typeof(GenericMonitorBox)) {
                return this._addMonitorBoxReading.AddReading((GenericMonitorBox)device);
            } else if (type == typeof(H2Generator)) {
                return this._addGenReading.AddReading((H2Generator)device);
            } else if (type == typeof(AmmoniaController)) {
                return this._addNHReading.AddReading((AmmoniaController)device);
            } else {
                return false;
            }
        }
    }

    public class AddNHControllerReading : IAddNHControllerReading {
        public AddNHControllerReading() {

        }

        public bool AddReading(AmmoniaController controller) {
            using var context = new FacilityContext();
            var device = context.ModbusDevices.Find(controller.Id);
            if (device != null) {
                controller.LastRead.AmmoniaControllerId = controller.Id;
                context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                context.AmmoniaControllerReadings.Add(controller.LastRead);
            } else {
                return false;
            }
            try {
                context.SaveChanges();

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

        public async Task<bool> AddReadingAsync(AmmoniaController controller) {
            try {
                using var context = new FacilityContext();
                var device = await context.ModbusDevices.FindAsync(controller.Id);
                if (device != null) {
                    controller.LastRead.AmmoniaControllerId = controller.Id;

                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    context.AmmoniaControllerReadings.Add(controller.LastRead);
                    context.AmmoniaControllerAlerts.Add(controller.LastRead.AmmoniaControllerAlert);
                    await context.SaveChangesAsync();
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

    public class AddMonitorBoxReading : IAddMonitorBoxReading {
        private readonly ILogger _logger;

        public AddMonitorBoxReading(ILogger<IAddGeneratorReading> logger) {
            this._logger = logger;
        }

        public bool AddReading(GenericMonitorBox monitorBox) {
            try {
                using var context = new FacilityContext();
                var device = context.ModbusDevices.Find(monitorBox.Id);
                if (device != null) {
                    monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
                    context.GenericBoxAlerts.Add(monitorBox.LastRead.GenericMonitorBoxAlert);
                    context.GenericBoxReadings.Add(monitorBox.LastRead);
                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    context.SaveChanges();
                    this._logger.LogInformation("{0} Save Succeeded, In-Memory Read: {1}", monitorBox.Identifier, monitorBox.BoxReadings.Count);
                    return true;
                } else {
                    this._logger.LogError("{0} Device Not Found", monitorBox.Identifier);
                    return false;
                }

            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", monitorBox.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }

                this._logger.LogError(builder.ToString());
                return false;
            }
        }

        public async Task<bool> AddReadingAsync(GenericMonitorBox monitorBox) {
            try {
                using var context = new FacilityContext();
                var device = await context.ModbusDevices.FindAsync(monitorBox.Id);
                if (device != null) {
                    monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
                    context.GenericBoxAlerts.Add(monitorBox.LastRead.GenericMonitorBoxAlert);
                    context.GenericBoxReadings.Add(monitorBox.LastRead);
                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    this._logger.LogInformation("{0} Save Succeeded, In-Memory Read: {1}", monitorBox.Identifier, monitorBox.BoxReadings.Count);
                    return true;
                } else {
                    this._logger.LogError("{0} Device Not Found", monitorBox.Identifier);
                    return false;
                }

            } catch (Exception e) {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} Save Failed", monitorBox.Identifier)
                    .AppendFormat("Exception: {0}", e.Message).AppendLine();
                if (e.InnerException != null) {
                    builder.AppendFormat("Inner Exception: {0}", e.InnerException.Message).AppendLine();
                }

                this._logger.LogError(builder.ToString());
                return false;
            }
        }
    }

    public class MonitorBoxReadingAdd : IAddMonitorBoxReading {


        public MonitorBoxReadingAdd() {

        }

        public bool AddReading(GenericMonitorBox monitorBox) {
            try {
                using var context = new FacilityContext();
                var device = context.ModbusDevices.Find(monitorBox.Id);
                if (device != null) {
                    monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
                    context.GenericBoxAlerts.Add(monitorBox.LastRead.GenericMonitorBoxAlert);
                    context.GenericBoxReadings.Add(monitorBox.LastRead);
                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    context.SaveChanges();

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

        public async Task<bool> AddReadingAsync(GenericMonitorBox monitorBox) {
            try {
                using var context = new FacilityContext();
                var device = await context.ModbusDevices.FindAsync(monitorBox.Id);
                if (device != null) {
                    monitorBox.LastRead.GenericMonitorBoxId = monitorBox.Id;
                    context.GenericBoxAlerts.Add(monitorBox.LastRead.GenericMonitorBoxAlert);
                    context.GenericBoxReadings.Add(monitorBox.LastRead);
                    context.Entry<ModbusDevice>(device).State = EntityState.Modified;
                    await context.SaveChangesAsync();

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

        public AddGeneratorReading() {
            this._context = new FacilityContext();
        }

        public async Task<bool> AddReadingAsync(H2Generator generator) {
            try {
                //using var this._context = new FacilityContext();
                var device = await this._context.ModbusDevices.FindAsync(generator.Id) as H2Generator;
                if (device != null) {
                    generator.LastRead.GeneratorId = generator.Id;
                    this._context.GeneratorSystemErrors.Add(generator.LastRead.AllSystemErrors);
                    this._context.GeneratorSystemWarnings.Add(generator.LastRead.AllSystemWarnings);
                    //generator.H2Readings.Add(generator.LastRead);
                    device.SystemState = generator.LastRead.SystemState;
                    device.OperationMode = generator.LastRead.OperationMode;
                    generator.SystemState = generator.LastRead.SystemState;
                    generator.OperationMode = generator.LastRead.OperationMode;
                    this._context.Entry(device).State = EntityState.Modified;
                    this._context.H2GenReadings.Add(generator.LastRead);
                    await this._context.SaveChangesAsync();
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
