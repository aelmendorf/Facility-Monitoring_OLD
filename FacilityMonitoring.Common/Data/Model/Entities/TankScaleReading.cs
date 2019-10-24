using FacilityMonitoring.Common.Data.DTO;
using System;
using System.Collections.Generic;

namespace FacilityMonitoring.Common.Data.Entities {

    public partial class TankScaleReading {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public int AmmoniaControllerId { get; set; }
        public TankScale AmmoniaController { get; set; }

        public TankScaleAlert AmmoniaControllerAlert { get; set; }

        public TankScaleReading() { }

        public TankScaleReading(DateTime timestamp, TankScale device) {
            this.TimeStamp = timestamp;
            this.AmmoniaControllerId = device.Id;
        }

        public int Tank1Weight { get; set; }
        public int Tank2Weight { get; set; }
        public int Tank3Weight { get; set; }
        public int Tank4Weight { get; set; }

        public int Tank1ZeroCal { get; set; }
        public int Tank1NonZeroCal { get; set; }
        public int Tank1Zero { get; set; }
        public int Tank1NonZero { get; set; }
        public int Tank1Total { get; set; }
        public int Tank1Gas { get; set; }

        public int Tank2ZeroCal { get; set; }
        public int Tank2NonZeroCal { get; set; }
        public int Tank2Zero { get; set; }
        public int Tank2NonZero { get; set; }
        public int Tank2Total { get; set; }
        public int Tank2Gas { get; set; }

        public int Tank3ZeroCal { get; set; }
        public int Tank3NonZeroCal { get; set; }
        public int Tank3Zero { get; set; }
        public int Tank3NonZero { get; set; }
        public int Tank3Total { get; set; }
        public int Tank3Gas { get; set; }

        public int Tank4ZeroCal { get; set; }
        public int Tank4NonZeroCal { get; set; }
        public int Tank4Zero { get; set; }
        public int Tank4NonZero { get; set; }
        public int Tank4Total { get; set; }
        public int Tank4Gas { get; set; }

        public int Tank1Tare { get; set; }
        public int Tank2Tare { get; set; }
        public int Tank3Tare { get; set; }
        public int Tank4Tare { get; set; }

        public double Tank1Temperature { get; set; }
        public double Tank2Temperature { get; set; }
        public double Tank3Temperature { get; set; }
        public double Tank4Temperature { get; set; }

        public int Heater1DutyCycle { get; set; }
        public int Heater2DutyCycle { get; set; }
        public int Heater3DutyCycle { get; set; }
        public int Heater4DutyCycle { get; set; }

        public bool Tank1Warning { get; set; }
        public bool Tank2Warning { get; set; }
        public bool Tank3Warning { get; set; }
        public bool Tank4Warning { get; set; }

        public bool Tank1Alarm { get; set; }
        public bool Tank2Alarm { get; set; }
        public bool Tank3Alarm { get; set; }
        public bool Tank4Alarm { get; set; }

