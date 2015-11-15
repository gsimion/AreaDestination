using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreaDestination
{
   /// <summary>
   /// Intergace defining specific methods for an area range between 0 and 1.
   /// Range is hold as [start,end).
   /// </summary>
   public interface IZeroOneAreaRange : IRange<Decimal>
   {
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      Boolean InRange(ulong value, bool inclIntersection);
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      Boolean InRange(Decimal value, bool inclIntersection);
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      Boolean InRange(Decimal start, Decimal end, bool inclIntersection);
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      Boolean InRange(IRange<Decimal> range, bool inclIntersection);
   }
}
