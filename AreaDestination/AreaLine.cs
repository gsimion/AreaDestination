using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Text;

namespace AreaDestination
{

   /// <summary>
   /// Object implementing the abstraction of area collection represented as line segments.
   /// Contains the logic for aggregating areas belonging to the same segment.
   /// Segments are stored within a 1-dimensional segment [0,1] as [a,b), where a, b in [0,1]
   /// </summary>
   /// <typeparam name="T">Type of area ID</typeparam>
   public class AreaLine<T> where T : IComparable
   {
      /// <summary>
      /// Sorted set containing the segments, not overlapping each other and ordered by start of range.
      /// </summary>
      private readonly SortedSet<CZeroOneSegment> _segments = new SortedSet<CZeroOneSegment>();

      /// <summary>
      /// Stores the min and max value of the covered segments.
      /// </summary>
      protected ZeroOneAreaRange _coveredRange = new ZeroOneAreaRange();

      /// <summary>
      /// Gets the covered range of area line.
      /// </summary>
      public IZeroOneAreaRange CoveredRange { get { return _coveredRange; } }

      /// <summary>
      /// Creates a new area line, given a list of areas.
      /// </summary>
      /// <param name="areas">List of areas</param>
      public AreaLine(IEnumerable<Area<T>> areas) : this()
      {
         foreach (IArea<T> area in areas)
            Add(area);
      }

      /// <summary>
      /// Creates an empty area line.
      /// </summary>
      public AreaLine()
      {
         _coveredRange.Start = 1;
         _coveredRange.End = 0;
      }

      /// <summary>
      /// Gets the count of the generic segments present in the area line.
      /// </summary>
      protected int Count { get { return _segments.Count; } }

      /// <summary>
      /// Gets whether the area line contains segments.
      /// </summary>
      protected bool HasSegments { get { return _segments.Any(); } }

      /// <summary>
      /// Clears any segment present in the area line.
      /// </summary>
      public void Clear()
      {
         _segments.Clear();
         _coveredRange.Start = 1;
         _coveredRange.End = 0;
      }

      /// <summary>
      /// Detects whether an area is contained by the area line.
      /// </summary>
      /// <param name="a">Area candidate to be contained by area line</param>
      /// <returns><value>True</value> if it is contained, <value>False</value> otherwise</returns>
      public bool Contains(IArea<T> a)
      {
         if ((a.End >= _coveredRange.Start) && (a.Start <= _coveredRange.End) && (_segments.Any(s => a.Start >= s.Start && a.End <= s.End)))
            return true;
         else
            return false;
      }

      /// <summary>
      /// Adds an area to the area line.
      /// </summary>
      /// <param name="a">Area to add</param>
      public void Add(IArea<T> a)
      {
         Add(a.Start, a.End);
      }

      /// <summary>
      /// Adds a segment [a,b) to the area line.
      /// </summary>
      /// <param name="start">Start (included)</param>
      /// <param name="end">End (excluded)</param>
      public void Add(Decimal start, Decimal end)
      {
         CZeroOneSegment segmentToAdd = new CZeroOneSegment(start, end);
         // check if outside boundaries for avoiding consuming checks
         if (_coveredRange.Start < _coveredRange.End && (_coveredRange.Start <= end || _coveredRange.End >= start))
         {
            // check already included
            if (_segments.Any(x => ((x.Start <= start) && (x.End >= end))))
               return;

            // check if new interval includes already existing intervals
            if (_segments.Any(x => ((x.Start > start) && (x.End < end))))
            {
               DeleteAllSegmentWithinRange(start, end);
               Add(start, end);
               return;
            }

            // check start: is new interval starting within a existing segment?
            CZeroOneSegment startSegment = _segments.Where(x => ((x.Start <= start) && (x.End >= start))).FirstOrDefault();
            if ((startSegment != null) && (startSegment.End < end))
            {
               DeleteAllSegmentWithinRange(startSegment.End, end);
               CZeroOneSegment merge = (from seg in _segments where seg.StartsWith(end) select seg).FirstOrDefault();
               if (merge != null)
               {
                  end = merge.End;
                  _segments.Remove(merge);
               }
               UpdateStart(startSegment.End, end);
               startSegment.End = end;
               return;
            }
            // check end: is new interval ending within an existing segment?
            CZeroOneSegment endSegment = _segments.Where(x => ((x.Start <= end) && (x.End >= end))).FirstOrDefault();
            if ((endSegment != null) && endSegment.Start > start)
            {
               DeleteAllSegmentWithinRange(start, endSegment.Start);
               CZeroOneSegment merge = (from seg in _segments where seg.EndsWith(start) select seg).FirstOrDefault();
               if (merge != null)
               {
                  start = merge.Start;
                  _segments.Remove(merge);
               }
               ResizeEnd(endSegment.Start, start);
               endSegment.Start = start;
               return;
            }
         }
         // updates min and max values
         _segments.Add(segmentToAdd);
         if (segmentToAdd.Start < _coveredRange.Start)
            _coveredRange.Start = segmentToAdd.Start;
         if (segmentToAdd.End > _coveredRange.End)
            _coveredRange.End = segmentToAdd.End;
      }

