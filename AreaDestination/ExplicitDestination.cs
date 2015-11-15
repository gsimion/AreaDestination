using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AreaDestination
{
   /// <summary>
   /// Class representing a destination as a set of full explicit area ranges. 
   /// Supports adding ranges in implicit format and getting a compacted version of it.
   /// </summary>
   /// <typeparam name="T">Type of destination ID</typeparam>
   public class ExplicitDestination<T> : AreaLine<T>, IDestination<T> where T: IComparable
   {
      /// <summary>
      /// Destination Id.
      /// </summary>
      public T Id { get; private set; }

      /// <summary>
      /// Gets the list of single codes belonging to the destination instance.
      /// </summary>
      public IEnumerable<ulong> SingleCodes { get { return FullExpandedAreas; } }

      /// <summary>
      /// Gets the list of areas belonging to the destination instance.
      /// </summary>
      public IEnumerable<IArea<T>> Areas { get { return GetExplicitAreas(); } }

      /// <summary>
      /// Get the full expanded representation of the explicit areas.
      /// Will expand every range in all the area codes belonging to that particular range.
      /// </summary>
      public IEnumerable<ulong> FullExpandedAreas
      {
         get
         {
            List<ulong> areas = new List<ulong>();
            foreach (Area<T> area in GetExplicitAreas())
            {
               string a = area.ToString();
               int iIdx = a.IndexOf(Global.Del);
               if (iIdx <= 0)
                  areas.Add(Convert.ToUInt64(a, CultureInfo.InvariantCulture.NumberFormat));
               else
               {
                  for (ulong l = Convert.ToUInt64(a.Substring(0, iIdx), CultureInfo.InvariantCulture.NumberFormat); l <= Convert.ToUInt64(a.Substring(iIdx + 1), CultureInfo.InvariantCulture.NumberFormat); l++)
                     areas.Add(l);
               }
            }
            return areas;
         }
      }

      /// <summary>
      /// Gets whether the destination contains implicit areas.
      /// </summary>
      public bool IsEmpty { get { return !HasSegments; } }

      /// <summary>
      /// Creates a new empty destination.
      /// </summary>
      /// <param name="id">Destination id</param>
      internal ExplicitDestination(T id)
         : base()
      {
         Id = id;
      }

      /// <summary>
      /// Creates a new empty destination.
      /// </summary>
      /// <param name="id">Destination id</param>
      /// <param name="areas">List of areas belonging to the destination, not containing overlaps</param>
      internal ExplicitDestination(T id, List<Area<T>> areas)
         : base()
      {
         Id = id;
         foreach (Area<T> area in areas)
            AddArea(area);
      }

      /// <summary>
      /// Adds an area to the destination.
      /// </summary>
      /// <param name="a">Area to add</param>
      public void AddArea(IArea<T> a)
      {
         Add(a);
      }

      /// <summary>
      /// Adds an area code to the destination.
      /// </summary>
      /// <param name="code">Area code to add</param>
      public void AddArea(ulong code)
      {
         Area<T> a = new Area<T>((T)Utility<T>.Default(), code);
         AddArea(a);
      }

      /// <summary>
      /// Removes an area from the destination.
      /// </summary>
      /// <param name="a">Area to remove</param>
      public void RemoveArea(IArea<T> a)
      {
         Remove(a);
      }

      /// <summary>
      /// Removes an area code from the destination.
      /// </summary>
      /// <param name="code">Area code to remove</param>
      public void RemoveArea(ulong code)
      {
         Area<T> a = new Area<T>((T)Utility<T>.Default(), code);
         RemoveArea(a);
      }

      /// <summary>
      /// Gets the string representation of the destination.
      /// </summary>
      /// <returns>String representation of the destination</returns>
      public override string ToString()
      {
         IArea<T>[] areas = Areas.ToArray();
         if (areas.Length == 0)
            return String.Empty;
         else if (areas.Length == 1)
            return areas[0].ToString();
         else
         {
            StringBuilder sb = new StringBuilder();
            string res = areas.Aggregate(sb, (builder, area) => builder.Append(area.ToString()).Append(Global.Sep)).ToString().TrimEnd(new char[]{Global.Sep});
            return res;
         }
      }
   }
}
