using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AreaDestination
{
   /// <summary>
   /// Generic interface defining a range.
   /// </summary>
   public interface IRange<T> : ICloneable where T : IComparable
   {
      T Start { get; }
      T End { get; }
   }
}
