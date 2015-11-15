using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace AreaDestination
{
   /// <summary>
   /// Base class defining a destination set.
   /// </summary>
   /// <typeparam name="T0">Type of destination used</typeparam>
   /// <typeparam name="T1">Type of destination ID</typeparam>
   public abstract class DestinationSetBase<T0, T1> : IDestinationSet<T0, T1> where T0 : IDestination<T1> where T1: IComparable
   {
      protected const char Sep = Global.Sep; // separator
      protected const char Del = Global.Del; // delimiter
      
      /// <summary>
      /// Adds a destination to the destination set.
      /// </summary>
      /// <param name="d">Destination to add</param>
      public abstract void AddDestination(T0 d);

      /// <summary>
      /// Populates the destination set given a generic dictionary of destination ids and area code strings.
      /// </summary>
      /// <param name="destinationsAndAreas">Generic dictionary of destination ids and area code strings</param>
      public void Populate(IDictionary<T1, String> destinationsAndAreas)
      {
         foreach (T1 id in destinationsAndAreas.Keys)
         {
            bool bIsDefault = Utility<T1>.IsDefault(id);
            if (id != null && !bIsDefault)
               UpdateDestination(id, destinationsAndAreas[id]);
         }
      }

      /// <summary>
      /// Gets the destination for a given destination id.
      /// </summary>
      /// <param name="id">Destination id</param>
      public IDestination<T1> this[T1 id] { get { return Destinations[id]; } }

      /// <summary>
      /// Gets the list of destinations in the destination set in implicit format (overlapping area codes are allowed)
      /// </summary>
      public abstract Dictionary<T1, T0> Destinations { get; }

      /// <summary>
      /// Populates the destination set given a generic table containing destination ids and area code strings on a row level.
      /// </summary>
      /// <param name="rows">Row collection containing destination ids and area code strings on a row level</param>
      /// <param name="destination">Destination ids column, type <paramref name="T">T</paramref> where empty values are not considered</param>
      /// <param name="areas">Areas (string) column</param>
      /// <param name="performAggregationOnAreas">If <value>true</value> performs area aggregation before calling the implemented class method for populating the destination set</param>
      /// <remarks>Empty destination ids are skipped</remarks>
      public void Populate(IEnumerable<DataRow> rows, DataColumn destination, DataColumn area, Boolean performAggregationOnAreas)
      {
         IDictionary<T1, String> destinationAndAreas;
         if (!performAggregationOnAreas)
         {
            destinationAndAreas = (from row in rows.AsEnumerable()
                                   where row.RowState != DataRowState.Deleted && !row.IsNull(destination) && !row.IsNull(area) && row[destination] != null
                                   group row by row.Field<T1>(destination))
                                   .ToDictionary(x => x.Key, y => String.Join(Sep.ToString(), y.Select(z => z.Field<string>(area)).ToArray()));
         }
         else
         {
            Dictionary<T1, List<String>> destinationAndCodes = new Dictionary<T1, List<String>>();
            foreach (DataRow row in rows)
            {
               if (row.IsNull(destination) || row.IsNull(area) || row[destination] == null)
                  continue;
               List<String> codes = null;
               T1 dest = row.Field<T1>(destination);
               if (!destinationAndCodes.TryGetValue(dest, out codes))
               {
                  codes = new List<String>();
                  destinationAndCodes.Add(dest, codes);
               }
               destinationAndCodes[dest].Add(row.Field<string>(area));
            }
            destinationAndAreas = new Dictionary<T1, String>();
            foreach (KeyValuePair<T1, List<String>> entry in destinationAndCodes)
               destinationAndAreas.Add(entry.Key, AreaTransformer.TransformAreaDigitsToString(entry.Value, Sep, Del));
         }

         Populate(destinationAndAreas);
      }
  
      /// <summary>
      /// Adds an area to a destination, or create a new destination if it does not exists
      /// </summary>
      /// <param name="id">The name of the destination to update</param>
      /// <param name="code">The area code to add</param>
      public abstract void AddArea(T1 id, ulong code);

      /// <summary>
      /// Removes an area from a destination if it exists
      /// </summary>
      /// <param name="code">The area code to add</param>
      public abstract void RemoveArea(ulong code);

      /// <summary>
      /// Updates a destination, by replacing existing definition of areas with the new specified one.
      /// </summary>
      /// <param name="id">Destination id</param>
      /// <param name="areas">Areas as string representation, where area code separator is ',' and range delimiter is '-'</param>
      public abstract void UpdateDestination(T1 id, string areas);

      /// <summary>
      /// Gets a matrix representing the areas passed as single area (array with single element) or range (array with two elements).
      /// </summary>
      /// <param name="sAreas">Full area representation, where area code separator is ',' and range delimiter is '-'</param>
      /// <returns>Area matrix</returns>
      public ulong[][] GetAreasAndRanges(string sAreas, char sep, char del)
      {
         if (sAreas == null)
            return new ulong[][] { };
         ulong[][] areaRangeArray = (from String range in sAreas.Split(new char[] { sep }, StringSplitOptions.RemoveEmptyEntries)
                                      select (from area in range.Split(new char[] { del }, StringSplitOptions.RemoveEmptyEntries)
                                              select Convert.ToUInt64(area, CultureInfo.InvariantCulture.NumberFormat)).ToArray())
            .ToArray();
         return areaRangeArray;
      }

      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="areacode">Area to find in the destination set</param>
      /// <returns>Destination name, if area falls within the destination set, <value>empty string</value> otherwise</returns>
      public abstract T1 FindArea(ulong areacode);

      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="value">Area to find in the destination set, can be a range, with '-' as delimiter</param>
      /// <returns>Destination name, if area falls within the destination set, <value>empty string</value> otherwise</returns>
      public T1 FindArea(string value)
      {
         ulong[][] areamatrix = GetAreasAndRanges(value, Sep, Del);
         if (areamatrix.Length != 1 || areamatrix[0].Length > 2)
            throw new FormatException(Properties.Resources.msgInvalidAreaFormatPassed);
         if (areamatrix[0].Length == 1)
            return FindArea(new Area<T1>((T1)Utility<T1>.Default(), areamatrix[0][0]));
         else
            return FindArea(new Area<T1>((T1)Utility<T1>.Default(), areamatrix[0][0], areamatrix[0][1]));
      }

      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="a">Area to find in the destination set</param>
      /// <returns>Destination name, if area falls within the destination set, <value>empty string</value> otherwise</returns>
      protected abstract T1 FindArea(ZeroOneAreaRange a);

      /// <summary>
      /// Performs the string match search among all the destinations to find destinations that are covering a given area.
      /// For instance area '123' is covered by destination 'A' with '1234' and 'B' with '1235'.
      /// </summary>
      /// <param name="areacode">Area to find in the destination set</param>
      /// <returns>Collection containing destination defining the area names</returns>
      public abstract IEnumerable<T1> FindDestinations(ulong areacode);

      /// <summary>
      /// Performs the string match search among all the destinations to find destinations that are covering a given area.
      /// For instance area '123' is covered by destination 'A' with '1234' and 'B' with '1235'.
      /// </summary>
      /// <param name="value">Area to find in the destination set, can be a range, with '-' as delimiter</param>
      /// <returns>Collection containing destination defining the area names</returns>
      public abstract IEnumerable<T1> FindDestinations(string value);
   }
}
