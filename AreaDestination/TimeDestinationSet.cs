using System;
using System.Collections.Generic;

namespace AreaDestination
{
   using System.Linq;

   using DatesSet = HashSet<DateTime>;
   using ValidityPeriod = KeyValuePair<bool, DateRange>;

   /// <summary>
   /// Class representing a destination set with the extended time concept, as an abstraction of destination set with the time dimension, i.e. time destination set is a set of destination sets across time.
   /// One destination is defined by a set of unique area codes. Preserves all the destinations implicit representation.
   /// </summary>
   public partial class TimeDestinationSet<T> where T : IComparable
   {
      /// <summary>
      /// Class defining a destination validity range. Day is the least important date value.
      /// </summary>
      internal class DestinationValidity : DateRange, ICloneable
      {
         public bool Valid { get; private set; }

         /// <summary>
         /// Creates a new destination validity range.
         /// </summary>
         /// <param name="isValid">Is valid</param>
         /// <param name="validFrom">Valid from</param>
         /// <param name="validUntil">Valid until</param>
         public DestinationValidity(bool isValid, DateTime validFrom, DateTime validUntil)
            : base(validFrom, validUntil)
         {
            Valid = isValid;
         }

         public override object Clone()
         {
            return new DestinationValidity(Valid, Start, End);
         }

         public override string ToString()
         {
            return Valid.ToString() + ": " + base.ToString();
         }
      }

      private Dictionary<T, TimeDestination> _timeDestinations = new Dictionary<T, TimeDestination>();

      /// <summary>
      /// Gets the destination at a given time.
      /// </summary>
      /// <param name="id">Destination id</param>
      /// <param name="time">Time</param>
      /// <returns>Destination at a given time</returns>
      public Destination<T> GetDestinationAtDate(T id, DateTime time)
      {
         if (_timeDestinations.ContainsKey(id))
            return _timeDestinations[id].GetDestinationAtDate(time);
         return null;
      }

      /// <summary>
      /// Gets the destination history by destination id as a list of validity periods,
      /// containing flag indicating whether period is valid and the time interval [valid from - valid until].
      /// </summary>
      /// <param name="id">Destination id to get destination history</param>
      /// <returns>Validity period if <paramref name="id">destination</paramref> exists, <value>null</value> otherwise</returns>
      public List<ValidityPeriod> GetDestinationHistory(T id)
      {
         if (!_timeDestinations.ContainsKey(id))
         {
            return null;
         }
         else
         {
            List<DestinationValidity> validityPeriods = _timeDestinations[id].GetDestinationHistory();
            return (from destV in validityPeriods select new ValidityPeriod(destV.Valid, new DateRange(destV.Start, destV.End))).ToList();
         }
         
      }

      /// <summary>
      /// Adds an area to the time destination set.
      /// </summary>
      /// <param name="id">Area id</param>
      /// <param name="code">Area code</param>
      /// <param name="vf">Valid from</param>
      /// <param name="vu">Valid until</param>
      public void AddArea(T id, ulong code, DateTime vf, DateTime vu)
      {
         if (!_timeDestinations.ContainsKey(id))
            _timeDestinations.Add(id, new TimeDestination(id));
         _timeDestinations[id].AddArea(code, vf, vu);
      }

      /// <summary>
      /// Gets a destination set that is corresponding to a picture of the time destination set at a given date.
      /// </summary>
      /// <param name="t">Time</param>
      /// <returns>Destination set at the passed time</returns>
      public DestinationSet<T> GetDestinationSetAtDate(DateTime t)
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Gets the mapping at date for the destination set against another destination set.
      /// </summary>
      /// <param name="time">Date for the mapping</param>
      /// <param name="toTDS">Time destination set to map to</param>
      /// <returns>Mapping at date for the destination set against another destination set</returns>
      public DestinationSetBase.MappingResult<T> MapToDestinationSetAtDate(DateTime time, TimeDestinationSet<T> toTDS)
      {
         DestinationSet<T> from = GetDestinationSetAtDate(time);
         DestinationSet<T> to = toTDS.GetDestinationSetAtDate(time);
         return from.MapToDestinationSet(to);
      }

