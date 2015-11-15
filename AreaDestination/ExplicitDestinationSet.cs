using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Collections;

namespace AreaDestination
{
   /// <summary>
   /// Class representing a destination set, that is a set area code ranges grouped in destinations
   /// One destination is explicitly defined by a set of area code ranges that are not overlapping with 
   /// any other destination in the destination set. Keeps all the destinations in explicit representation.
   /// </summary>
   /// <typeparam name="T">Type of destination ID</typeparam>
   public class ExplicitDestinationSet<T> : DestinationSetBase<ExplicitDestination<T>, T> where T : IComparable 
   {
      private Dictionary<T, ExplicitDestination<T>> _destinations = new Dictionary<T, ExplicitDestination<T>>();

      public ExplicitDestinationSet()
      {
      }

      /// <summary>
      /// Gets the list of all the destinations (included empty) in the destination set.
      /// </summary>
      public override Dictionary<T, ExplicitDestination<T>> Destinations { get { return _destinations; } }

      /// <summary>
      /// Gets the areas as [a, b) ranges, where a is less than b, ordered by a.
      /// </summary>
      private IEnumerable<Area<T>> Ranges
      {
         get
         {
            List<Area<T>> areas = new List<Area<T>>();
            foreach (T dest in _destinations.Keys)
               areas.AddRange(from range in _destinations[dest].GetRanges() select new Area<T>(dest, range.Start, range.End));
            return areas.OrderBy(x => x.Start);
         }
      }

      /// <summary>
      /// Gets the explicit areas [a, b) where a is the same order of b and a is less than b, ordered by a.
      /// </summary>
      public IEnumerable<IArea<T>> ExplicitAreas
      {
         get
         {
            List<IArea<T>> areas = new List<IArea<T>>();
            foreach (T dest in _destinations.Keys)
               areas.AddRange(from range in _destinations[dest].Areas select new Area<T>(dest, range.Start, range.End) as IArea<T>);
            return areas.OrderBy(x => x.Start);
         }
      }

      /// <summary>
      /// Adds a destination to the destination set.
      /// </summary>
      /// <param name="d">Destination to add</param>
      public override void AddDestination(ExplicitDestination<T> d)
      {
         UpdateDestination(d.Id, d.ToString());
      }

      /// <summary>
      /// Adds an area to a destination, or create a new destination if it does not exists
      /// </summary>
      /// <param name="i">The name of the destination to update</param>
      /// <param name="a">The area code to add</param>
      public override void AddArea(T id, ulong a)
      {
         ExplicitDestination<T> destination = null;
         if (!_destinations.TryGetValue(id, out destination))
         {
            destination = new ExplicitDestination<T>(id);
            _destinations.Add(id, destination);
         }
         foreach (ExplicitDestination<T> exisitngdest in _destinations.Where(x => !x.Key.Equals(id) && x.Value.CoveredRange.InRange(a, true)).Select(x => x.Value))
         {
            exisitngdest.RemoveArea(a);
         }
         _destinations[id].AddArea(a);

      }


      /// <summary>
      /// Removes an area from a destination if it exists
      /// </summary>
      /// <param name="i">The name of the destination to update</param>
      /// <param name="code">The area code to add</param>
      public override void RemoveArea(ulong code)
      {
         foreach (ExplicitDestination<T> exisitngdest in _destinations.Where(x => x.Value.CoveredRange.InRange(code, true)).Select(x => x.Value))
            exisitngdest.RemoveArea(code);
      }

      /// <summary>
      /// Updates a destination, by replacing existing definition of areas with the new specified one.
      /// </summary>
      /// <param name="id">Destination id</param>
      /// <param name="areas">Areas as string representation, where area code separator is ',' and range delimiter is '-'</param>
      public override void UpdateDestination(T id, string areas)
      {
         List<Area<T>> arealist = new List<Area<T>>();

         ulong[][] areamatrix = GetAreasAndRanges(areas);
         foreach (ulong[] area in areamatrix)
         {
            if (area.Length == 1)
               arealist.Add(new Area<T>(id, area[0]));
            else if (area.Length == 2)
               arealist.Add(new Area<T>(id, area[0], area[1]));
            else
               throw new FormatException(Properties.Resources.msgInvalidAreaFormatPassed);
         }
         ExplicitDestination<T> destination = null;
         if (!_destinations.TryGetValue(id, out destination))
         {
            destination = new ExplicitDestination<T>(id);
            _destinations.Add(id, destination);
         }
         destination.Clear();
         foreach (Area<T> area in arealist)
         {
            foreach (ExplicitDestination<T> exisitngdest in _destinations.Where(x => !x.Key.Equals(id) && x.Value.CoveredRange.InRange(area, true)).Select(x => x.Value))
               exisitngdest.RemoveArea(area);
            destination.AddArea(area);
         }
      }

      /// <summary>
      /// Gets a matrix representing the areas passed as single area (array with single element) or range (array with two elements).
      /// </summary>
      /// <param name="areas">Full area representation, where area code separator is ',' and range delimiter is '-'</param>
      /// <returns>Area matrix</returns>
      protected UInt64[][] GetAreasAndRanges(string areas)
      {
         return GetAreasAndRanges(areas, Sep, Del);
      }

