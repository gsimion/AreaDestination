using System;

namespace AreaDestination
{
   /// <summary>
   /// Class defining a date range, where day is the least important unit.
   /// </summary>
   public class DateRange : Range<DateTime>, IRange<DateTime>
   {
      private const string FormatRepresentation = "({0}; {1})";

      /// <summary>
      /// Creates a new date range.
      /// </summary>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      public DateRange(DateTime start, DateTime end)
         : base(start.Date, end.Date)
      {
      }

      /// <summary>
      /// Creates a new date range covering the full available date range.
      /// </summary>
      /// <param name="s">Start</param>
      /// <param name="e">End</param>
      public DateRange()
         : base(DateTime.MinValue.Date, DateTime.MaxValue.Date)
      {
      }

      /// <summary>
      /// Gets the string representation of the <see cref="CAreaRange"/>date range.
      /// </summary>
      public override string ToString()
      {
         return string.Format(FormatRepresentation, Start.ToShortDateString(), End.ToShortDateString());
      }

      /// <summary>
      /// Creates a new instance of date range object with the same properties.
      /// </summary>
      public override object Clone()
      {
         return new DateRange(Start, End);
      }
   }
}
