namespace AreaDestination
{
   using System;
   using System.Globalization;
   using System.Collections.Generic;

   /// <summary>
   /// Class defining an area.
   /// The class supports generic digits, also starting with '0'.
   /// </summary>
   /// <typeparam name="T">Type of area ID</typeparam>
   public class Area<T> : ZeroOneAreaRange, IArea<T> where T : IComparable 
   {
      /// <summary>
      /// String defining area id.
      /// </summary>
      public T Id { get; private set; }

      /// <summary>
      /// Creates a new area.
      /// </summary>
      /// <param name="Id">Id</param>
      /// <param name="code">Area</param>
      public Area(T Id, ulong code)
         : this(Id, ConvertImplicitly(code), IsPowerOf10(code + 1) ? ((code == 0) ? new decimal(0.1) : new decimal(1)) : ConvertImplicitly(code + 1))
      {
      }

      /// <summary>
      /// Creates a new area.
      /// </summary>
      /// <param name="Id">Id</param>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      public Area(T Id, ulong start, ulong end)
         : this(Id, ConvertImplicitly(start), ConvertImplicitly(end + 1))
      {
      }

      /// <summary>
      /// Creates a new area. 
      /// </summary>
      /// <param name="Id">Id</param>
      /// <param name="code">Area</param>
      public Area(T Id, string code)
         : this(Id, Convert.ToDecimal(Global.Dot + code.ToString(), CultureInfo.InvariantCulture.NumberFormat),
         ((code[0] != Zero) && IsPowerOf10(Convert.ToUInt64(code, CultureInfo.InvariantCulture.NumberFormat) + 1)) ? new decimal(1) : Convert.ToDecimal(Global.Dot + code, CultureInfo.InvariantCulture.NumberFormat) + GetUnit(Convert.ToDecimal(Global.Dot + code, CultureInfo.InvariantCulture.NumberFormat)))
      {
      }

      /// <summary>
      /// Creates a new area.
      /// </summary>
      /// <param name="Id">Id</param>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      public Area(T Id, string start, string end)
         : this(Id, Convert.ToDecimal(Global.Dot + start.ToString(), CultureInfo.InvariantCulture.NumberFormat),
         Convert.ToDecimal(Global.Dot + end, CultureInfo.InvariantCulture.NumberFormat) + GetUnit(Convert.ToDecimal(Global.Dot + end, CultureInfo.InvariantCulture.NumberFormat)))
      {
      }

      /// <summary>
      /// Creates a new area.
      /// </summary>
      /// <param name="id">Id</param>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      public Area(T id, decimal start, decimal end)
         : base(start, end)
      {
         this.Id = id;
      }

      /// <summary>
      /// Creates a new instance of area object with the same properties.
      /// </summary>
      public override object Clone()
      {
         return new Area<T>(Id, Start, End);
      }

      /// <summary>
      /// Gets the string representation of the <see cref="ZeroOneAreaRange"/>area range, with a passed range delimiter.
      /// </summary>
      /// <param name="del">Range delimiter</param>
      /// <returns>String representation of the <see cref="ZeroOneAreaRange"/>area range</returns>
      public string ToString(char del)
      {
         return ToString().Replace(Global.Del, del);
      }

      /// <summary>
      /// Gets whether a long is a power of 10.
      /// </summary>
      /// <param name="value">Long value</param>
      /// <returns><value>true</value> if <paramref name="value"/>value is power of 10, <value>false</value> otherwise</returns>
      private static bool IsPowerOf10(ulong value)
      {
         if (value == 1 ||
              value == 10 ||
              value == 100 ||
              value == 1000 ||
              value == 10000 ||
              value == 100000 ||
              value == 1000000 ||
              value == 10000000 ||
              value == 100000000 ||
              value == 1000000000 ||
              value == 10000000000 ||
              value == 100000000000 ||
              value == 1000000000000 ||
              value == 10000000000000 ||
              value == 100000000000000 ||
              value == 1000000000000000 ||
              value == 10000000000000000 ||
              value == 100000000000000000 ||
              value == 1000000000000000000 ||
              value == 10000000000000000000)
            return true;
         return false;
      }
   }

   /// <summary>
   /// Class comparer for start of a generic area, 
   /// so that when the default comparer return equality, is checking in the deepness of the start value.
   /// </summary>
   internal class AreaStartComparer : IComparer<decimal>
   {
      /// <summary>
      /// Compare two areas starting decimals, using the default comparer for decimal.
      /// If starts are the same, prioritize the start having the least precision, 
      /// so that 0.1 is greater than 0.10.
      /// </summary>
      /// <param name="x">First value</param>
      /// <param name="y">Second value</param>
      /// <returns>Comparison result</returns>
      public int Compare(decimal x, decimal y)
      {
         int defaultComparison = decimal.Compare(x, y);
         if (defaultComparison != 0)
            return defaultComparison;
         return decimal.Compare(ZeroOneDecimalRange.GetUnit(y), ZeroOneDecimalRange.GetUnit(x));
      }
   }
}
