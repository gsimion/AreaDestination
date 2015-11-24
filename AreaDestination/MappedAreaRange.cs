namespace AreaDestination
{
   using System;

   /// <summary>
   /// Object holding a range associated to a mapping.
   /// </summary>
   /// <typeparam name="T">Type of destination ID</typeparam>
   public class MappedAreaRange<T> : ZeroOneAreaRange where T : IComparable 
   {
      /// <summary>
      /// Mapping from ID.
      /// </summary>
      public T FromId { get; private set; }
      /// <summary>
      /// Mapping to ID.
      /// </summary>
      public T ToId { get; private set; }

      /// <summary>
      /// Creates a new Mapped Area Range instance, holding a range associated to a mapping.
      /// </summary>
      /// <param name="from">Mapping from ID</param>
      /// <param name="to">Mapping to ID</param>
      /// <param name="start">Range start</param>
      /// <param name="end">Range end</param>
      public MappedAreaRange(T from, T to, Decimal start, Decimal end)
         : base(start, end)
      {
         FromId = from;
         ToId = to;
      }
   }
}
