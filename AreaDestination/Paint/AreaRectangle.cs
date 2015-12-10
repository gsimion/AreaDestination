namespace AreaDestination
{
   using System;

   /// <summary>
   /// Class representing an area as an abstraction of a rectangle.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class AreaRectangle<T> : Area<T> where T:IComparable 
   {
      internal readonly decimal _rank;

      /// <summary>
      /// Gets/sets the description of the rectangle.
      /// </summary>
      public string Description { get; set; }
      
      /// <summary>
      /// Gets the title of the rectangle.
      /// </summary>
      public string Title 
      {
         get
         {
            return Id.ToString();
         }
      }

      /// <summary>
      /// Gets the width dilation of the rectangle.
      /// </summary>
      public int Scale { get; private set; }

      /// <summary>
      /// Gets the height dilation of the rectangle.
      /// </summary>
      public int ScaleY { get; set; }

      /// <summary>
      /// Gets the X coordinate according to the scale set.
      /// </summary>
      public int X
      {
         get
         {
            return Convert.ToInt32(decimal.Round(Start * Scale,0));
         }
      }

      /// <summary>
      /// Gets the width of the rectangle.
      /// </summary>
      public int Width 
      {
         get
         {
            return Convert.ToInt32(decimal.Round(((End - Start) * Scale), 0));
         }
      }

      /// <summary>
      /// Gets the height of the rectangle.
      /// </summary>
      public int Height 
      {
         get
         {
            return Convert.ToInt32(decimal.Round(_rank * ScaleY));
         }
      }

      /// <summary>
      /// Creates a new rectangle representing an area.
      /// </summary>
      /// <param name="area">Area</param>
      internal AreaRectangle(IArea<T> area)
         : this(area, string.Empty)
      { }

      /// <summary>
      /// Creates a new rectangle representing an area.
      /// </summary>
      /// <param name="area">Area</param>
      /// <param name="description">Description</param>
      internal AreaRectangle(IArea<T> area, string description) : base(area.Id, area.Start, area.End)
      {
         _rank = GetRank(Start);
         Scale = 1;
         ScaleY = Scale;
         Description = description;
      }

      /// <summary>
      /// Sets a specific scale for the rectangle dimensions.
      /// </summary>
      /// <param name="scale">Scale</param>
      public void SetScale(int scale)
      {
         if (scale < 1)
            throw new ArgumentException("Scale must be greater than zero");
         Scale = scale;
         ScaleY = scale;
      }

      /// <summary>
      /// Gets a number in [0,1] specifying the rank, according to the object definition.
      /// </summary>
      /// <param name="value">Value</param>
      /// <returns>Rank</returns>
      private static decimal GetRank(decimal value)
      {
         const decimal  maxPrecision = 20; // by definition of area
         int precision = BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
         return (maxPrecision - (precision)) / maxPrecision;
      }
   }
}