      /// <summary>
      /// Removes an area to the area line.
      /// </summary>
      /// <param name="area">Area to remove</param>
      public void Remove(IArea<T> area)
      {
         Remove(area.Start, area.End);
      }

      /// <summary>
      /// Removes a segment [a,b) to the area line.
      /// </summary>
      /// <param name="start">Start (included)</param>
      /// <param name="end">End (excluded)</param>
      public void Remove(Decimal start, Decimal end)
      {
         // optimize and update min and max values 
         if ((end < _coveredRange.Start) || (start > _coveredRange.End))
            return;

         if (end > _coveredRange.End) 
            _coveredRange.End = end;
         if (start < _coveredRange.Start) 
            _coveredRange.Start = start;

         // delete all the sub segments included in the removal interval
         DeleteAllSegmentWithinRange(start, end);
         
         // any segment need to be split?
         CZeroOneSegment segmentToSplit = (from s in _segments where s.Start < start && s.End > end select s).FirstOrDefault();
         if (segmentToSplit != null)
         {
            CZeroOneSegment First = new CZeroOneSegment(segmentToSplit.Start, start);
            CZeroOneSegment Second = new CZeroOneSegment(end, segmentToSplit.End);
            _segments.Remove(segmentToSplit);
            _segments.Add(First);
            _segments.Add(Second);
            return;
         }

         // resize start
         while (true)
         {
            CZeroOneSegment segment = (from s in _segments where s.Start >= start && s.Start < end select s).FirstOrDefault();
            if (segment == null)
               break;
            segment.Start = end;
         }
         // resize end
         while (true)
         {
            CZeroOneSegment segment = (from s in _segments where s.End > start && s.End <= end select s).FirstOrDefault();
            if (segment == null)
               break;
            segment.End = start;
         }
      }

      /// <summary>
      /// Deletes all segments in the area line included in a given range.
      /// </summary>
      /// <param name="start">Start</param>
      /// <param name="end">End</param>
      private void DeleteAllSegmentWithinRange(Decimal start, Decimal end)
      {
         while (true)
         {
            CZeroOneSegment segment = (from s in _segments where s.Start >= start && s.End <= end select s).FirstOrDefault();
            if (segment == null)
               return;
            _segments.Remove(segment);
         }
      }

      /// <summary>
      /// Resizes the end of an existing segment.
      /// </summary>
      /// <param name="prevEnd">Previus end</param>
      /// <param name="newEnd">End to resize to</param>
      private void ResizeEnd(Decimal prevEnd, Decimal newEnd)
      {
         if (prevEnd < newEnd)
            throw new ArgumentException(Properties.Resources.msgUpdateNewEnd);
         CZeroOneSegment endSegment = _segments.Where(x => ((x.End <= prevEnd) && (x.End >= newEnd))).FirstOrDefault();
         if (endSegment != null)
            endSegment.End = newEnd;
      }

      /// <summary>
      /// Resizes the start of an existing segment.
      /// </summary>
      /// <param name="prevStart">Previus start</param>
      /// <param name="newStart">Start to resize to</param>
      private void UpdateStart(Decimal prevStart, Decimal newStart)
      {
         if (prevStart > newStart)
            throw new ArgumentException(Properties.Resources.msgUpdateNewStart);
         CZeroOneSegment endSegment = _segments.Where(x => ((x.Start >= prevStart) && (x.Start <= newStart))).FirstOrDefault();
         if (endSegment != null)
            endSegment.Start = newStart;
      }

