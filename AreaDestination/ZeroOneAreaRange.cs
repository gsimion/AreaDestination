using System;
using System.Globalization;

namespace AreaDestination
{
   /// <summary>
   /// Class defining an area range between 0 and 1.
   /// Range is hold as [start,end).
   /// </summary>
   public class ZeroOneAreaRange : ZeroOneDecimalRange, IZeroOneAreaRange
   {
      protected const Char Zero = '0';
      protected const Char Nine = '9';

      /// <summary>
      /// Creates a new area range in [0,1], where the start is included and the end is excluded.
      /// </summary>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      public ZeroOneAreaRange(Decimal start, Decimal end)
         : base(start, end)
      {
      }

      /// <summary>
      /// Creates a area range where <seealso cref="Start"/>start = 0 and <seealso cref="End"/>end = 1.
      /// </summary>
      public ZeroOneAreaRange()
         : base()
      {
      }

      /// <summary>
      /// Gets the string representation of the <see cref="ZeroOneAreaRange"/>area range.
      /// </summary>
      public override string ToString()
      {
         if (Start < 0.1m)
         {
            // old code, this works in any case but with string parsing
            string _start = (Start != 0) ? Start.ToString().Substring(2) : Zero.ToString();
            string _end = (End != 1) ? (End - GetUnit(End)).ToString().Substring(2) : Nine.ToString();
            if (_start.Length < _end.Length)
               _start = _start + new string(Zero, _end.Length - _start.Length);
            if (_end.Length < _start.Length)
               _end = _end + new string(Nine, _start.Length - _end.Length);
            if ((Start != 0 && Start + GetUnit(Start) == End) || (String.Equals(_start, _end, StringComparison.InvariantCulture)))
               return _start;
            return _start + Global.Del + _end;
         }

         ulong start = ConvertImplicitly(Start);
         ulong end = (End != 1) ? ConvertImplicitly(End) - 1 : 9;

         int startPrecision = GetMostSignificantPosition(start);
         int endPrecision = GetMostSignificantPosition(end);

         if (endPrecision > startPrecision)
            for (int i = 0; i < endPrecision - startPrecision; i++)
               start *= 10;
         if (endPrecision < startPrecision)
            for (int i = 0; i < startPrecision - endPrecision; i++)
               end = (end * 10) + 9;
         if (start == end)
            return start.ToString();
         return start.ToString() + Global.Del + end.ToString();
      }

      /// <summary>
      /// Creates a new instance of area range object with the same properties.
      /// </summary>
      public override object Clone()
      {
         return new ZeroOneAreaRange(Start, End);
      }

      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      public bool InRange(ulong value, bool inclIntersection)
      {
         return InRange(ConvertImplicitly(value), inclIntersection);
      }

      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      public bool InRange(Decimal value, bool inclIntersection)
      {
         return InRange(value, value + Smallest, inclIntersection);
      }

      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      public bool InRange(IRange<Decimal> range, bool inclIntersection)
      {
         return InRange(range.Start, range.End, inclIntersection);
      }

      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      public bool InRange(Decimal start, Decimal end, bool inclIntersection)
      {
         if (inclIntersection)
            return IsSharingRange(new ZeroOneAreaRange(start, end));
         else
            return ContainsRange(new ZeroOneAreaRange(start, end));
      }

   }

   /// <summary>
   /// Class defining a decimal range between 0 and 1.
   /// Range is hold as (start,end).
   /// </summary>
   public class ZeroOneDecimalRange : Range<Decimal>, IRange<Decimal>
   {
      public const string DecimalFormat = "0.0########################";
      protected const Decimal Smallest = 0.0000000000000000000000001m;

      /// <summary>
      /// Creates a new decimal range in [0,1], where the start is included and the end is excluded.
      /// </summary>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      public ZeroOneDecimalRange(Decimal start, Decimal end)
         : base(start, end)
      {
         if (start < 0 || end > 1)
         {
            throw new ArgumentException(Properties.Resources.msgValuesMustFallWithin01);
         }
      }

      /// <summary>
      /// Creates a decimal range where <seealso cref="Start"/>start = 0 and <seealso cref="End"/>end = 1.
      /// </summary>
      public ZeroOneDecimalRange()
         : base(0, 1)
      {
      }

      /// <summary>
      /// Creates a new instance of area range object with the same properties.
      /// </summary>
      public override Object Clone()
      {
         return new ZeroOneDecimalRange(Start, End);
      }

      /// <summary>
      /// Get the least important unit of a decimal having decimal values.
      /// </summary>
      /// <param name="value">Value to get the least important unit from</param>
      /// <returns>Least important unit</returns>
      public static Decimal GetUnit(Decimal value)
      {
         int iPrecision = BitConverter.GetBytes(Decimal.GetBits(value)[3])[2];
         Decimal dcUnit = Decimal.One;
         while (iPrecision > 0)
         {
            dcUnit /= 10;
            iPrecision--;
         }
         return dcUnit;
      }

      /// <summary>
      /// Converts implicitly a UInt64 value to a decimal value between 0 and 1, so that 123 is converted to 0.123.
      /// </summary>
      /// <param name="value">UInt64 value to convert to decimal</param>
      /// <returns>Decimal [0,1] value</returns>
      /// <remarks>Equivalent to Convert.ToDecimal("." + value.ToString(), CultureInfo.InvariantCulture.NumberFormat) for decimal in [0,1]</remarks>
      public static Decimal ConvertImplicitly(ulong value)
      {
         Decimal dcValue = (Decimal)value;
         Int32[] bytes = Decimal.GetBits(dcValue);
         return new Decimal(bytes[0], bytes[1], bytes[2], false, GetMostSignificantPosition(value));
      }

      /// <summary>
      /// Gets the position of the most significant digit belonging to a UInt64 value.
      /// </summary>
      /// <param name="value">UInt64 value</param>
      /// <returns>Position of the most significant digit</returns>
      protected static byte GetMostSignificantPosition(ulong value)
      {
         byte unit = 1;
         ulong dec = 10;
         while (value % dec != value)
         {
            dec *= 10;
            unit++;
         }
         return unit;
      }

      /// <summary>
      /// Converts implicitly a Decimal [0,1] value to a UInt64 value, so that 0.123 is converted to 123.
      /// </summary>
      /// <param name="value">Decimal value in [0,1] to convert to UInt64</param>
      /// <returns>UInt64 value</returns>
      public static ulong ConvertImplicitly(Decimal value)
      {
         if (value.Equals(0))
            return 0;
         ulong longNo = Convert.ToUInt64(value.ToString().Substring(2).TrimEnd(Global.Zero), CultureInfo.InvariantCulture.NumberFormat);
         return longNo;
      }
   }
}
