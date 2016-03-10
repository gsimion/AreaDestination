namespace AreaDestinationVisualizer
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Windows.Forms;
   using AreaDestination;

   public partial class InteractiveWorldMap : UserControl
   {
      /// <summary>
      /// Event that can be used to be notified whenever the country selected within the world map has changed.
      /// </summary>
      public event CountrySelectedChanged Changed;

      /// <summary>
      /// Collection containing all the world countries.
      /// </summary>
      private readonly List<Country> _countries;

      private readonly DestinationSet<string> _countrySet;

      /// <summary>
      /// Static structure representing a country.
      /// </summary>
      public struct Country
      {
         private readonly Area<string> _countryArea;
         private readonly string _IsoCode;

         /// <summary>
         /// Gets the country dial code.
         /// </summary>
         public string Code
         {
            get
            {
               return _countryArea.ToString();
            }
         }

         /// <summary>
         /// Gets the country full name.
         /// </summary>
         public string CountryName
         {
            get
            {
               return _countryArea.Id;
            }
         }

         /// <summary>
         /// Gets the country ISO two-digits code.
         /// </summary>
         public string CountryISOCode
         {
            get
            {
               return _IsoCode;
            }
         }

         /// <summary>
         /// Creates a new instance of structure representing a country.
         /// </summary>
         /// <param name="name">Country full name</param>
         /// <param name="code">Two-digits ISO country code</param>
         /// <param name="dialCode">Dial code</param>
         public Country(string name, string code, ulong dialCode)
         {
            if (code == null || String.IsNullOrEmpty(name) || code.Length != 2)
               throw new ArgumentException("Wrong input(s)");
            _countryArea = new Area<string>(name, dialCode);
            _IsoCode = code.ToUpperInvariant();
         }
      }

      /// <summary>
      /// Interactive world map initializer.
      /// </summary>
      public InteractiveWorldMap()
      {
         InitializeComponent();
         _countries = GetCountryList();
         _countrySet = GetCountryDestinationSet(_countries);
         picCountry.Hide();
      }

      /// <summary>
      /// Overridable function handling the logic occurring before the event changed is called.
      /// </summary>
      /// <param name="e">Country changed event arguments</param>
      protected virtual void OnChanged(CountryChangedEventArgs e)
      {
         if (Changed != null)
            Changed(this, e);
      }

      /// <summary>
      /// Sets a country within the interactive world map.
      /// </summary>
      /// <param name="countryName">Country name or country ISO two-digits code</param>
      public void SetCountry(string countryName)
      {
         Country country = new Country();
         if (!String.IsNullOrEmpty(countryName))
         {
            if (countryName.Length == 2)
            {
               country = _countries.FirstOrDefault(x => x.CountryISOCode.Equals(countryName, StringComparison.OrdinalIgnoreCase));  
            }
            else
            {
               country = _countries.FirstOrDefault(x => x.CountryName.Equals(countryName, StringComparison.OrdinalIgnoreCase));  
            }
         }
         SetCountry(country);
      }

      /// <summary>
      /// Sets a country within the interactive world map.
      /// </summary>
      /// <param name="dialCode">Generic area code to map against the country</param>
      public void SetCountry(ulong dialCode)
      {
         string code = null;
         string destination = _countrySet.FindFirstMatchingDestination(dialCode);
         if (destination != null && !destination.Equals(_countrySet.UndefinedDestinationId))
            code = _countries.FirstOrDefault(x => x.CountryName.Equals(destination)).CountryISOCode;          
         SetCountry(code);
      }

      /// <summary>
      /// Sets a country within the interactive world map.
      /// </summary>
      /// <param name="country">Country</param>
      private void SetCountry(Country country)
      {
         Bitmap current = ReferenceData.GetCountryImage(country.CountryISOCode);
         if (picCountry.Image != null)
            picCountry.Image.Dispose();
         if (current != null)
         {
            int width = Math.Min(this.Width, this.Height);
            picCountry.Width = width;
            picCountry.Height = width;
            picCountry.Image = current;
            picCountry.SizeMode = PictureBoxSizeMode.StretchImage;
            picCountry.Refresh();
            picCountry.Show();
         }
         else
         {
            picCountry.Hide();
         }
         CountryChangedEventArgs args = new CountryChangedEventArgs(country.CountryName, country.CountryISOCode, country.Code);
         OnChanged(args);
      }

      /// <summary>
      /// Gets a country list populated from the reference data.
      /// </summary>
      private static List<Country> GetCountryList()
      {
         List<Country> result = new List<Country>();
         for (int i = 0; i < ReferenceData.CountryNames.Length; i++)
         {
            Country c = new Country(ReferenceData.CountryNames[i], ReferenceData.CountryISOCode[i], ReferenceData.CountryDialCodes[i]);
            result.Add(c);
         }
         return result;
      }

      /// <summary>
      /// Gets a destination set populated with data inside the country list.
      /// If duplicated dial codes have been found, the country is not updated in the destination set.
      /// </summary>
      /// <param name="countries">Collection of countries to be inserted in resulting destination set</param>
      /// <returns>Destination set, where destination name is the country name</returns>
      private DestinationSet<string> GetCountryDestinationSet(List<Country> countries)
      {
         DestinationSet<string> res = new DestinationSet<string>();
         HashSet<ulong> singleCodes = new HashSet<ulong>();
         foreach (Country c in countries)
         {
            ulong code = Convert.ToUInt64(c.Code);
            if (singleCodes.Add(code))
               res.AddArea(c.CountryName, code);
         }
         return res;
      }
   }
}
