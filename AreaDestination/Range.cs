namespace AreaDestination
{
   using System;

   /// <summary>
   /// Class defining a generic range.
   /// </summary>
   public abstract class Range<T> : IRange<T> where T : IComparable
   {
      /// <summary>
      /// Start of the range.
      /// </summary>
      public T Start { get; set; }
      /// <summary>
      /// End of the range.
      /// </summary>
      public T End { get; set; }

      /// <summary>
      /// Creates a new range.
      /// </summary>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      public Range(T start, T end)
      {
         if (end.CompareTo(start) <= 0)
         {
            throw new ArgumentException(Properties.Resources.msgEndValueMustBeGreaterThanStart);
         }
         Start = start;
         End = end;
      }

      /// <summary>
      /// Creates a new empty range.
      /// </summary>
      public Range()
      {
      }

      /// <summary>
      /// Determines if the provided value is inside the current instance of range.
      /// </summary>
      protected bool ContainsValue(T value)
      {
         return (Start.CompareTo(value) <= 0) && (value.CompareTo(End) <= 0);
      }

      /// <summary>
      /// Determines if this Range is inside the bounds of the current instance of range.
      /// </summary>
      protected bool ContainsRange(Range<T> range)
      {
         return ContainsValue(range.Start) && ContainsValue(range.End);
      }

      /// <summary>
      /// Determines if another range is sharing something with the current instance of range.
      /// </summary>
      protected bool IsSharingRange(Range<T> range)
      {
         return range.ContainsRange(this) || ContainsValue(range.Start) || ContainsValue(range.End);
      }

      /// <summary>
      /// Creates a new instance of Range object with the same properties.
      /// </summary>
      public abstract object Clone();
   }
}
