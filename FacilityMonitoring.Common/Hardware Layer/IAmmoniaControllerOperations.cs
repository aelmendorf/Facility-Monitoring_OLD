using FacilityMonitoring.Common.Model;
using FacilityMonitoring.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityMonitoring.Common.Hardware {
    public interface IAmmoniaControllerOperations {
        AmmoniaController Device { get; set; }
        AmmoniaControllerReading ReadAll();
        AmmoniaCalibrationData ReadTankCalibration(int tank);
        bool SetCalibration(AmmoniaCalibrationData data,int tank);
        bool SetCalibrationMode(bool on_off);
    }
}
