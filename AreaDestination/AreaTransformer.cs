namespace AreaDestination
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Globalization;
   using System.Data;

   /// <summary>
   /// Static class used for formatting/transforming area codes representations.
   /// </summary>
   public static class AreaTransformer
   {
      /// <summary>
      /// Gets the collection of single digits from a string having a full representation of areas with the delimiter and separator specified.
      /// Eats up the child areas and explodes the ranges.
      /// </summary>
      /// <typeparam name="T">Type to parse the areas to (must be string or numeric)</typeparam>
      /// <param name="areas">String containing the full representation of areas</param>
      /// <param name="sep">Area separator used within <paramref name="areas">area string</paramref></param>
      /// <param name="del">Area range delimiter used within <paramref name="areas">area string</paramref></param>
      /// <returns>List containing single digits.</returns>
      /// <example>'1,12,146' -> ['1']; '400-402' -> ['400', '401', '402'], '4-62' -> ['4', '5', '60', '61', '62'], '688-8' -> ['688', '689', '69', '7', '8']</example>
      /// <exception cref="FormatException">When <paramref name="areas"/> is not correctly specified according to <paramref name="sep"/> and <paramref name="del"/></exception>
      public static List<T> GetAreasFromString<T>(string areas, char sep, char del) where T : IComparable
      {
         return GetAreasFromString<T>(areas, sep, del, ParsingMode.EatChildReturnSingleDigits);
      }

      /// <summary>
      /// Gets the collection of areas from a string having a full representation of areas with the delimiter and separator specified.
      /// </summary>
      /// <typeparam name="T">Type to parse the areas to (must be string or numeric)</typeparam>
      /// <param name="areas">String containing the full representation of areas</param>
      /// <param name="sep">Area separator used within <paramref name="areas">area string</paramref></param>
      /// <param name="del">Area range delimiter used within <paramref name="areas">area string</paramref></param>
      /// <param name="parsing">Parsing method used: 
      /// if <value>EatChildReturnSingleDigits</value> eats child areas and prints single digits only (no ranges);
      /// if <value>EatChildReturnRanges</value> eats child areas and prints single digits and ranges when possible;
      /// if <value>PreserveChildReturnSingleDigits</value> preserves explicit areas and prints single digits only (no ranges);
      /// if <value>PreserveChildReturnRanges</value> preserves explicit areas and prints single digits and ranges when possible</param>
      /// <returns>List containing single areas formatted according to the parsing method in string format.</returns>
      /// <exception cref="FormatException">When <paramref name="areas"/> is not correctly specified according to <paramref name="sep"/> and <paramref name="del"/></exception>
      public static List<T> GetAreasFromString<T>(string areas, char sep, char del, ParsingMode parsing) where T : IComparable
      {
         if (areas == null || areas.Length == 0)
            return new List<T>();

         List<Area<string>> arealist = new List<Area<string>>();

         ulong[][] areamatrix = GetAreasAndRanges(areas, sep, del);
         foreach (ulong[] arearange in areamatrix)
         {
            if (arearange.Length == 1)
               arealist.Add(new Area<string>(string.Empty, arearange[0]));
            else if (arearange.Length == 2)
               arealist.Add(new Area<string>(string.Empty, arearange[0], arearange[1]));
            else
               throw new FormatException(Properties.Resources.msgInvalidAreaFormatPassed);
         }

         ExplicitDestination<string> explDest = new ExplicitDestination<string>(string.Empty);
         Destination<string> sysDest = new Destination<string>(string.Empty);

         List<T> res = new List<T>();

         switch (parsing)
         {
            case ParsingMode.EatChildReturnRanges:
               foreach (Area<string> a in arealist)
                  explDest.AddArea(a);
               res.AddRange(from a in explDest.Areas select (T)Convert.ChangeType(a.ToString(del), typeof(T)));
               break;
            case ParsingMode.EatChildReturnSingleDigits:
               foreach (Area<string> a in arealist)
                  explDest.AddArea(a);
               res.AddRange(from a in explDest.FullExpandedAreas select (T)Convert.ChangeType(a.ToString(), typeof(T)));
               break;
            case ParsingMode.PreserveChildReturnRanges:
               foreach (Area<string> a in arealist)
                  sysDest.AddArea(a);
               res.AddRange(from a in sysDest.Areas select (T)Convert.ChangeType(a.ToString(del), typeof(T)));
               break;
            case ParsingMode.PreserveChildReturnSingleDigits:
               foreach (Area<string> a in arealist)
                  sysDest.AddArea(a);
               foreach (Area<string> area in sysDest.Areas)
               {
                  string a = area.ToString();
                  int iIdx = a.IndexOf(Global.Del);
                  if (iIdx <= 0)
                     res.Add((T)Convert.ChangeType(a, typeof(T)));
                  else
                  {
                     for (ulong l = Convert.ToUInt64(a.Substring(0, iIdx), CultureInfo.InvariantCulture.NumberFormat); l <= Convert.ToUInt64(a.Substring(iIdx + 1), CultureInfo.InvariantCulture.NumberFormat); l++)
                        res.Add((T)Convert.ChangeType(l, typeof(T)));
                  }
               }
               break;
         }
         return res;
      }

      /// <summary>
      /// Gets a matrix representing the areas passed as single area (array with single element) or range (array with two elements).
      /// </summary>
      /// <param name="areas">Full area representation</param>
      /// <param name="sep">Area code separator</param>
      /// <param name="del">Area code range delimiter</param>
      /// <returns>Area matrix</returns>
      private static ulong[][] GetAreasAndRanges(string areas, char sep, char del)
      {
         if (areas == null)
            return new ulong[][] { };
         ulong[][] areaRangeArray = (from string sRange in areas.Split(new char[] { sep }, StringSplitOptions.RemoveEmptyEntries)
                                     select (from area in sRange.Split(new char[] { del }, StringSplitOptions.RemoveEmptyEntries)
                                             select Convert.ToUInt64(area, CultureInfo.InvariantCulture.NumberFormat)).ToArray())
            .ToArray();
         return areaRangeArray;
      }

      /// <summary>
      /// Gets a string representing the merge areas contained by the arguments. 
      /// Resulting string will eat child areas and overlaps.
      /// </summary>
      /// <param name="sep">Area separator I/O</param>
      /// <param name="del">Area range delimiter I/O</param>
      /// <param name="codesCollection">Strings containing the areas to merge, represented according to the parameters passed</param>
      /// <param name="compression">Compression method</param>
      /// <returns>String representing the merge areas</returns>
      public static string MergeAreas(IEnumerable<string> codesCollection, char sep, char del, Compression compression)
      {
         ParsingMode parsing = (compression == Compression.Full) ? ParsingMode.EatChildReturnSingleDigits : ParsingMode.PreserveChildReturnSingleDigits;
         string init = String.Empty;
         if (codesCollection == null || !codesCollection.Any())
            return init;

         HashSet<ulong> codes = new HashSet<ulong>();
         foreach (string s in codesCollection)
            foreach (ulong l in GetAreasFromString<ulong>(s, sep, del, parsing))
               codes.Add(l);
         if (compression == Compression.Full)
         {
            ExplicitDestination<Int32> dest = new ExplicitDestination<Int32>(Int32.MaxValue);
            foreach (ulong c in codes)
               dest.AddArea(c);
            return dest.ToString().Replace(new string(Global.Sep, 1), new string(new char[] { sep, Global.Space })).Replace(Global.Del, del);
         }
         else
         {
            Destination<Int32> dest = new Destination<Int32>(Int32.MaxValue);
            foreach (ulong c in codes)
               dest.AddArea(c);
            return dest.ToString().Replace(new string(Global.Sep, 1), new string(new char[] { sep, Global.Space })).Replace(Global.Del, del);
         }
      }

      /// <summary>
      /// Gets a string having a full representation of the passed area codes. Compresses child digits already belonging to another one.
      /// </summary>
      /// <param name="digits">List of digits to get the string representation from</param>
      /// <param name="sep">Area separator used for the output</param>
      /// <param name="del">Area range delimiter used for the output</param>
      /// <param name="compression">Compression method to use</param>
      /// <returns>Full representation as string.</returns>
      public static string TransformAreaDigitsToString(List<ulong> digits, char sep, char del, Compression compression)
      {
         if (digits == null || digits.Count == 0)
            return String.Empty;
         if (compression == Compression.Full)
         {
            ExplicitDestination<int> destination = new ExplicitDestination<int>(Int32.MaxValue);
            foreach (ulong fullCode in digits)
            {
               destination.AddArea(fullCode);
            }
            return destination.ToString().Replace(new string(Global.Sep, 1), new string(new char[] { sep, Global.Space })).Replace(Global.Del, del);
         }
         else
         {
            Destination<int> destination = new Destination<int>(Int32.MaxValue);
            foreach (ulong l in digits)
            {
               destination.AddArea(l);
            }
            return destination.ToString().Replace(new string(Global.Sep, 1), new string(new char[] { sep, Global.Space })).Replace(Global.Del, del);
         }
      }

      /// <summary>
      /// Gets a string having a full representation of the passed digits.
      /// </summary>
      /// <param name="digits">List of digits to get the string representation from</param>
      /// <param name="sep">Area separator used for the output</param>
      /// <param name="del">Area range delimiter used for the output</param>
      /// <param name="compression">Compression method to use</param>
      /// <param name="trimZeros">If <value>True</value> trim the initial zeros, treating the digits as full area codes, so that 0046 becomes 46</param>
      /// <returns>Full representation as string.</returns>
      public static string TransformAreaDigitsToString(List<string> digits, char sep, char del, Compression compression, bool trimZeros = true)
      {
         if (digits == null || digits.Count == 0)
            return string.Empty;

         if (trimZeros)
         {
            List<ulong> d = digits.Select(x => Convert.ToUInt64(x)).ToList();
            return TransformAreaDigitsToString(d, sep, del, compression);
         }
         else
         {
            if (compression == Compression.Full)
            {
               ExplicitDestination<int> destination = new ExplicitDestination<int>(Int32.MaxValue);
               foreach (string digit in digits)
               {
                  destination.Add(new Area<int>(destination.Id, digit));
               }
               return destination.ToString().Replace(new string(Global.Sep, 1), new string(new char[] { sep, Global.Space })).Replace(Global.Del, del);
            }
            else
            {
               Destination<int> destination = new Destination<int>(Int32.MaxValue);
               foreach (string digit in digits)
               {
                  destination.AddArea(new Area<int>(destination.Id, digit));
               }
               return destination.ToString().Replace(new string(Global.Sep, 1), new string(new char[] { sep, Global.Space })).Replace(Global.Del, del);
            }
         }
      }

      /// <summary>
      /// Gets a string having a full representation of the passed digits.
      /// </summary>
      /// <param name="rows">Rows containing the digits to get the string representation from</param>
      /// <param name="column">Column of type string containing the area digits</param>
      /// <param name="sep">Area separator used for the output</param>
      /// <param name="del">Area range delimiter used for the output</param>
      /// <param name="compression">Compression method to use</param>
      /// <param name="trimZeros">If <value>True</value> trim the initial zeros, treating the digits as full area codes, so that 0046 becomes 46</param>
      /// <returns>Full representation as string.</returns>
      public static string TransformAreaDigitsToString(IEnumerable<DataRow> rows, DataColumn column, char sep, char del, Compression compression, bool trimZeros = true)
      {
         if (rows == null || !rows.Any())
            return string.Empty;

         if (column == null || column.DataType != typeof(string))
            return string.Empty;

         List<string> digits = (from row in rows let digit = row[column].ToString() where digit.Length > 0 select digit).ToList();
         return TransformAreaDigitsToString(digits, sep, del, compression, trimZeros);
      }

      /// <summary>
      /// Gets a string having a full representation of the passed digits.
      /// </summary>
      /// <param name="rows">Rows containing the digits to get the string representation from</param>
      /// <param name="column">Column of type string containing the area digits name</param>
      /// <param name="sep">Area separator used for the output</param>
      /// <param name="del">Area range delimiter used for the output</param>
      /// <param name="compression">Compression method to use</param>
      /// <param name="trimZeros">If <value>True</value> trim the initial zeros, treating the digits as full area codes, so that 0046 becomes 46</param>
      /// <returns>Full representation as string.</returns>
      public static string TransformAreaDigitsToString(IEnumerable<DataRow> rows, string column, char sep, char del, Compression compression, bool trimZeros = true)
      {
         if (rows == null || !rows.Any())
            return string.Empty;

         if (!rows.First().Table.Columns.Contains(column))
            return string.Empty;

         return TransformAreaDigitsToString(rows, rows.First().Table.Columns[column], sep, del, compression, trimZeros);
      }
   }
}
