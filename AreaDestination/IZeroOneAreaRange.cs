namespace AreaDestination
{
   /// <summary>
   /// Intergace defining specific methods for an area range between 0 and 1.
   /// Range is hold as [start,end).
   /// </summary>
   public interface IZeroOneAreaRange : IRange<decimal>
   {
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      bool InRange(ulong value, bool inclIntersection);
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      bool InRange(decimal value, bool inclIntersection);
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      bool InRange(decimal start, decimal end, bool inclIntersection);
      /// <summary>
      /// Checks whether a value falls in area range. Include partial intersections.
      /// </summary>
      bool InRange(IRange<decimal> range, bool inclIntersection);
   }
}
