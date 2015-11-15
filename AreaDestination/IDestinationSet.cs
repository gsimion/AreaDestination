using System;
using System.Collections.Generic;
using System.Data;

namespace AreaDestination
{
   /// <summary>
   /// Interface defining a generic destination set.
   /// </summary>
   /// <typeparam name="T0">Type of destination used</typeparam>
   /// <typeparam name="T1">Type of destination ID</typeparam>
   public interface IDestinationSet<T0, T1>
      where T0 : IDestination<T1>
      where T1 : IComparable
   {
      /// <summary>
      /// Gets the destination for a given destination id.
      /// </summary>
      /// <param name="id">Destination id</param>
      IDestination<T1> this[T1 id] { get; }
      /// <summary>
      /// Gets the list of destinations in the destination set.
      /// </summary>
      Dictionary<T1, T0> Destinations { get; }
      /// <summary>
      /// Adds an area to a destination, or create a new destination if it does not exists
      /// </summary>
      /// <param name="id">The name of the destination to update</param>
      /// <param name="code">The area code to add</param>
      void AddArea(T1 id, ulong code);
      /// <summary>
      /// Adds a destination to the destination set.
      /// </summary>
      /// <param name="d">Destination to add</param>
      void AddDestination(T0 d);
      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="area">Area to find in the destination set, can be a range, with '-' as delimiter</param>
      /// <returns>Destination name, if area falls within the destination set, <value>empty string</value> otherwise</returns>
      T1 FindArea(string area);
      /// <summary>
      /// Performs the long string match search among all the destinations to find the area.
      /// </summary>
      /// <param name="code">Area to find in the destination set</param>
      /// <returns>Destination name, if area falls within the destination set, <value>empty string</value> otherwise</returns>
      T1 FindArea(ulong code);
      /// <summary>
      /// Performs the string match search among all the destinations to find destinations that are covering a given area.
      /// For instance area '123' is covered by destination 'A' with '1234' and 'B' with '1235'.
      /// </summary>
      /// <param name="area">Area to find in the destination set, can be a range, with '-' as delimiter</param>
      /// <returns>Collection containing destination defining the area names</returns>
      IEnumerable<T1> FindDestinations(string area);
      /// <summary>
      /// Performs the string match search among all the destinations to find destinations that are covering a given area.
      /// For instance area '123' is covered by destination 'A' with '1234' and 'B' with '1235'.
      /// </summary>
      /// <param name="code">Area to find in the destination set</param>
      /// <returns>Collection containing destination defining the area names</returns>
      IEnumerable<T1> FindDestinations(ulong code);
      /// <summary>
      /// Populates the destination set given a generic dictionary of destination ids and area code strings.
      /// </summary>
      /// <param name="destinationsAndAreas">Generic dictionary of destination ids and area code strings</param>
      void Populate(IDictionary<T1, string> destinationsAndAreas);
      /// <summary>
      /// Populates the destination set given a generic table containing destination ids and area code strings on a row level.
      /// </summary>
      /// <param name="rows">Row collection containing destination ids and area code strings on a row level</param>
      /// <param name="destination">Destination ids column, type <paramref name="T">T</paramref> where empty values are not considered</param>
      /// <param name="areas">Areas (string) column</param>
      /// <param name="performAggregationOnAreas">If <value>true</value> performs area aggregation before calling the implemented class method for populating the destination set</param>
      /// <remarks>Empty destination ids are skipped</remarks>
      void Populate(IEnumerable<DataRow> rows, DataColumn destination, DataColumn area, bool performAggregationOnAreas);
      /// <summary>
      /// Removes an area from a destination if it exists
      /// </summary>
      /// <param name="code">The area code to add</param>
      void RemoveArea(ulong code);
      /// <summary>
      /// Updates a destination, by replacing existing definition of areas with the new specified one.
      /// </summary>
      /// <param name="id">Destination id</param>
      /// <param name="areas">Areas as string representation, where area code separator is ',' and range delimiter is '-'</param>
      void UpdateDestination(T1 id, string areas);
   }
}