      /// <summary>
      /// Gets the list of destinations containing at least one explicit area.
      /// </summary>
      public Dictionary<T, ExplicitDestination<T>> ExplicitDestinations
      {
         get
         {
            IEnumerable<IArea<T>> expl = ExplicitAreas;
            Dictionary<T, ExplicitDestination<T>> dests = new Dictionary<T, ExplicitDestination<T>>();
            foreach (Area<T> a in expl)
            {
               if (!dests.ContainsKey(a.Id))
                  dests[a.Id] = new ExplicitDestination<T>(a.Id);
               dests[a.Id].AddArea(a);
            }
            return dests;
         }
      }

      /// <summary>
      /// Maps the destination set to another destination set
      /// </summary>
      /// <param name="b">The destination set to be mapped to</param>
      /// <returns>The result of the mapping between the two sets</returns>
      public DestinationSetBase.CMappingResult<T> MapToDestinationSet(ExplicitDestinationSet<T> b)
      {
         IArea<T>[] fromExplicit = this.ExplicitAreas.ToArray();
         IArea<T>[] toExplicit = b.ExplicitAreas.ToArray();

         int destToAreaIndex = 0;
         int destFromAreaIndex = 0;
         IArea<T> rangeFrom, rangeTo;
         DestinationSetBase.CMappingResult<T> mapRes = new DestinationSetBase.CMappingResult<T>();

         while (destFromAreaIndex < fromExplicit.Length && destToAreaIndex < toExplicit.Length)
         {
            rangeFrom = fromExplicit.ElementAt(destFromAreaIndex);
            rangeTo = toExplicit.ElementAt(destToAreaIndex);
            if (rangeTo.End <= rangeFrom.Start)
               destToAreaIndex++;
            else
            {
               if (rangeTo.Start < rangeFrom.End)
               {
                  mapRes.AddMap(new MappedAreaRange<T>(rangeFrom.Id, rangeTo.Id, Math.Max(rangeFrom.Start, rangeTo.Start), Math.Min(rangeFrom.End, rangeTo.End)));
               }
               if (rangeTo.End < rangeFrom.End)
                  destToAreaIndex++;
               else
                  destFromAreaIndex++;
            }
         }

         return mapRes;
      }

      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="value">Area to find in the destination set</param>
      /// <returns>Destination name, if area falls within the destination set, <value>null</value> otherwise</returns>
      public override T FindArea(ulong value)
      {
         return FindArea(new Area<T>((T)Utility<T>.Default(), value));
      }

      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="a">Area to find in the destination set</param>
      /// <returns>Destination name, if area falls within the destination set, <value>null</value> otherwise</returns>
      protected override T FindArea(ZeroOneAreaRange a)
      {
         Area<T> lookUpArea = Ranges.LastOrDefault(c => c.InRange(a, false));
         if (lookUpArea == null)
            return default(T);
         else
            return lookUpArea.Id;
      }

      /// <summary>
      /// Performs the string match search among all the destinations to find destinations that are covering a given area.
      /// For instance area '123' is covered by destination 'A' with '1234' and 'B' with '1235'.
      /// </summary>
      /// <param name="code">Area to find in the destination set</param>
      /// <returns>Collection containing destination defining the area names</returns>
      public override IEnumerable<T> FindDestinations(ulong code)
      {
         return FindDestinations(new Area<T>((T)Utility<T>.Default(), code));
      }

      /// <summary>
      /// Performs the string match search among all the destinations to find destinations that are covering a given area.
      /// For instance area '123' is covered by destination 'A' with '1234' and 'B' with '1235'.
      /// </summary>
      /// <param name="area">Area to find in the destination set, can be a range, with '-' as delimiter</param>
      /// <returns>Collection containing destination defining the area names</returns>
      public override IEnumerable<T> FindDestinations(string area)
      {
         ulong[][] areamatrix = GetAreasAndRanges(area);
         if (areamatrix.Length != 1 || areamatrix[0].Length > 2)
            throw new FormatException(Properties.Resources.msgInvalidAreaFormatPassed);
         if (areamatrix[0].Length == 1)
            return FindDestinations(new Area<T>((T)Utility<T>.Default(), areamatrix[0][0]));
         else
            return FindDestinations(new Area<T>((T)Utility<T>.Default(), areamatrix[0][0], areamatrix[0][1]));
      }

      /// <summary>
      /// Performs the string match search among all the destinations to find destinations that are covering a given area.
      /// For instance area '123' is covered by destination 'A' with '1234' and 'B' with '1235'.
      /// </summary>
      /// <param name="a">Area to find in the destination set</param>
      /// <returns>Array containing destination names</returns>
      private T[] FindDestinations(ZeroOneAreaRange a)
      {
         T[] FindDestinations = Ranges.Where(c => a.InRange(c, false)).Select(x => x.Id).ToArray();
         return FindDestinations;
      }
   }
}
