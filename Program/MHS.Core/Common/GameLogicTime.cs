using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core.Common
{
    /// <summary>
    /// Logical time in the game
    /// </summary>
    public class GameLogicTime
    {
        /// <summary>
        /// Elapsed unit - in minute
        /// </summary>
        public long elapsedUnits;


        /// <summary>
        /// Game starting day of week - Day of week at the 0 unit elapsed
        /// </summary>
        public const DayOfWeek kStartDayOfWeek = DayOfWeek.Monday;

        /// <summary>
        /// Starting time offset for the simplicity - starting at the midnight 00:00 is umm...
        /// </summary>
        public const long kStartUnitOffset = 720;


        /// <summary>
        /// Total minute elapsed
        /// </summary>
        public double TotalMinute { get { return elapsedUnits; } }

        /// <summary>
        /// Total minute elapsed count
        /// </summary>
        public long TotalMinutes { get { return elapsedUnits; } }

        /// <summary>
        /// Total hour elapsed
        /// </summary>
        public double TotalHour { get { return elapsedUnits / 60.0; } }

        /// <summary>
        /// Total hour elapsed count
        /// </summary>
        public long TotalHours { get { return elapsedUnits / 60; } }

        /// <summary>
        /// Total days elapsed
        /// </summary>
        public double TotalDay { get { return elapsedUnits / 1440.0; } }

        /// <summary>
        /// Total days elapsed count
        /// </summary>
        public long TotalDays { get { return elapsedUnits / 1440; } }


        /// <summary>
        /// Days part of elapsed time
        /// </summary>
        public long Days { get { return elapsedUnits / 1440; } }

        /// <summary>
        /// Hours part of elapsed time
        /// </summary>
        public long Hours { get { return (elapsedUnits % 1440) / 60; } }

        /// <summary>
        /// Minutes part of elapsed time
        /// </summary>
        public long Minutes { get { return elapsedUnits % 60; } }

        /// <summary>
        /// The day of week
        /// </summary>
        public DayOfWeek DayOfWeek { get { return s_dayOfWeekTable[TotalDays % 7]; } }


        /// <summary>
        /// Get elapsed time from standard starting time
        /// </summary>
        public GameLogicTime FromStandardStartTime { get { return this - StandardStarting; } }


        /// <summary>
        /// Add time interval
        /// </summary>
        /// <param name="rhs">Adding interval of time</param>
        public void AddTime(GameLogicTime rhs)
        {
            elapsedUnits += rhs.elapsedUnits;
        }

        /// <summary>
        /// Set time as given time
        /// </summary>
        /// <param name="rhs">Reference time</param>
        public void Set(GameLogicTime rhs)
        {
            elapsedUnits = rhs.elapsedUnits;
        }


        #region Generators

        /// <summary>
        /// Constructor with time in initial elapsed unit
        /// </summary>
        /// <param name="initialElapsedUnits">Representing time in initial elapsed unit</param>
        public GameLogicTime(long initialElapsedUnits = 0)
        {
            elapsedUnits = initialElapsedUnits;
        }

        /// <summary>
        /// Zero Time
        /// </summary>
        public static GameLogicTime Zero { get { return new GameLogicTime(0); } }

        /// <summary>
        /// Logic Unit Time
        /// </summary>
        public static GameLogicTime Unit { get { return new GameLogicTime(1); } }

        /// <summary>
        /// Standard Strting Time
        /// </summary>
        public static GameLogicTime StandardStarting { get { return new GameLogicTime(kStartUnitOffset); } }

        /// <summary>
        /// Generate a time from total minutes
        /// </summary>
        /// <param name="minutes">Initial elapsed minutes</param>
        /// <returns>A time of given minutes elapsed</returns>
        public static GameLogicTime FromMinutes(long minutes)
        {
            return new GameLogicTime(minutes);
        }

        /// <summary>
        /// Generate a time from total hours
        /// </summary>
        /// <param name="hours">Initial elapsed hours</param>
        /// <returns>A time of given hours elapsed</returns>
        public static GameLogicTime FromHours(long hours)
        {
            return new GameLogicTime(hours * 60);
        }

        /// <summary>
        /// Generate a time from total days
        /// </summary>
        /// <param name="days">Initial elapsed days</param>
        /// <returns>A time of given days elapsed</returns>
        public static GameLogicTime FromDays(long days)
        {
            return new GameLogicTime(days * 1440);
        }

        #endregion


        #region Operators

        public static GameLogicTime operator +(GameLogicTime a, GameLogicTime b)
        {
            return new GameLogicTime() { elapsedUnits = a.elapsedUnits + b.elapsedUnits };
        }

        public static GameLogicTime operator -(GameLogicTime a, GameLogicTime b)
        {
            return new GameLogicTime() { elapsedUnits = a.elapsedUnits - b.elapsedUnits };
        }

        public static GameLogicTime operator -(GameLogicTime a)
        {
            return new GameLogicTime() { elapsedUnits = -a.elapsedUnits };
        }

        public static bool operator ==(GameLogicTime a, GameLogicTime b)
        {
            return (a.elapsedUnits == b.elapsedUnits);
        }

        public static bool operator !=(GameLogicTime a, GameLogicTime b)
        {
            return (a.elapsedUnits != b.elapsedUnits);
        }

        public static bool operator <(GameLogicTime a, GameLogicTime b)
        {
            return (a.elapsedUnits < b.elapsedUnits);
        }

        public static bool operator >(GameLogicTime a, GameLogicTime b)
        {
            return (a.elapsedUnits > b.elapsedUnits);
        }

        public static bool operator <=(GameLogicTime a, GameLogicTime b)
        {
            return (a.elapsedUnits <= b.elapsedUnits);
        }

        public static bool operator >=(GameLogicTime a, GameLogicTime b)
        {
            return (a.elapsedUnits >= b.elapsedUnits);
        }

        public static GameLogicTime operator *(GameLogicTime a, long b)
        {
            return new GameLogicTime() { elapsedUnits = a.elapsedUnits * b };
        }

        public static GameLogicTime operator *(long a, GameLogicTime b)
        {
            return new GameLogicTime() { elapsedUnits = a * b.elapsedUnits };
        }

        #endregion


        #region Type Common Implementation

        public override bool Equals(Object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            return (elapsedUnits == ((GameLogicTime)obj).elapsedUnits);
        }

        public override int GetHashCode()
        {
            return elapsedUnits.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("GameLogicTime({0}D {1}H {2}M)", Days, Hours, Minutes);
        }

        #endregion


        #region Class Common

        static GameLogicTime()
        {
            s_InitializeDayOfWeekTable();
        }

        private static DayOfWeek[] s_dayOfWeekTable;

        static void s_InitializeDayOfWeekTable()
        {
            s_dayOfWeekTable = new DayOfWeek[7];

            DayOfWeek[] generatingTable = new DayOfWeek[7]
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday,
            };

            for (int i = 0; i < 7; ++i)
            {
                if (generatingTable[i] == kStartDayOfWeek)
                {
                    for (int j = 0; j < 7; ++j)
                    {
                        s_dayOfWeekTable[j] = generatingTable[(i + j) % 7];
                    }
                    break;
                }
            }
        }

        #endregion
    }
}
