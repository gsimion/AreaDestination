namespace AreaDestination
{
   using System;
   using System.Collections.Generic;

   /// <summary>
   /// Interface defining a generic area.
   /// </summary>
   /// <typeparam name="T">Type of area ID</typeparam>
   public interface IArea<T> : IRange<Decimal> where T : IComparable
   {
      /// <summary>
      /// Gets the string representation of the <see cref="ZeroOneAreaRange"/>area range.
      /// </summary>
      string ToString();
      /// <summary>
      /// Gets the string representation of the <see cref="ZeroOneAreaRange"/>area range, with a passed range delimiter.
      /// </summary>
      /// <param name="del">Range delimiter</param>
      /// <returns>String representation of the <see cref="ZeroOneAreaRange"/>area range</returns>
      string ToString(char del);
      /// <summary>
      /// Area id.
      /// </summary>
      T Id { get; }
   }
}
