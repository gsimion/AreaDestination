using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AreaDestination
{
   /// <summary>
   /// Class representing a destination set.
   /// The class does not check for child areas overlapping, but does not allow same implicit areas to be assigned twice within the destinaiton set.
   /// One destination is defined by a set of unique area codes. Preserves all the destinations implicit representation.
   /// </summary>
   /// <typeparam name="T">Type of destination ID</typeparam>
   public partial class DestinationSet<T> : DestinationSetBase<Destination<T>, T> where T : IComparable
   {

      #region Internal classes

      private class Node<T0>
      {
         public Node<T0> next;
         public T0 data;

         public Node(T0 a)
         {
            data = a;
            next = null;
         }

         public Node(T0 a, Node<T0> n)
         {
            data = a;
            next = n;
         }
      }

      private static Node<IArea<T>> makeLinkedList(IArea<T>[] e)
      {
         Node<IArea<T>> head = null, cur = null, n;

         for (int j = 0; j < e.Length; j++)
         {
            n = new Node<IArea<T>>(e[j]);
            if (head == null)
               head = n;
            if (cur != null)
            {
               cur.next = n;
               cur = n;
            }
            else
               cur = n;
         }
         return head;
      }

      #endregion

      /// <summary>
      /// Key associated to the uncovered destination 
      /// that represents the difference between what is cover and the full range of possible areas.
      /// </summary>
      public readonly T UndefinedDestinationId = (T)Utility<T>.Default();
      private bool _areasNeedsRebuild = false;
      private readonly Dictionary<T, Destination<T>> _implicitDests = new Dictionary<T, Destination<T>>();
      private readonly Dictionary<ulong, T> _uniqueAreaCodes = new Dictionary<ulong, T>();
      private readonly List<Area<T>> _areas;

      public DestinationSet()
      {
         _areas = new List<Area<T>>();
         for (uint i = 0; i < 10; i++)
            _areas.Add(new Area<T>(UndefinedDestinationId, i));
         UpdateDestination(UndefinedDestinationId, "0-9");
      }

      /// <summary>
      /// Gets the explicit areas [a, b).
      /// </summary>
      public IEnumerable<IArea<T>> Areas
      {
         get
         {
            if (_areasNeedsRebuild)
            {
               PopulateExplicitDestinations();
            }
            return _areas.Select(x => x as IArea<T>);
         }
      }

      /// <summary>
      /// Gets the undefined destination representing the difference between what is cover and the full range of possible areas.
      /// </summary>
      public Destination<T> Undefined { get { return ExplicitDestinations[UndefinedDestinationId]; } }

      /// <summary>
      /// Gets the explicit areas [a, b) by destination id.
      /// </summary>
      public IEnumerable<IArea<T>> ExplicitAreas
      {
         get
         {
            IComparer<Decimal> startComparer = new AreaStartComparer(); // use the customize comparer for getting the right order
            var sortedAreas = Areas.OrderBy(c => c.Start, startComparer);
            Node<IArea<T>> ll = makeLinkedList(sortedAreas.ToArray());

            List<IArea<T>> explArea = new List<IArea<T>>();
            while (ll.next != null)
            {
               while (ll.next != null && ll.data.End <= ll.next.data.Start)
               {
                  explArea.Add(ll.data);
                  ll = ll.next;
               }
               if (ll.next == null)
               {
                  explArea.Add(ll.data);
                  return explArea;
               }
               else
               {
                  if (ll.data.Start < ll.next.data.Start)
                  {
                     explArea.Add(new Area<T>(ll.data.Id, ll.data.Start, ll.next.data.Start));
                  }
                  if (ll.next.data.End < ll.data.End)
                  {
                     Area<T> a = new Area<T>(ll.data.Id, ll.next.data.End, ll.data.End);
                     Node<IArea<T>> inode = ll;
                     while (inode.next != null && inode.next.data.End <= a.Start)
                        inode = inode.next;
                     inode.next = new Node<IArea<T>>(a, inode.next);

                  }
                  ll = ll.next;
               }
            }
            return explArea;
         }
      }

      /// <summary>
      /// Adds a destination to the destination set.
      /// </summary>
      /// <param name="d">Destination to add</param>
      public override void AddDestination(Destination<T> d)
      {
         UpdateDestination(d.Id, d.ToString());
      }

      /// <summary>
      /// Adds an area to a destination, or create a new destination if it does not exists
      /// </summary>
      /// <param name="id">The id of the destination to update</param>
      /// <param name="code">The area code to add</param>
      public override void AddArea(T id, ulong code)
      {
         if (_uniqueAreaCodes.ContainsKey(code))
         {
            if (_uniqueAreaCodes[code].Equals(UndefinedDestinationId))
               RemoveArea(code);
            else
               throw new InvalidOperationException(String.Format(Properties.Resources.msgAreaCodeAlreadyExistsFormatted, id, code, _uniqueAreaCodes[code]));
         }

         if (!_implicitDests.ContainsKey(id))
            _implicitDests.Add(id, new Destination<T>(id));
         _implicitDests[id].AddArea(code);
         _uniqueAreaCodes.Add(code, id);
         _areasNeedsRebuild = true;
      }


      /// <summary>
      /// Removes an area from a destination if it exists
      /// </summary>
      /// <param name="code">The area code to add</param>
      public override void RemoveArea(ulong code)
      {
         if (!_uniqueAreaCodes.ContainsKey(code))
            throw new InvalidOperationException(Properties.Resources.msgAreaDoesNotExist);
         _implicitDests[_uniqueAreaCodes[code]].RemoveArea(code);
         if (_implicitDests[_uniqueAreaCodes[code]].IsEmpty)
            _implicitDests.Remove(_uniqueAreaCodes[code]);
         _uniqueAreaCodes.Remove(code);

         _areasNeedsRebuild = true;
      }

      /// <summary>
      /// Update the defintion of a destination, replacing the current definition. Create a new destination if it does not exists.
      /// </summary>
      /// <param name="id">The id of the destination to update</param>
      /// <param name="areas">The area codes to add, eg "33", ""330;3323-3326"</param>
      public override void UpdateDestination(T id, string areas)
      {
         if (!_implicitDests.ContainsKey(id))
            _implicitDests.Add(id, new Destination<T>(id));
         else
         {   // We have to keep the unique area store in synch
            foreach (ulong ea in _implicitDests[id].ImplicitAreas)
               _uniqueAreaCodes.Remove(ea);
         }
         _implicitDests[id].Clear();
         foreach (ulong a in getSingleAreaCodes(areas))
         {
            if (_uniqueAreaCodes.ContainsKey(a))
            {
               // undef area codes added silently by the system should not throw the exception
               if (_uniqueAreaCodes[a].Equals(UndefinedDestinationId))
               {
                  _uniqueAreaCodes.Remove(a);
               }
               else
               {
                  throw new InvalidOperationException(Properties.Resources.msgAreaCodeAlreadyExists);
               }
            }
            _implicitDests[id].AddArea(a);
            _uniqueAreaCodes.Add(a, id);
         }


         _areasNeedsRebuild = true;
      }

      /// <summary>
      /// Get the single area codes in a string representation
      /// </summary>
      /// <param name="areas">The string representing the ranges eg "33,334-336"</param>
      /// <returns>A list of individual area codes, eg 33,334,335,336.</returns>
      private List<ulong> getSingleAreaCodes(string areas)
      {
         ulong[][] areaRanges = GetAreasAndRanges(areas, Sep, Del);
         List<ulong> singleCodes = new List<ulong>();
         ulong[][] areamatrix = GetAreasAndRanges(areas, Sep, Del);
         foreach (ulong[] area in areamatrix)
         {
            if (area.Length == 1)
               singleCodes.Add(area[0]);
            else if (area.Length == 2)
               for (ulong i = area[0]; i <= area[1]; i++)
                  singleCodes.Add(i);
            else
               throw new ArgumentException(Properties.Resources.msgInvalidRangeSpecified);
         }

         return singleCodes;
      }

      /// <summary>
      /// Gets the list of destinations in the destination set in implicit format (overlapping area codes are allowed)
      /// </summary>
      public override Dictionary<T, Destination<T>> Destinations { get { return _implicitDests; } }

      /// <summary>
      /// Populates internal collection for explicit destinations.
      /// </summary>
      private void PopulateExplicitDestinations()
      {
         _areas.Clear();
         foreach (Destination<T> d in _implicitDests.Values)
         {
            _areas.AddRange(d.Areas.Select(x => (Area<T>)x));
         }
         _areasNeedsRebuild = false;
      }

      /// <summary>
      /// Gets the list of destination in the destination set in explicit format
      /// </summary>
      public Dictionary<T, Destination<T>> ExplicitDestinations
      {
         get
         {
            IEnumerable<IArea<T>> expl = ExplicitAreas;
            Dictionary<T, Destination<T>> dests = new Dictionary<T, Destination<T>>();
            foreach (IArea<T> a in expl)
            {
               if (!dests.ContainsKey(a.Id))
                  dests[a.Id] = new Destination<T>(a.Id);
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
      public DestinationSetBase.CMappingResult<T> MapToDestinationSet(DestinationSet<T> b)
      {
         IEnumerable<IArea<T>> a_expl = this.ExplicitAreas;
         IEnumerable<IArea<T>> b_expl = b.ExplicitAreas;

         int dbi = 0;
         int dai = 0;
         IArea<T> ra, rb;
         DestinationSetBase.CMappingResult<T> mapRes = new DestinationSetBase.CMappingResult<T>();

         while (dai < a_expl.Count() && dbi < b_expl.Count())
         {
            ra = a_expl.ElementAt(dai);
            rb = b_expl.ElementAt(dbi);
            if (rb.End <= ra.Start)
               dbi++;
            else
            {
               if (rb.Start < ra.End)
               {
                  mapRes.AddMap(new MappedAreaRange<T>(ra.Id, rb.Id, Math.Max(ra.Start, rb.Start), Math.Min(ra.End, rb.End)));
               }
               if (rb.End < ra.End)
                  dbi++;
               else
                  dai++;
            }
         }
         return mapRes;
      }

      /// <summary>
      /// Finds a single area code. Return the first matching destination in case the area is too short
      /// </summary>
      /// <param name="code">The area to find</param>
      /// <returns>The ID of the destination, or undefined</returns>
      public T FindFirstMatchingDestination(ulong code)
      {
         Decimal a = Convert.ToDecimal(Global.Dot + code.ToString(), CultureInfo.InvariantCulture.NumberFormat);
         IArea<T> aRange = ExplicitAreas.FirstOrDefault(c => c.Start <= a && c.End > a);
         return aRange.Id;
      }

      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="code">Area to find in the destination set</param>
      /// <returns>Destination name, if area falls within the destination set, <value>empty string</value> otherwise</returns>
      public override T FindArea(ulong code)
      {
         return FindArea(new Area<T>((T)Utility<T>.Default(), code));
      }

      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="a">Area to find in the destination set</param>
      /// <returns>Destination name, if area falls within the destination set, <value>null</value> otherwise</returns>
      protected override T FindArea(ZeroOneAreaRange a)
      {
         IArea<T> aRange = ExplicitAreas.FirstOrDefault(c => c.Start <= a.Start && c.End >= a.End);
         if (aRange != null)
            return aRange.Id;
         else
            return default(T);
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
         ulong[][] areamatrix = GetAreasAndRanges(area, Sep, Del);
         if (areamatrix.Length != 1 || areamatrix[0].Length > 2)
            throw new FormatException(String.Format(Properties.Resources.msgUnableToParseAreaStringFormatted, area));
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
         T[] FindDestinations = (from dest in _implicitDests where dest.Value.Areas.Any(c => a.InRange(c, false)) select dest.Key).ToArray();
         return FindDestinations;
      }
   }
}
