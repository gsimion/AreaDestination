namespace AreaDestination
{
   using System;
   using System.Collections.Generic;

   /// <summary>
   /// Interface defining a generic destination.
   /// </summary>
   /// <typeparam name="T">Type of destination ID</typeparam>
   public interface IDestination<T> where T : IComparable
   {
      /// <summary>
      /// Gets the list of single codes belonging to the destination instance.
      /// </summary>
      IEnumerable<ulong> SingleCodes { get; }
      /// <summary>
      /// Gets the list of areas belonging to the destination instance.
      /// </summary>
      IEnumerable<IArea<T>> Areas { get; }
      /// <summary>
      /// Destination Id.
      /// </summary>
      T Id { get; }
      /// <summary>
      /// Gets whether the destination is empty.
      /// </summary>
      bool IsEmpty { get; }
   }
}