      /// <summary>
      /// Gets a collection of ranges contained by the area line.
      /// </summary>
      /// <returns>Collection of non overlapping ranges, ordered by start</returns>
      public IEnumerable<ZeroOneDecimalRange> GetRanges()
      {
         List<ZeroOneDecimalRange> ranges = (from CZeroOneSegment segment in _segments select new ZeroOneDecimalRange(segment.Start, segment.End)).ToList();
         return ranges;
      }

      /// <summary>
      /// Gets a collection of explicit areas contained in the current instance of area line.
      /// </summary>
      /// <returns>Collection of explicit areas</returns>
      protected IEnumerable<IArea<T>> GetExplicitAreas()
      {
         List<IArea<T>> areas = new List<IArea<T>>();
         foreach (CZeroOneSegment segment in _segments)
         {
            foreach (CZeroOneSegment explodedsegment in ExplodeSegment(segment))
               areas.Add(new Area<T>((T)Utility<T>.Default(), explodedsegment.Start, explodedsegment.End ));                   
         }
         return areas.OrderBy(x => x.Start);
      }

      /// <summary>
      /// Explodes a segment in all the sub segments by their precision.
      /// </summary>
      /// <param name="segment">Segment to be exploded</param>
      /// <returns>Collection of exploded segments</returns>
      private static IEnumerable<CZeroOneSegment> ExplodeSegment(CZeroOneSegment segment)
      {
         IList<CZeroOneSegment> list = new List<CZeroOneSegment>();
         Explode(segment.Start, segment.End, list);
         return list;
      }

      /// <summary>
      /// Recursive call to populate the list with explicit segments from a generic segment [a, b).
      /// Generic case: [a, b) => [a, b1), [b1, b2), ..., [bn, b), where b > b1, ..., bn > a
      /// Example 1: [0.1, 0.234) => [0.1, 0.2), [0.2, 0.23), [0.23, 0.234)
      /// Example 2: [0.123, 0.23) => [0.123, 0.13), [0.13, 0.2), [0.2, 0.23)
      /// </summary>
      /// <param name="start">Start of generic [0,1] segment</param>
      /// <param name="end">End of generic [0,1] segment</param>
      /// <param name="list">List with explicit segments</param>
      private static void Explode(Decimal start, Decimal end, IList<CZeroOneSegment> list)
      {
         if (start >= end)
            return;

         string sStart = start.ToString(Area<T>.DecimalFormat).Substring(2);
         string sEnd = end.ToString(Area<T>.DecimalFormat).Substring(2);

         int commPrecision = 0;
         while (sStart.Length > commPrecision && sEnd.Length > commPrecision && sStart[commPrecision] == sEnd[commPrecision])
            commPrecision++;

         Decimal newEnd = Decimal.Zero;
         if (Math.Abs(commPrecision - sStart.Length) <= 1)
         {
            newEnd = (end != 1) ? Convert.ToDecimal(Global.Dot + sEnd.Substring(0, commPrecision + 1), CultureInfo.InvariantCulture.NumberFormat) : end;
            if (newEnd == start) newEnd = end;
         }
         else
         {
            newEnd = Convert.ToDecimal(Global.Dot + sStart.Substring(0, sStart.Length - 1), CultureInfo.InvariantCulture.NumberFormat);
            newEnd += (newEnd != 0) ? CZeroOneSegment.GetUnit(newEnd) : 0.1m;
         }
         list.Add(new CZeroOneSegment(start, newEnd)); // add [a, b1)
         Explode(newEnd, end, list); // recursive call for [b1, b), where a < b1 < b
      }

      /// <summary>
      /// Gets whether an area line is overlapping with the current instance of area line.
      /// </summary>
      /// <param name="arealine">Area line to check for overlaps</param>
      /// <returns><value>True</value> if it is overlapping, <value>False</value> otherwise</returns>
      public Boolean IsOverlapping(AreaLine<T> arealine)
      {
         foreach (CZeroOneSegment segment in arealine._segments)
            if (_segments.Any(x => (x.Start <= segment.Start && x.End > segment.Start)) || _segments.Any(x => (x.End >= segment.End && x.Start < segment.End)))
               return true;
         return false;
      }

