//*********************************************************************************
//      SystemTime.cs
//*********************************************************************************
// File Name: SystemTime.cs
// Description: 系統時間操作類別
//
//(c) Copyright 2014, MIRLE Automation Corporation
//
// Date          Author         Request No.    Tag     Description
// ------------- -------------  -------------  ------  -----------------------------
// 2014/06/04    Hayes Chen     N/A            N/A     Initial Release
// 
//**********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OHTM.ReadCsv
{
    /// <summary>
    /// Struct SystemTime
    /// </summary>
    public struct SystemTime
    {
        /// <summary>
        /// The year
        /// </summary>
        public ushort Year;
        /// <summary>
        /// The month
        /// </summary>
        public ushort Month;
        /// <summary>
        /// The day of week
        /// </summary>
        public ushort DayOfWeek;
        /// <summary>
        /// The day
        /// </summary>
        public ushort Day;
        /// <summary>
        /// The hour
        /// </summary>
        public ushort Hour;
        /// <summary>
        /// The minute
        /// </summary>
        public ushort Minute;
        /// <summary>
        /// The second
        /// </summary>
        public ushort Second;
        /// <summary>
        /// The millisecond
        /// </summary>
        public ushort Millisecond;

        /// <summary>
        /// Convert form System.DateTime
        /// </summary>
        /// <param name="time">The time.</param>
        public void FromDateTime(DateTime time)
        {
            Year = (ushort)time.Year;
            Month = (ushort)time.Month;
            DayOfWeek = (ushort)time.DayOfWeek;
            Day = (ushort)time.Day;
            Hour = (ushort)time.Hour;
            Minute = (ushort)time.Minute;
            Second = (ushort)time.Second;
            Millisecond = (ushort)time.Millisecond;
        }

        /// <summary>
        /// Convert to System.DateTime
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond);
        }

        /// <summary>
        /// Convert to System.DateTime
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>DateTime.</returns>
        public static DateTime ToDateTime(SystemTime time)
        {
            return time.ToDateTime();
        }

        /// <summary>
        /// Sets the system time.
        /// </summary>
        /// <param name="lpSystemTime">The lp system time.</param>
        /// <returns>System.UInt32.</returns>
        [DllImport("kernel32.dll", EntryPoint = "SetLocalTime", SetLastError = true)]
        public extern static uint SetSystemTime(ref SystemTime lpSystemTime);

        /// <summary>
        /// Gets the system time.
        /// </summary>
        /// <param name="sysTime">The system time.</param>
        [DllImport("kernel32.dll", EntryPoint = "GetLocalTime", SetLastError = true)]
        public extern static void GetSystemTime(ref SystemTime sysTime);
    }
}
