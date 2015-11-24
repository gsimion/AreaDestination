namespace AreaDestination
{
   using System;

   /// <summary>
   /// Generic interface defining a range.
   /// </summary>
   public interface IRange<T> : ICloneable where T : IComparable
   {
       /// <summary>
      /// Start of the range.
      /// </summary>
      T Start { get; }
       /// <summary>
      /// End of the range.
      /// </summary>
      T End { get; }
   }
}