      /// <summary>
      /// Merges the current instance of area line with another area line.
      /// Adds all the segment of the area line to be merged.
      /// </summary>
      /// <param name="arealine">Area line to be merged</param>
      public void Merge(AreaLine<T> arealine)
      {
         foreach (CZeroOneSegment segment in arealine._segments)
            Add(segment.Start, segment.End);
      }

      /// <summary>
      /// Performs the union of two area lines as the aggregation of all the segment contained by each one of them.
      /// </summary>
      /// <param name="al">Area line to union with</param>
      /// <returns>New area line representing the union of the instance of area line and the area line passed as input</returns>
      public AreaLine<T> Union(AreaLine<T> al)
      {
         AreaLine<T> areaLine = new AreaLine<T>((from CZeroOneSegment s in _segments select (new Area<T>((T)Utility<T>.Default(), s.Start, s.End))));
         foreach (Area<T> area in (from CZeroOneSegment s in al._segments select new Area<T>((T)Utility<T>.Default(), s.Start, s.End)))
            areaLine.Add(area);
         return areaLine;
      }

      /// <summary>
      /// Performs the intersection of two area lines as the aggregation of all the segment contained by each one of them.
      /// </summary>
      /// <param name="al">Area line to intersect with</param>
      /// <returns>New area line representing the intersection of the instance of area line and the area line passed as input</returns>
      public AreaLine<T> Intersect(AreaLine<T> al)
      {
         AreaLine<T> areaLine = new AreaLine<T>();
         foreach (CZeroOneSegment parent in _segments)
         {
            foreach (CZeroOneSegment child in al._segments.Where(x => x.Start >= parent.Start))
            {
               if (child.Start >= parent.End)
                  break;
               Decimal min = (child.Start > parent.Start) ? child.Start : parent.Start;
               Decimal max = (child.End < parent.End) ? child.End : parent.End;
               CZeroOneSegment intersection = new CZeroOneSegment(min, max);
               areaLine._segments.Add(intersection);
               if (max == parent.End)
                  break;
            }
         }
         return areaLine;
      }

      /// <summary>
      /// Gets the string representation of the area line as "[a1, b1); [a2, b2), ...".
      /// </summary>
      /// <returns>String representation of the area line as "[a1, b1); [a2, b2), ..."</returns>
      public override string ToString()
      {
         if (_segments.Count == 0)
            return String.Empty;
         else if (_segments.Count == 1)
            return _segments.First().ToString();
         else
         {
            StringBuilder sb = new StringBuilder();
            string res = _segments.Aggregate(sb, (builder, area) => builder.Append(area.ToString()).Append("; ")).ToString().TrimEnd(new char[] { ';', ' ' });
            return res;
         }
      }

      /// <summary>
      /// Class defining a segment in [0,1].
      /// </summary>
      private class CZeroOneSegment : ZeroOneDecimalRange, IEqualityComparer<CZeroOneSegment>
      {
         private const string A = "a";
         private const string B = "b";
         private const string DefaultFormat = "[a, b)";

         /// <summary>
         /// Creates a new segment.
         /// </summary>
         /// <param name="start">Start</param>
         /// <param name="end">End</param>
         public CZeroOneSegment(Decimal start, Decimal end)
            : base(start, end)
         {
         }

         /// <summary>
         /// Clones a segment.
         /// </summary>
         /// <returns>Cloned segment</returns>
         public override object Clone()
         {
            return new CZeroOneSegment(Start, End);
         }

         /// <summary>
         /// Gets hash code.
         /// </summary>
         /// <param name="a">Segment</param>
         /// <returns>Hash code</returns>
         public int GetHashCode(CZeroOneSegment a)
         {
            return a.Start.GetHashCode();
         }

         /// <summary>
         /// Compares two segments.
         /// </summary>
         /// <param name="a">First segment</param>
         /// <param name="b">Second segment</param>
         /// <returns>Comparison on the start</returns>
         public bool Equals(CZeroOneSegment a, CZeroOneSegment b)
         {
            return ((a.Start == b.Start) && (a.End == b.End));
         }

         /// <summary>
         /// Determines whether the segment starts with a given decimal.
         /// </summary>
         /// <param name="value">Decimal value</param>
         public bool StartsWith(Decimal value)
         {
            return (Start == value);
         }

