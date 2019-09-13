using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacilityMonitoring.Common.Data;

namespace FacilityMonitoring.Common.Converters {
    public static class RegisterConverters {

        public static int ToInt32(ushort first,ushort second) {
            return BitConverter.ToInt32(BitConverter.GetBytes(second).Concat(BitConverter.GetBytes(first)).ToArray(),0);
        }

        public static float ToFloat(ushort first,ushort second) {
            return BitConverter.ToSingle(BitConverter.GetBytes(first).Concat(BitConverter.GetBytes(second)).ToArray(), 0);
        }

        public static ushort[] ToUshortArray(int value) {
            ushort[] s = new ushort[2];
            byte[] fBytes = BitConverter.GetBytes(value);
            s[0] = BitConverter.ToUInt16(fBytes, 2);
            s[1] = BitConverter.ToUInt16(fBytes, 0);
            return s;
        }

        public static float[] GetFloatArray(object fltArray) {
            if (fltArray.GetType() == typeof(float[])) {
                object[] t = ((Array)fltArray).Cast<object>().ToArray();

                return Array.ConvertAll(t, item => (float)item);
            }
            return null;
        }

        public static ushort[] GetUshortArray(object array) {
            if (array.GetType() == typeof(ushort[])) {
                object[] t = ((Array)array).Cast<object>().ToArray();

                return Array.ConvertAll(t, item => (ushort)item);
            }
            return null;
        }

        public static ushort[] ConvertCalToReg(AmmoniaCalibrationData nHcal,int tank) {
            List<ushort> reg = new List<ushort>();
            reg.AddRange(ToUshortArray(nHcal.CalZero));
            reg.AddRange(ToUshortArray(nHcal.CalNonZero));
            reg.AddRange(ToUshortArray(nHcal.ActualZero));
            reg.AddRange(ToUshortArray(nHcal.ActualNonZero));
            reg.AddRange(ToUshortArray(nHcal.TotalWeight));
            reg.AddRange(ToUshortArray(nHcal.GasWeight));
            reg.Add((ushort)tank);
            return reg.ToArray();
        }

        public static byte[] GetBytes(Int16[] arr) {
            return arr.Select(x => (byte)x).ToArray();
        }
    }
}
