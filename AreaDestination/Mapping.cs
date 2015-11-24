namespace AreaDestination
{
   using System;
   using System.Collections.Generic;
   using System.Linq;

   /// <summary>
   /// Abstract class representing a generic destination set.
   /// </summary>
   public abstract partial class DestinationSetBase
   {
      /// <summary>
      /// Interface representing the mapping between two generic destination sets.
      /// </summary>
      /// <typeparam name="T">Type of destination and area ids</typeparam>
      public interface IMapping<T> where T : IComparable 
      {
         /// <summary>
         /// Gets the list of the areas that have been mapped.
         /// </summary>
         IList<MappedAreaRange<T>> Areas { get; }
         /// <summary>
         /// Gets mapping entity from.
         /// </summary>
         T FromId { get; }
         /// <summary>
         /// Gets mapping entity to.
         /// </summary>
         T ToId { get; }
      }
      
      /// <summary>
      /// Class that represents a Mapping between two entities. All overlapping ranges between the two entities are part of the Mapping
      /// </summary>
      protected class Mapping<T> : IMapping<T> where T : IComparable 
      {
         private List<MappedAreaRange<T>> _areas = new List<MappedAreaRange<T>>();
         /// <summary>
         /// Mapping entity from id.
         /// </summary>
         public T FromId { get; private set; }
         /// <summary>
         /// Mapping entity to id.
         /// </summary>
         public T ToId { get; private set; }

         /// <summary>
         /// Creates a new instance of the mapping between two entities.
         /// </summary>
         /// <param name="from">Entity from</param>
         /// <param name="to">Entity to</param>
         public Mapping(T from, T to)
         {
            FromId = from;
            ToId = to;
         }

         /// <summary>
         /// Gets the list of the areas that have been mapped.
         /// </summary>
         public IList<MappedAreaRange<T>> Areas
         {
            get { return _areas; }
         }

         /// <summary>
         /// Creates a new mapping by adding a mapped area range. 
         /// </summary>
         /// <param name="MappedAreaRange">Mapped area range to add</param>
         internal Mapping(MappedAreaRange<T> MappedAreaRange)
         {
            _areas.Add(MappedAreaRange);
         }

         /// <summary>
         /// Sets the internal list of mapped area range to a new instance.
         /// </summary>
         /// <param name="NewMappedAreaRanges">New list containing mapped area ranges</param>
         private void SetAreas(List<MappedAreaRange<T>> NewMappedAreaRanges)
         {
            _areas = NewMappedAreaRanges;
         }

         /// <summary>
         /// Returns a string that represents the mapped areas in compact representation between from destination and to destination.
         /// </summary>
         /// <returns>String that represents the mapped areas in compact representation between from destination and to destination</returns>
         public override string ToString()
         {
            MappedAreaRange<T> curArea = null;
            List<MappedAreaRange<T>> compactAreas = new List<MappedAreaRange<T>>();
            foreach (MappedAreaRange<T> a in Areas)
            {
               if (curArea == null || (curArea != null && curArea.End != a.Start))
               {
                  curArea = new MappedAreaRange<T>(a.FromId, a.ToId, a.Start, a.End);
                  compactAreas.Add(curArea);
               }
               else
               {
                  curArea.End = a.End;
               }
            }
            return string.Join(new string(Global.Sep, 1), compactAreas.Select(c => c.ToString()).ToArray());
         }
      }

      /// <summary>
      /// Results of a mapping between two destination sets. Contains the list of all calculated mappings between the 
      /// destinations in the destination set.
      /// </summary>
      /// <typeparam name="T">Type of destination ID</typeparam>
      public class MappingResult<T> where T : IComparable 
      {
         /// <summary>
         /// Structure containing the mapping state.
         /// </summary>
         public struct TState
         {
            private readonly TMapState _state;
            internal TState(TMapState state) { _state = state; }
            /// <summary>
            /// Gets whether [from -> to] map contains any form of mapping.
            /// If from destination does not have mapping, then it cannot have full, multiple and parial mapping.
            /// </summary>
            public bool HasMapping { get { return (_state != TMapState.Unmapped && _state != TMapState.Undefined); } }
            /// <summary>
            /// Gets whether [from -> to] map is full and unique (i.e. from destination has the same definition of to destination).
            /// </summary>
            public bool IsFullyMapped { get { return (_state == TMapState.Mapped); } }
            /// <summary>
            /// Gets whether [from -> to] map is full but not unique (i.e. from destination has the same definition of more than one to destination).
            /// </summary>
            public bool IsMultiMapped { get { return (_state == TMapState.MultiMapped); } }
            /// <summary>
            /// Gets whether [from -> to] map is not full and not unique (i.e. from destination has different definition of more than one to destination).
            /// </summary>
            public bool IsPartiallyMapped { get { return (_state == TMapState.PrtiallyMapped); } }            
         }

         /// <summary>
         /// Map state classification.
         /// </summary>
         internal enum TMapState
         {
            Undefined = -1,
            Unmapped = 0,
            Mapped = 1,
            MultiMapped = 2,
            PrtiallyMapped = 3
         }

         /// <summary>
         /// Gets full dictionary containing area codes changes from one destination id to another.
         /// </summary>
         public IDictionary<KeyValuePair<T, T>, IMapping<T>> Maps { get; private set; }
         
         private Dictionary<T, IDestination<T>> _materializedLost = null; // materialization of the lost area codes
         private Dictionary<T, IDestination<T>> _materializedGain = null; // materialization of the gained area codes
         private Dictionary<T, TState> _materializedMapState = null; // materialization of the map state

         /// <summary>
         /// Gets a list containing area code changes.
         /// </summary>
         public IList<IMapping<T>> ToList()
         {
            return Maps.Values.ToList();
         }

         /// <summary>
         /// Gets the total number of maps contained by the mapping result.
         /// </summary>
         public int Count
         {
            get { return Maps.Count; }
         }

         /// <summary>
         /// Gets a string representation of the mapped areas from a destination to another.
         /// </summary>
         /// <param name="from">Destination id from</param>
         /// <param name="to">Destionation id to</param>
         /// <returns>String representation of the mapped areas if a map exists, empty string otherwise</returns>
         public string GetMappedAreas(T from, T to)
         {
            KeyValuePair<T, T> key = new KeyValuePair<T, T>(from, to);
            if (Maps.ContainsKey(key))
               return Maps[key].ToString();
            return string.Empty;
         }

         /// <summary>
         /// Gets the mapping(s) from a given destination, if any.
         /// Filters the undefined destination, as it maps whatever is unmapped.
         /// </summary>
         /// <param name="from">Destination id from</param>
         /// <returns>Collection of mapping from the given destination</returns>
         public IEnumerable<IMapping<T>> GetMappedDestination(T from)
         {
            T fromDestinationID = from;
            var mappedDestinationFrom = (from map in Maps.Keys where map.Key.Equals(fromDestinationID) && !Utility<T>.IsDefault(map.Value) 
                                         select Maps[new KeyValuePair<T, T>(map.Key, map.Value)]);

            return mappedDestinationFrom;
         }

         /// <summary>
         /// Creates a new instance of the object containing the results of a mapping between two destination sets. 
         /// </summary>
         public MappingResult()
         {
            Maps = new Dictionary<KeyValuePair<T, T>, IMapping<T>>();
         }

         /// <summary>
         /// Gets the mapping state of a given destination id from.
         /// </summary>
         /// <param name="id">Destination id from</param>
         /// <returns>Map state, undefined if now found</returns>
         public TState this[T id]
         {
            get
            {
               TState mapState;
               if (_materializedMapState == null)
               {
                  T defaultValue = (T)Utility<T>.Default();
                  // including the unmapped destination for getting the partially mapped items
                  _materializedMapState = (from pair in Maps.Keys group pair by pair.Key)
                     .ToDictionary(
                     x=> x.Key, 
                     x=> new TState(GetState(x, defaultValue))
                     );
               }
               if (!_materializedMapState.TryGetValue(id, out mapState))
                  mapState = new TState(TMapState.Undefined);

               return mapState;
            }
         }

         /// <summary>
         /// Gets the map state state [from -> to] for a given from destination id.
         /// </summary>
         /// <param name="ToIdsGivenFromId">Group containing to ids, given from destination id</param>
         /// <param name="undefinedDestinationID">Id identifying the undefined destination, containing all the unmapped areas</param>
         /// <returns>Map state [from -> to] for a given from destination id</returns>
         private TMapState GetState(IGrouping<T, KeyValuePair<T,T>> ToIdsGivenFromId, T undefinedDestinationID)
         {
            T[] toIds = ToIdsGivenFromId.Select(x=> x.Value).ToArray();
            bool bToIdsContainUndefined = toIds.Contains(undefinedDestinationID);
            if (toIds.Length == 1 && bToIdsContainUndefined)
            {
               return TMapState.Unmapped;
            }
            if (toIds.Length == 1 && !bToIdsContainUndefined)
            {
               return TMapState.Mapped;
            }
            else if (toIds.Length > 1 && !bToIdsContainUndefined)
            {
               return TMapState.MultiMapped;
            }
            else if (toIds.Length > 1 && bToIdsContainUndefined)
            {
               return TMapState.PrtiallyMapped;
            }
            else
            {
               return TMapState.Undefined;
            }      
         }

         /// <summary>
         /// Adds a mapped area range to the mapping result.
         /// </summary>
         /// <param name="mar">Mapped area range to add</param>
         internal void AddMap(MappedAreaRange<T> mar)
         {
            KeyValuePair<T, T> key = new KeyValuePair<T, T>(mar.FromId, mar.ToId);
            if (!Maps.ContainsKey(key))
               Maps.Add(key, new Mapping<T>(mar.FromId, mar.ToId));
            Maps[key].Areas.Add(mar);
         }

         /// <summary>
         /// Gets a dictionary containing lost area codes by destination id.
         /// </summary>
         public IDictionary<T, IDestination<T>> LostAreaCodes
         {
            get
            {
               if (_materializedLost == null)
               {
                  _materializedLost = new Dictionary<T, IDestination<T>>();
                  foreach (Mapping<T> m in Maps.Values)
                  {
                     if (!m.FromId.Equals(m.ToId))
                     {
                        IDestination<T> d;
                        if (!_materializedLost.TryGetValue(m.FromId, out d))
                        {
                           d = new ExplicitDestination<T>(m.FromId);
                           _materializedLost.Add(m.FromId, d);
                        }
                        foreach (MappedAreaRange<T> mar in m.Areas)
                           ((ExplicitDestination<T>)d).Add(new Area<T>(mar.FromId, mar.Start, mar.End));
                     }
                  }
               }
               return _materializedLost;
            }
         }

         /// <summary>
         /// Gets a dictionary containing gained area codes by destination id.
         /// </summary>
         public IDictionary<T, IDestination<T>> GainedAreaCodes
         {
            get
            {
               if (_materializedGain == null)
               {
                  _materializedGain = new Dictionary<T, IDestination<T>>();
                  foreach (Mapping<T> m in Maps.Values)
                  {
                     if (!m.FromId.Equals(m.ToId))
                     {
                        IDestination<T> d;
                        if (!_materializedGain.TryGetValue(m.ToId, out d))
                        {
                           d = new ExplicitDestination<T>(m.ToId);
                           _materializedGain.Add(m.ToId, d);
                        }
                        foreach (MappedAreaRange<T> mar in m.Areas)
                           ((ExplicitDestination<T>)d).Add(new Area<T>(mar.ToId, mar.Start, mar.End));
                     }
                  }
               }
               return _materializedGain;
            }
         }
      }
   }
}
