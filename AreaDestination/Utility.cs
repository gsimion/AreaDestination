using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreaDestination
{
   /// <summary>
   /// Utility class overriding checks for defaults.
   /// </summary>
   /// <typeparam name="T">Type</typeparam>
   internal static class Utility<T> where T : IComparable
   {
      /// <summary>
      /// Gets the default of type T.
      /// </summary>
      /// <returns>Default of type T</returns>
      public static object Default()
      {
         if (typeof(T) == typeof(string))
            return String.Empty;
         else
            return default(T);
      }

      /// <summary>
      /// Gets whether a T object matches its default definition.
      /// </summary>
      /// <returns><value>True</value> if it matches, <value>false</value> otherwise</returns>
      public static bool IsDefault(T obj)
      {
         if (obj == null)
            return false;
         if (typeof(T) == typeof(string))
            return (obj.ToString().Length == 0);
         else
            return (default(T).Equals(obj));
      }
   }
}