         /// <summary>
         /// Determines whether the segment ends with a given decimal.
         /// </summary>
         /// <param name="value">Decimal value</param>
         public bool EndsWith(Decimal value)
         {
            return (End == value);
         }

         /// <summary>
         /// Gets the string representation of the segment.
         /// </summary>
         /// <returns>1-dimensional segment representation as [a, b)</returns>
         public override string ToString()
         {
            return this.ToString(DefaultFormat);
         }

         /// <summary>
         /// Gets the string representation of the segment [a, b).
         /// Any format can be specified as far as 'a' and 'b' are defined.
         /// </summary>
         /// <param name="format">Format, containing 'a' and/or 'b'</param>
         /// <returns>1-dimensional segment representation of [a, b) with the specified format</returns>
         public string ToString(string format)
         {
            return format.Replace(A, Start.ToString()).Replace(B, End.ToString());
         }
      }

      /// <summary>
      /// Class implementing a generic ordered set.
      /// </summary>
      /// <typeparam name="T0">Type</typeparam>
      private class SortedSet<T0> : ICollection<T0>
      {
         private readonly IDictionary<T0, LinkedListNode<T0>> m_Dictionary;
         private readonly LinkedList<T0> m_LinkedList;

         /// <summary>
         /// Creates a new generic ordered set with the default object comparer.
         /// </summary>
         public SortedSet()
            : this(EqualityComparer<T0>.Default)
         {
         }

         /// <summary>
         /// Creates a new generic ordered set with a given comparer.
         /// </summary>
         /// <param name="comparer">Comparer</param>
         public SortedSet(IEqualityComparer<T0> comparer)
         {
            m_Dictionary = new Dictionary<T0, LinkedListNode<T0>>(comparer);
            m_LinkedList = new LinkedList<T0>();
         }

         /// <summary>
         /// Gets the number of objects contained by the ordered set.
         /// </summary>
         public int Count
         {
            get { return m_Dictionary.Count; }
         }

         /// <summary>
         /// Gets whether the collection is readonly.
         /// </summary>
         public virtual bool IsReadOnly { get { return m_Dictionary.IsReadOnly; } }

         /// <summary>
         /// Adds an object to the ordered set.
         /// </summary>
         /// <param name="item">Object to add</param>
         void ICollection<T0>.Add(T0 item)
         {
            Add(item);
         }

         /// <summary>
         /// Clears the collection.
         /// </summary>
         public void Clear()
         {
            m_LinkedList.Clear();
            m_Dictionary.Clear();
         }

         /// <summary>
         /// Removes an object to the ordered set.
         /// </summary>
         /// <param name="item">Object to remove</param>
         public bool Remove(T0 item)
         {
            LinkedListNode<T0> node;
            bool bFound = m_Dictionary.TryGetValue(item, out node);
            if (!bFound) return false;
            m_Dictionary.Remove(item);
            m_LinkedList.Remove(node);
            return true;
         }

         /// <summary>
         /// Gets the enumerator.
         /// </summary>
         public IEnumerator<T0> GetEnumerator()
         {
            return m_LinkedList.GetEnumerator();
         }

         /// <summary>
         /// Gets the enumerator.
         /// </summary>
         IEnumerator IEnumerable.GetEnumerator()
         {
            return GetEnumerator();
         }

         /// <summary>
         /// Check whether the collection contains the generic key.
         /// </summary>
         public bool Contains(T0 item)
         {
            return m_Dictionary.ContainsKey(item);
         }

         /// <summary>
         /// Implementation of collection copy to method.
         /// </summary>
         /// <param name="array">Array</param>
         /// <param name="iArrayIdx">Array index</param>
         public void CopyTo(T0[] array, int iArrayIdx)
         {
            m_LinkedList.CopyTo(array, iArrayIdx);
         }

         /// <summary>
         /// Adds an object to the ordered set.
         /// </summary>
         /// <param name="item">Object to add</param>
         public bool Add(T0 item)
         {
            if (m_Dictionary.ContainsKey(item)) 
               return false;
            LinkedListNode<T0> node = m_LinkedList.AddLast(item);
            m_Dictionary.Add(item, node);
            return true;
         }
      }
   }
}
