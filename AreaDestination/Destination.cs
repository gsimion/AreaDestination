namespace AreaDestination
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;

   /// <summary>
   /// Class representing a destination as a hash set of single area codes, including expanded version of ranges. 
   /// The class does not check for child areas overlapping, but does not allow same implicit areas to be assigned twice to the same destination.
   /// The class supports adding ranges in implicit format and getting a compact version of it.
   /// </summary>
   /// <typeparam name="T">Type of destination ID</typeparam>
   public class Destination<T> : IDestination<T> where T : IComparable
   {
      /// <summary>
      /// Destination Id.
      /// </summary>
      public T Id { get; private set; }

      private readonly HashSet<ulong> _implicitAreas = new HashSet<ulong>();
      private List<Area<T>> _areas;
      private bool _areasNeedsRebuild = false;

      /// <summary>
      /// Gets the list of areas belonging to the destination instance.
      /// </summary>
      public IEnumerable<IArea<T>> Areas
      {
         get
         {
            if (_areasNeedsRebuild)
               PopulateRanges();
            return _areas.Select(x => x as IArea<T>);
         }
      }

      /// <summary>
      /// Gets the list of single codes belonging to the destination instance.
      /// </summary>
      public IEnumerable<ulong> SingleCodes { get { return ImplicitAreas; } }

      /// <summary>
      /// Get the implicit areas.
      /// </summary>
      public IEnumerable<ulong> ImplicitAreas { get { return _implicitAreas.ToList(); } }

      /// <summary>
      /// Gets whether the destination contains implicit areas.
      /// </summary>
      public bool IsEmpty { get { return !_implicitAreas.Any(); } }

      /// <summary>
      /// Creates a new empty destination.
      /// </summary>
      /// <param name="id">Destination id</param>
      internal Destination(T id)
      {
         Id = id;
         _areas = new List<Area<T>>();
      }

      /// <summary>
      /// Creates a new empty destination.
      /// </summary>
      /// <param name="id">Destination id</param>
      /// <param name="areas">List of areas belonging to the destination, not containing overlaps</param>
      internal Destination(T id, List<Area<T>> areas)
      {
         Id = id;
         _areas = areas;
      }

      /// <summary>
      /// Empties the destination.
      /// </summary>
      public void Clear()
      {
         _areas.Clear();
         _implicitAreas.Clear();
         _areasNeedsRebuild = true;
      }

      /// <summary>
      /// Updates internal ranges.
      /// </summary>
      private void PopulateRanges()
      {
         _areas.Clear();
         foreach (ulong a in _implicitAreas)
            _areas.Add(new Area<T>(Id, a));
         // removeOverlap();
         _areasNeedsRebuild = false;
      }

      /// <summary>
      /// Removes internal overlaps.
      /// </summary>
      private void removeOverlap()
      {
         var sortedRanges = _areas.OrderBy(c => c.Start);
         List<Area<T>> compat = new List<Area<T>>();
         foreach (Area<T> a in sortedRanges)
         {
            if (compat.Count() > 0 && compat.Last().Start <= a.Start && compat.Last().End >= a.End)
               continue;
            compat.Add(a);
         }
         _areas = compat;
      }

      /// <summary>
      /// Adds an area code to the destination.
      /// </summary>
      /// <param name="code">Area code to add</param>
      internal void AddArea(ulong code)
      {
         _implicitAreas.Add(code);
         _areasNeedsRebuild = true;
      }

      /// <summary>
      /// Removes an area code from the destination.
      /// </summary>
      /// <param name="code">Area code to remove</param>
      internal void RemoveArea(ulong code)
      {
         if (_implicitAreas.Contains(code))
            _implicitAreas.Remove(code);
         _areasNeedsRebuild = true;
      }

      /// <summary>
      /// Adds an area to the destination.
      /// </summary>
      /// <param name="area">Area to add</param>
      public void AddArea(IArea<T> area)
      {
         if (_implicitAreas.Count > 0)
            throw new InvalidOperationException(Properties.Resources.msgUnableToAddRangesToDestinationContainingImplicitAreas);
         _areas.Add(new Area<T>(Id, area.Start, area.End));
      }

      /// <summary>
      /// Gets the string representation of the destination.
      /// </summary>
      /// <returns>String representation of the destination</returns>
      public override string ToString()
      {
         Area<T> curArea = null;
         List<Area<T>> compatAreas = new List<Area<T>>();
         foreach (Area<T> a in Areas.OrderBy(x=>x.Start, new AreaStartComparer()))
         {
            if (curArea == null || (curArea != null && curArea.End != a.Start))
            {
               curArea = new Area<T>(a.Id, a.Start, a.End);
               compatAreas.Add(curArea);
            }
            else
            {
               curArea.End = a.End;
            }
         }
         if (compatAreas.Count == 0)
            return string.Empty;
         else if (compatAreas.Count == 1)
            return compatAreas[0].ToString();
         else
         {
            StringBuilder sb = new StringBuilder();
            string res = compatAreas.Aggregate(sb, (builder, area) => builder.Append(Global.Sep).Append(area.ToString())).ToString().Substring(1);
            return res;
         }
      }
   }
}
