namespace AreaDestinationVisualizer
{
   using System;

   /// <summary>
   /// Delegate type for hooking up country selected changed notifications.
   /// </summary>
   public delegate void CountrySelectedChanged(object sender, CountryChangedEventArgs e);

   /// <summary>
   /// Country changed event arguments.
   /// </summary>
   public class CountryChangedEventArgs : EventArgs
   {
      /// <summary>
      /// Creates a new instance of country changed event arguments.
      /// </summary>
      /// <param name="name">Country full name</param>
      /// <param name="countryISOcode">Country two-digits ISO code</param>
      /// <param name="dialCode">Country dial code</param>
      public CountryChangedEventArgs(string name, string countryISOcode, string dialCode) : base()
      {
         this.CountryName = name;
         this.CountryISOCode = countryISOcode;
         this.DialCode = dialCode;
      }
      /// <summary>
      /// Country full name.
      /// </summary>
      public string CountryName { get; private set; }
      /// <summary>
      /// Country two-digits ISO code.
      /// </summary>
      public string CountryISOCode { get; private set; }
      /// <summary>
      /// Country dial code.
      /// </summary>
      public string DialCode { get; private set; }
   }
}