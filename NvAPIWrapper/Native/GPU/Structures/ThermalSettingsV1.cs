﻿using System.Linq;
using System.Runtime.InteropServices;
using NvAPIWrapper.Native.Attributes;
using NvAPIWrapper.Native.General.Structures;
using NvAPIWrapper.Native.Interfaces;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace NvAPIWrapper.Native.GPU.Structures
{
    /// <summary>
    ///     Holds a list of thermal sensor information settings (temperature values)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    [StructureVersion(1)]
    public struct ThermalSettingsV1 : IInitializable, IThermalSettings
    {
        public const int MaxThermalSensorsPerGPU = 3;

        internal StructureVersion _Version;
        internal readonly uint _Count;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxThermalSensorsPerGPU)] internal readonly ThermalSensor[]
            _Sensors;

        /// <inheritdoc />
        public IThermalSensor[] Sensors
            => _Sensors.Take((int) _Count).Cast<IThermalSensor>().ToArray();

        /// <summary>
        ///     Holds information about a single thermal sensor
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ThermalSensor : IThermalSensor
        {
            internal readonly ThermalSettingsController _Controller;
            internal readonly uint _DefaultMinTemp;
            internal readonly uint _DefaultMaxTemp;
            internal readonly uint _CurrentTemp;
            internal readonly ThermalSettingsTarget _Target;

            /// <inheritdoc />
            public ThermalSettingsController Controller => _Controller;

            /// <inheritdoc />
            public int DefaultMinimumTemperature => (int) _DefaultMinTemp;

            /// <inheritdoc />
            public int DefaultMaximumTemperature => (int) _DefaultMaxTemp;

            /// <inheritdoc />
            public int CurrentTemperature => (int) _CurrentTemp;

            /// <inheritdoc />
            public ThermalSettingsTarget Target => _Target;
        }
    }
}