      /// <summary>
      /// Maps the destination set to another destination set.
      /// </summary>
      /// <param name="toTDS">Destination set to map against</param>
      /// <param name="dtDate">Time</param>
      /// <returns>Result of the mapping between the two destination set at the given time</returns>
      public List<KeyValuePair<DateTime, DestinationSetBase.MappingResult<T>>> MapToDestinationSet(TimeDestinationSet<T> toTDS, DateTime dtDate)
      {

         DatesSet uniquePointInTime = new DatesSet();
         foreach (TimeDestination td in _timeDestinations.Values)
            foreach (DateTime dt in td.DestinationDateChanges)
               uniquePointInTime.Add(dt);
         foreach (TimeDestination td in toTDS._timeDestinations.Values)
            foreach (DateTime dt in td.DestinationDateChanges)
               uniquePointInTime.Add(dt);
         List<KeyValuePair<DateTime, DestinationSetBase.MappingResult<T>>> res = new List<KeyValuePair<DateTime, DestinationSetBase.MappingResult<T>>>();
         foreach (DateTime dt in uniquePointInTime.ToList().OrderBy(c => c))
            res.Add(new KeyValuePair<DateTime, DestinationSetBase.MappingResult<T>>(dt, MapToDestinationSetAtDate(dt, toTDS)));
         return res;

      }

      #region range collection manipulator

      /// <summary>
      /// Range collection manipulator.
      /// </summary>
      public static class RangeCollectionManipulator<T2> where T2 : Range<DateTime>, ICloneable
      {
         private class Node<K>
         {
            public Node<K> next;
            public K data;

            public Node(K a)
            {
               data = a;
               next = null;
            }

            public Node(K a, Node<K> n)
            {
               data = a;
               next = n;
            }
         }

         private static Node<T2> makeLinkedList(IEnumerable<T2> e)
         {
            Node<T2> head = null, cur = null, n;

            foreach (T2 a in e)
            {
               n = new Node<T2>(a);
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

         /// <summary>
         /// Splits ranges.
         /// </summary>
         /// <param name="ranges">Ranges to split</param>
         /// <returns>Split ranges</returns>
         public static List<T2> SplitRanges(IEnumerable<T2> ranges)
         {
            var sortedAreas = ranges.OrderBy(c => c.Start);
            Node<T2> ll = makeLinkedList(sortedAreas);

            List<T2> explArea = new List<T2>();
            while (ll.next != null)
            {
               if (ll.data.End.CompareTo(ll.next.data.Start) <= 0)
               {
                  explArea.Add(ll.data);
               }
               else
               {
                  if (ll.data.Start.CompareTo(ll.next.data.Start) < 0)
                  {
                     T2 newRange = (T2)ll.data.Clone();
                     newRange.End = ll.next.data.Start;
                     explArea.Add(newRange);
                  }
                  if (ll.next.data.End.CompareTo(ll.data.End) < 0)
                  {
                     T2 a = (T2)ll.data.Clone();
                     a.Start = ll.next.data.End;
                     Node<T2> inode = ll;
                     while (inode.next != null && inode.next.data.End.CompareTo(a.Start) <= 0)
                        inode = inode.next;
                     inode.next = new Node<T2>(a, inode.next);
                  }
               }
               ll = ll.next;
            }
            explArea.Add(ll.data);
            return explArea;
         }

         /// <summary>
         /// Coalesces ranges.
         /// </summary>
         /// <param name="ranges">Ranges to coalesce</param>
         /// <returns>Coalesces ranges</returns>
         public static List<T2> CoalesceRanges(IEnumerable<T2> ranges)
         {
            var sortedAreas = ranges.OrderBy(c => c.Start);
            Node<T2> ll = makeLinkedList(sortedAreas);
            Node<T2> head = ll;

            List<T2> explArea = new List<T2>();
            while (ll.next != null)
            {
               while (ll.next != null && ll.data.End.CompareTo(ll.next.data.Start) < 0)
               {
                  explArea.Add(ll.data);
                  ll = ll.next;
               }
               if (ll.next != null)
               {
                  if (ll.data.End.CompareTo(ll.next.data.Start) == 0)
                  {
                     ll.data.End = ll.next.data.End;
                     ll.next = ll.next.next;
                  }
                  else if (ll.data.Start.CompareTo(ll.next.data.Start) < 0)
                  {
                     ll.data.End = ll.data.End.CompareTo(ll.next.data.End) > 0 ? ll.data.End : ll.next.data.End;
                     ll.next = ll.next.next;
                  }
               }
            }
            ll = head;
            while (ll != null)
            {
               explArea.Add(ll.data);
               ll = ll.next;
            }
            return explArea;
         }
      }
      #endregion
   }
}