        public void Set(List<int> regData, bool[] coilData) {
            for (int i = 0; i < 4; i++) {
                switch (i) {
                    case 0:
                        this.Tank1Weight = regData[i];
                        var tankData = regData.GetRange(4, 6).ToArray();
                        this.Tank1ZeroCal = tankData[0];
                        this.Tank1NonZeroCal = tankData[1];
                        this.Tank1Zero = tankData[2];
                        this.Tank1NonZero = tankData[3];
                        this.Tank1Total = tankData[4];
                        this.Tank1Gas = tankData[5];
                        this.Tank1Tare = regData[28];
                        this.Tank1Temperature = regData[32]/100;
                        this.Heater1DutyCycle = regData[38];
                        this.Tank1Warning = coilData[2];
                        this.Tank1Alarm = coilData[6];
                        break;

                    case 1:
                        this.Tank2Weight = regData[i];
                        var tank2Data = regData.GetRange(10, 6).ToArray();
                        this.Tank2ZeroCal = tank2Data[0];
                        this.Tank2NonZeroCal = tank2Data[1];
                        this.Tank2Zero = tank2Data[2];
                        this.Tank2NonZero = tank2Data[3];
                        this.Tank2Total = tank2Data[4];
                        this.Tank2Gas = tank2Data[5];
                        this.Tank2Tare = regData[29];
                        this.Tank2Temperature = regData[33] / 100;
                        this.Heater2DutyCycle = regData[39];
                        this.Tank2Warning = coilData[3];
                        this.Tank2Alarm = coilData[7];
                        break;

                    case 2:
                        this.Tank3Weight = regData[i];
                        var tank3Data = regData.GetRange(16, 6).ToArray();
                        this.Tank3ZeroCal = tank3Data[0];
                        this.Tank3NonZeroCal = tank3Data[1];
                        this.Tank3Zero = tank3Data[2];
                        this.Tank3NonZero = tank3Data[3];
                        this.Tank3Total = tank3Data[4];
                        this.Tank3Gas = tank3Data[5];
                        this.Tank3Tare = regData[30];
                        this.Tank3Temperature = regData[34] / 100;
                        this.Heater3DutyCycle = regData[40];
                        this.Tank3Warning = coilData[4];
                        this.Tank3Alarm = coilData[8];
                        break;

                    case 3:
                        this.Tank4Weight = regData[i];
                        var tank4Data = regData.GetRange(22, 6).ToArray();
                        this.Tank4ZeroCal = tank4Data[0];
                        this.Tank4NonZeroCal = tank4Data[1];
                        this.Tank4Zero = tank4Data[2];
                        this.Tank4NonZero = tank4Data[3];
                        this.Tank4Total = tank4Data[4];
                        this.Tank4Gas = tank4Data[5];
                        this.Tank4Tare = regData[31];
                        this.Tank4Temperature = regData[35] / 100;
                        this.Heater4DutyCycle = regData[41];
                        this.Tank4Warning = coilData[5];
                        this.Tank4Alarm = coilData[9];
                        break;
                }
            }
        }

        public AmmoniaCalibrationData GetTankCalibration(int tank) {
            switch (tank) {
                case 1: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank1ZeroCal;
                    cal.CalNonZero = this.Tank1NonZeroCal;
                    cal.ActualZero = this.Tank1Zero;
                    cal.ActualNonZero = this.Tank1NonZero;
                    cal.GasWeight = this.Tank1Gas;
                    cal.TotalWeight = this.Tank1Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                case 2: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank2ZeroCal;
                    cal.CalNonZero = this.Tank2NonZeroCal;
                    cal.ActualZero = this.Tank2Zero;
                    cal.ActualNonZero = this.Tank2NonZero;
                    cal.GasWeight = this.Tank2Gas;
                    cal.TotalWeight = this.Tank2Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                case 3: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank3ZeroCal;
                    cal.CalNonZero = this.Tank3NonZeroCal;
                    cal.ActualZero = this.Tank3Zero;
                    cal.ActualNonZero = this.Tank3NonZero;
                    cal.GasWeight = this.Tank3Gas;
                    cal.TotalWeight = this.Tank3Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                case 4: {
                    AmmoniaCalibrationData cal = new AmmoniaCalibrationData();
                    cal.CalZero = this.Tank4ZeroCal;
                    cal.CalNonZero = this.Tank4NonZeroCal;
                    cal.ActualZero = this.Tank4Zero;
                    cal.ActualNonZero = this.Tank4NonZero;
                    cal.GasWeight = this.Tank4Gas;
                    cal.TotalWeight = this.Tank4Total;
                    cal.Tank = (ushort)tank;
                    return cal;
                }
                default: return null;
            }
        }

        public IEnumerable<Tank> GetDataTransfer() {

            Tank tank1 = new Tank();
            tank1.Identifier = "Tank 1";
            tank1.Weight = this.Tank1Weight;
            tank1.Temperature = this.Tank1Temperature;
            tank1.DutyCycle = this.Heater1DutyCycle;

            Tank tank2 = new Tank();
            tank2.Identifier = "Tank 2";
            tank2.Weight = this.Tank2Weight;
            tank2.Temperature = this.Tank2Temperature;
            tank2.DutyCycle = this.Heater2DutyCycle;

            Tank tank3 = new Tank();
            tank3.Identifier = "Tank 3";
            tank3.Weight = this.Tank3Weight;
            tank3.Temperature = this.Tank3Temperature;
            tank3.DutyCycle = this.Heater3DutyCycle;

            Tank tank4 = new Tank();
            tank4.Identifier = "Tank 4";
            tank4.Weight = this.Tank4Weight;
            tank4.Temperature = this.Tank4Temperature;
            tank4.DutyCycle = this.Heater4DutyCycle;
            return new List<Tank>() { tank1,tank2,tank3,tank4};
        }
    }
}
