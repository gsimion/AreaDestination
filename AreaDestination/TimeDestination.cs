using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreaDestination
{
   using DatesSet = HashSet<DateTime>;

   /// <summary>
   /// Class representing a destination set with the extended time concept, as an abstraction of destination set with the time dimension, i.e. time destination set is a set of destination sets across time.
   /// One destination is defined by a set of unique area codes. Preserves all the destinations implicit representation.
   /// </summary>
   /// <typeparam name="T">Type of destination ID</typeparam>
   public partial class TimeDestinationSet<T> where T : IComparable
   {

      /// <summary>
      /// Internal time destination, implementing destination interface but not inheriting the destination, since time dimension is added and any comparison operand is not applicable.
      /// </summary>
      internal class TimeDestination : IDestination<T>
      {
         /// <summary>
         /// Destination Id.
         /// </summary>
         public T Id { get; private set; }

         private readonly Dictionary<ulong, List<TimeArea>> _timeAreas = new Dictionary<ulong, List<TimeArea>>();

         /// <summary>
         /// Creates a new empty time destination.
         /// </summary>
         /// <param name="id">Time destination id</param>
         internal TimeDestination(T id)
         {
            Id = id;
         }

         /// <summary>
         /// Gets whether the destination contains any area.
         /// </summary>
         public bool IsEmpty { get { return !_timeAreas.Any(); } }

         /// <summary>
         /// Gets the list of single codes belonging to the destination instance.
         /// </summary>
         public IEnumerable<ulong> SingleCodes { get { return _timeAreas.Select(x => x.Key); } }

         /// <summary>
         /// Gets the list of all areas belonging to the time destination instance, anytime.
         /// </summary>
         public IEnumerable<IArea<T>> Areas { get { return _timeAreas.Keys.Select(x => (IArea<T>)(new Area<T>(Id, x))); } }

         /// <summary>
         /// Adds an area code to the destination.
         /// </summary>
         /// <param name="code">Area code to add</param>
         internal void AddArea(ulong code, DateTime validFrom, DateTime validUntil)
         {
            if (!_timeAreas.ContainsKey(code))
            {
               _timeAreas.Add(code, new List<TimeArea>());
               _timeAreas[code].Add(new TimeArea(code, validFrom, validUntil));
               return;
            }
            var overlapPeriod = from c in _timeAreas[code]
                                where c.Start <= validUntil.Date && c.End >= validFrom.Date
                                select c;
            if (overlapPeriod.Count() > 0)
            {
               TimeArea ta = overlapPeriod.First();
               ta.MergePeriod(validFrom, validUntil);
            }
            else
            {
               TimeArea ta = new TimeArea(code, validFrom, validUntil);
               _timeAreas.Add(code, new List<TimeArea>());
               _timeAreas[code].Add(ta);
            }
         }

         public List<DateTime> DestinationDateChanges
         {
            get
            {
               DatesSet uniquePointInTime = new DatesSet();
               foreach (List<TimeArea> lta in _timeAreas.Values)
               {
                  foreach (TimeArea t in lta)
                  {
                     uniquePointInTime.Add(t.Start);
                     uniquePointInTime.Add(t.End);
                  }
               }
               List<DateTime> res = uniquePointInTime.ToList();
               //res.Sort();
               return res;
            }
         }

         public Destination<T> GetDestinationAtDate(DateTime d)
         {
            Destination<T> dest = new Destination<T>(Id);
            foreach (List<TimeArea> lta in _timeAreas.Values)
            {
               var match = lta.Where(c => c.Start <= d.Date && c.End > d.Date);
               if (match.Count() > 0)
                  dest.AddArea(match.First().Area);
            }

            return dest;
         }

         public List<DestinationValidity> GetDestinationHistory()
         {
            List<DestinationValidity> allPeriods = new List<DestinationValidity>();

            foreach (var lta in _timeAreas.Values)
               foreach (TimeArea t in lta)
                  allPeriods.Add(new DestinationValidity(true, t.Start, t.End));

            var validRanges = RangeCollectionManipulator<DestinationValidity>.CoalesceRanges(allPeriods);
            validRanges.Add(new DestinationValidity(false, DateTime.MinValue, DateTime.MaxValue));

            return RangeCollectionManipulator<DestinationValidity>.SplitRanges(validRanges);
         }

         /// <summary>
         /// Class representing a time area as explicit area code and validity period.
         /// </summary>
         internal class TimeArea : DateRange, IComparable
         {
            public string Id { get; set; }
            public ulong Area { get; private set; }

            public TimeArea(UInt64 area, DateTime vf, DateTime vu)
               : base(vf, vu)
            {
               Area = area;
            }

            private static void validatePeriod(DateTime vf, DateTime vu)
            {
               if (vf > vu)
                  throw new ArgumentException(Properties.Resources.msgEndValueMustBeGreaterThanStart);
            }

            public void MergePeriod(DateTime vf, DateTime vu)
            {
               validatePeriod(vf, vu);
               Start = vf.Date >= Start ? Start : vf.Date; //Min
               End = vu.Date >= End ? vu.Date : End; //Max

            }

            public int CompareTo(object obj)
            {
               if (!(obj is TimeArea))
                  throw new ArgumentException();
               TimeArea b = obj as TimeArea;
               return b.Start.CompareTo(Start);
            }
         }
      }
   }
}
