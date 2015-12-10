namespace AreaDestination
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;

   /// <summary>
   /// Shared API for getting abstract representation of destination, set, and area as rectangles.
   /// </summary>
   public static class Painter
   {
      /// <summary>
      /// Default scale factor for rectangles.
      /// </summary>
      public const int ScaleFactor = 1;
      
      /// <summary>
      /// Gets list of rectangles representing the areas of a destination set excluding the uncovered areas.
      /// </summary>
      /// <typeparam name="T0">Type of destination</typeparam>
      /// <typeparam name="T1">Type of destination id</typeparam>
      /// <param name="set">Destination set</param>
      /// <param name="scale">Scale</param>
      /// <returns>List of rectangles representing the areas of a destination set excluding the uncovered areas</returns>
      public static List<AreaRectangle<T1>> GetRepresentation<T0, T1>(IDestinationSet<T0, T1> set, int scale = ScaleFactor)
         where T1 : IComparable
         where T0 : IDestination<T1>
      {
         DestinationSet<T1> benchmark = new DestinationSet<T1>();
         T1 undefined = benchmark.UndefinedDestinationId ; 
         List<AreaRectangle<T1>> rect = new List<AreaRectangle<T1>>();
         foreach (IDestination<T1> d in set.Destinations.Values)
         {
            if (!d.Id.Equals(undefined))
               rect.AddRange(GetRepresentation<T1>(d, scale));
         }
         List<AreaRectangle<T1>> res = (from r in rect orderby r._rank descending, r.Start ascending select r).ToList();
         return res;
      }

      /// <summary>
      /// Gets list of rectangles representing the areas of a destination.
      /// </summary>
      /// <typeparam name="T">Type of destination id</typeparam>
      /// <param name="destination">Destination</param>
      /// <param name="scale">Scale</param>
      /// <returns>List of rectangles representing the areas of a destination</returns>
      public static List<AreaRectangle<T>> GetRepresentation<T>(IDestination<T> destination, int scale = ScaleFactor)
         where T : IComparable
      {
         List<AreaRectangle<T>> rect = new List<AreaRectangle<T>>();
         foreach (IArea<T> a in destination.Areas)
         {
            AreaRectangle<T> r = new AreaRectangle<T>(a);
            r.SetScale(scale);
            rect.Add(r);
         }
         return rect;
      }

      /// <summary>
      /// Gets rectangle representing the area.
      /// </summary>
      /// <typeparam name="T">Type of area id</typeparam>
      /// <param name="area">Area</param>
      /// <param name="scale">Scale</param>
      /// <returns>Rectangle representing the area</returns>
      public static AreaRectangle<T> GetRepresentation<T>(IArea<T> area, int scale = ScaleFactor)
         where T : IComparable
      {
         return GetRepresentation<T>(area, string.Empty, scale);
      }

      /// <summary>
      /// Gets rectangle representing the area.
      /// </summary>
      /// <typeparam name="T">Type of area id</typeparam>
      /// <param name="area">Area</param>
      /// <param name="description">Description</param>
      /// <param name="scale">Scale</param>
      /// <returns>Rectangle representing the area</returns>
      public static AreaRectangle<T> GetRepresentation<T>(IArea<T> area, string description, int scale = ScaleFactor)
         where T : IComparable
      {
         return new AreaRectangle<T>(area, description);
      }

   }
}
