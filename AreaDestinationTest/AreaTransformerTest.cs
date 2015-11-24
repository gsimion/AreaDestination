using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AreaDestination;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AreaDestinationTest
{
   [TestClass]
   public class AreaTransformerTest
   {

      #region "StringToDigit"

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only , and with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only, and with default delimiters")]
      public void AreaTransformer_StringToDigitWithZeroCountryCodeEatChildReturnRanges()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("0", ';', '-', ParsingMode.EatChildReturnRanges);

         Assert.IsTrue(AreaList.Contains("0"), "0 not returned correctly after transformation");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only , and with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only, and with default delimiters")]
      public void AreaTransformer_StringToDigitWithZeroCountryCodeEatChildReturnDigits()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("0", ';', '-', ParsingMode.EatChildReturnSingleDigits);

         Assert.IsTrue(AreaList.Contains("0"), "0 not returned correctly after transformation");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only , and with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only, and with default delimiters")]
      public void AreaTransformer_StringToDigitWithZeroCountryCodePreserveChildReturnRanges()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("0", ';', '-', ParsingMode.PreserveChildReturnRanges);

         Assert.IsTrue(AreaList.Contains("0"), "0 not returned correctly after transformation");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only , and with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly,when country code is set to 0 only, and with default delimiters")]
      public void AreaTransformer_StringToDigitWithZeroCountryCodePreserveChildReturnDigits()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("0", ';', '-', ParsingMode.PreserveChildReturnSingleDigits);

         Assert.IsTrue(AreaList.Contains("0"), "0 not returned correctly after transformation");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts string to digit, with long numbers.
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts string to digit, with long numbers")]
      public void AreaTransformer_StringToDigitLongAreaCodes()
      {
         foreach (var p in Enum.GetValues(typeof(ParsingMode)))
         {
            AreaTransformer_StringToDigitLongAreaCodes((ParsingMode)p);
         }
      }

      private void AreaTransformer_StringToDigitLongAreaCodes(ParsingMode ParsingMode)
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("123456, 21234556", ',', '-', ParsingMode);

         Assert.IsTrue(AreaList.Contains("123456"), "123456");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, with full range and 'eating up' child numbers.
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with full range and 'eating up' child numbers")]
      public void AreaTransformer_StringToDigitMakeCompact()
      {
         foreach (var p in Enum.GetValues(typeof(ParsingMode)))
         {
            AreaTransformer_StringToDigitMakeCompact((ParsingMode)p);
         }
      }

      private void AreaTransformer_StringToDigitMakeCompact(ParsingMode ParsingMode)
      {
         bool bExpectedEatingUpChildren = (ParsingMode == ParsingMode.EatChildReturnRanges || ParsingMode == ParsingMode.EatChildReturnSingleDigits);
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("10-19", ';', '-', ParsingMode);
         Assert.IsTrue(AreaList.Contains("1"));

         AreaList = AreaTransformer.GetAreasFromString<string>("11;110-117", ';', '-', ParsingMode);
         Assert.IsTrue(AreaList.Contains("11"));
         Assert.AreNotEqual<bool>(bExpectedEatingUpChildren, AreaList.Any(x => x.StartsWith("110")), "Child areas expected to be handled correctly");

         AreaList = AreaTransformer.GetAreasFromString<string>("5;510-517", ';', '-', ParsingMode);
         int iExpectedCount = bExpectedEatingUpChildren ? 1 : ParsingMode == ParsingMode.PreserveChildReturnSingleDigits ? 9 : 2;
         Assert.AreEqual<int>(iExpectedCount, AreaList.Count, "Child areas expected to be handled correctly");
         Assert.AreEqual<string>("5", AreaList.First(), "Parent digit expected to be at first place");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts areas correctly without compact.
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts areas correctly without compact")]
      public void AreaTransformer_CanGetTheAreasWithoutEatingUpChildAreas()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("11,110-117", ',', '-', ParsingMode.PreserveChildReturnRanges);
         List<string> ExpectedSequence = new List<string>(new string[] { "11", "110-117" });
         Assert.IsTrue(ExpectedSequence.SequenceEqual(AreaList));

         AreaList = AreaTransformer.GetAreasFromString<string>("11,110-117", ',', '-', ParsingMode.PreserveChildReturnSingleDigits);
         ExpectedSequence = new List<string>(new string[] { "11", "110", "111", "112", "113", "114", "115", "116", "117" });
         Assert.IsTrue(ExpectedSequence.SequenceEqual(AreaList));

      }

      /// <summary>
      /// Asserts that AreaTransformer converts digits to string correctly.
      /// </summary>
      [TestMethod, Description("Asserts that AreaTransformer converts digits to string correctly")]
      public void AreaTransformer_CanConvertInBothDirectionCorrectly()
      {
         foreach (var p in Enum.GetValues(typeof(ParsingMode)))
         {
            AreaTransformer_CanConvertInBothDirectionCorrectly((ParsingMode)p);
         }
      }

      private void AreaTransformer_CanConvertInBothDirectionCorrectly(
ParsingMode ParsingMode)
      {
         char chSep = '+';
         char chDel = '&';
         //first conversion, from digits to string
         string sExpectedAreaString = "110&117+ 22";
         string sActualAreaString = AreaTransformer.TransformAreaDigitsToString((new int[] { 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 110, 111, 112, 113, 114, 115, 116, 117 })
            .Select(x => x.ToString()).ToList(), chSep, chDel, Compression.Full);
         Assert.AreEqual<string>(sExpectedAreaString, sActualAreaString);
         //second conversion, back to digits
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>(sActualAreaString, chSep, chDel, ParsingMode);
         List<string> ExpectedSequence = null;

         switch (ParsingMode)
         {
            case ParsingMode.EatChildReturnRanges:
            case ParsingMode.PreserveChildReturnRanges:
               ExpectedSequence = new List<string>(new string[] { "110&117", "22" });
               Assert.IsTrue(ExpectedSequence.SequenceEqual(AreaList));
               break;
            default:
               ExpectedSequence = new List<string>(new string[] { "110", "111", "112", "113", "114", "115", "116", "117", "22" });
               Assert.IsTrue(ExpectedSequence.SequenceEqual(AreaList));
               break;
         }
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, with special case with range '0-9'
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with special case with range '0-9'")]
      public void AreaTransformer_StringToDigitMakeCompactWithSpecialRange09()
      {
         foreach (var p in Enum.GetValues(typeof(ParsingMode)))
         {
            if (((int)p).Equals((int)ParsingMode.PreserveChildReturnRanges))
               continue;
            AreaTransformer_StringToDigitMakeCompactWithHeadingZeroes((ParsingMode)p);
         }
      }

      private void AreaTransformer_StringToDigitMakeCompactWithSpecialRange09(
ParsingMode ParsingMode)
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("0-9", ';', '-', ParsingMode);
         Assert.AreEqual<string>("0", AreaList.First());
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, starting with '0'
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, starting with '0'")]
      public void AreaTransformer_StringToDigitMakeCompactWithHeadingZeroes()
      {
         foreach (var p in Enum.GetValues(typeof(ParsingMode)))
         {
            AreaTransformer_StringToDigitMakeCompactWithHeadingZeroes((ParsingMode)p);
         }
      }

      private void AreaTransformer_StringToDigitMakeCompactWithHeadingZeroes(ParsingMode ParsingMode)
{
   bool bPrintRanges = (ParsingMode == ParsingMode.EatChildReturnRanges || ParsingMode == ParsingMode.PreserveChildReturnRanges);
   List<string> AreaList = AreaTransformer.GetAreasFromString<string>("00000000001-3;0000000000000000005;6", ';', '-', ParsingMode);
   string[] ExpectedSequence = (bPrintRanges ? new string[] { "1-3", "5-6" } :
      new string[] { "1", "2", "3", "5", "6" });
   if (ParsingMode == ParsingMode.PreserveChildReturnRanges)
   {
      ExpectedSequence = new string[] { "1-3", "5", "6" };
   }
   Assert.IsTrue(ExpectedSequence.SequenceEqual(AreaList));
}

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters")]
      public void AreaTransformer_StringToSingleDigit()
      {
         List<string> AreaList_ECSD = AreaTransformer.GetAreasFromString<string>("1, 2 ,3 -5", ',', '-', ParsingMode.EatChildReturnSingleDigits);
         List<string> AreaList_PCSD = AreaTransformer.GetAreasFromString<string>("1, 2 ,3 -5", ',', '-', ParsingMode.PreserveChildReturnSingleDigits);

         List<string> AreaList = AreaList_ECSD.Intersect(AreaList_PCSD).ToList();

         Assert.IsTrue(AreaList.Contains("1"), "1");
         Assert.IsTrue(AreaList.Contains("2"), "2");
         Assert.IsTrue(AreaList.Contains("3"), "3");
         Assert.IsTrue(AreaList.Contains("4"), "4");
         Assert.IsTrue(AreaList.Contains("5"), "5");

         Assert.IsFalse(AreaList.Contains("6"), "6");
         Assert.IsFalse(AreaList.Contains("7"), "7");
         Assert.IsFalse(AreaList.Contains("23"), "23");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters")]
      public void AreaTransformer_StringToDigitWrongRangeDelimiter()
      {
         List<string> AreaList_ECSD = AreaTransformer.GetAreasFromString<string>("1+2 +3L5", '+', 'L', ParsingMode.EatChildReturnSingleDigits);
         List<string> AreaList_PCSD = AreaTransformer.GetAreasFromString<string>("1+2 +3L5", '+', 'L', ParsingMode.PreserveChildReturnSingleDigits);

         List<string> AreaList = AreaList_ECSD.Intersect(AreaList_PCSD).ToList();

         Assert.IsTrue(AreaList.Contains("1"), "1");
         Assert.IsTrue(AreaList.Contains("2"), "2");
         Assert.IsTrue(AreaList.Contains("4"), "4");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_StringToDigitDefaultDelimiters()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("1; 2 ;3-5; 4-675", ';', '-', ParsingMode.EatChildReturnSingleDigits);

         Assert.IsTrue(AreaList.Contains("1"), "1");
         Assert.IsTrue(AreaList.Contains("2"), "2");
         Assert.IsTrue(AreaList.Contains("3"), "3");
         Assert.IsTrue(AreaList.Contains("4"), "4");
         Assert.IsTrue(AreaList.Contains("5"), "5");

         Assert.IsTrue(AreaList.Contains("63"), "63");
         Assert.IsTrue(AreaList.Contains("675"), "675");

         Assert.IsFalse(AreaList.Contains("6"), "6");
         Assert.IsFalse(AreaList.Contains("7"), "7");
         Assert.IsFalse(AreaList.Contains("23"), "23");
      }

      /// <summary>
      /// Ensures that ArrayContainSame returns false when checking two arrays that have same values
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer throws exception if any digit is not numeric"), ExpectedException(typeof(FormatException))]
      public void AreaTransformer_StringToDigitNonNumericException()
      {
         AreaTransformer.GetAreasFromString<string>("F;2", ';', '-', ParsingMode.EatChildReturnRanges);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer returns empty list if empty area string is provided
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer returns empty list if empty area string is provided")]
      public void AreaTransformer_StringToDigitEmptyString()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>(String.Empty, ',', '-', ParsingMode.EatChildReturnRanges);

         Assert.IsTrue(AreaList.Count == 0, "Area codes returned something, but shouldn't have.");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer returns empty list if area string is nothing
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer returns empty list if area string is nothing")]
      public void AreaTransformer_GetAreaFromStringWithNullInput()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>(null, ',', '-', ParsingMode.PreserveChildReturnRanges);

         Assert.IsTrue(AreaList.Count == 0, "Area codes returned something, but shouldn't have.");
      }

      /// <summary>
      /// Asserts that Area Transformer throws exception when not standard invalid range is passed.
      /// </summary>
      [TestMethod, Description("Asserts that Area Transformer throws exception when not standard invalid range is passed"), ExpectedException(typeof(ArgumentException), "Invalid area format passed.")]
      public void AreaTransformer_GetAreaFromStringWithInvalidNotStandardRange3()
      {
         AreaTransformer.GetAreasFromString<string>("343542326546-1", ',', '-', ParsingMode.EatChildReturnSingleDigits);
      }

      /// <summary>
      /// Asserts that Area Transformer throws exception when not standard invalid range is passed.
      /// </summary>
      [TestMethod, Description("Asserts that Area Transformer throws exception when not standard invalid range is passed"), ExpectedException(typeof(ArgumentException), "Invalid area format passed.")]
      public void AreaTransformer_GetAreaFromStringWithInvalidNotStandardRange1()
      {
         AreaTransformer.GetAreasFromString<string>("34-1", ',', '-', ParsingMode.PreserveChildReturnRanges);
      }

      /// <summary>
      /// Asserts that Area Transformer throws exception when not standard invalid range is passed.
      /// </summary>
      [TestMethod, Description("Asserts that Area Transformer throws exception when not standard invalid range is passed"), ExpectedException(typeof(ArgumentException), "Invalid area format passed.")]
      public void AreaTransformer_GetAreaFromStringWithInvalidNotStandardRange2()
      {
         AreaTransformer.GetAreasFromString<string>("3434-1", ',', '-', ParsingMode.EatChildReturnSingleDigits);
      }

      /// <summary>
      /// Asserts that Area Transformer computes the right transformation for not standard valid range is passed.
      /// </summary>
      [TestMethod, Description("Asserts that Area Transformer computes the right transformation for not standard valid range is passed")]
      public void AreaTransformer_StringToDigitIntervalMultipleDigits()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("1-123456789", ',', '-', ParsingMode.EatChildReturnSingleDigits);

         Assert.IsTrue(AreaList.Contains("10"), "10");
         Assert.IsTrue(AreaList.Contains("12342"), "12342");
         Assert.IsTrue(AreaList.Contains("12345678"), "12345678");

         //Should not have "1", since not complete "1" in the range. 13 and so on are not part of the area codes.
         Assert.IsFalse(AreaList.Contains("1"), "1");

         // should not have these area codes. The first one is because the area code is ending with 9 and therefore the complete parent should be part
         Assert.IsFalse(AreaList.Contains("123456782"), "123456782");
         Assert.IsFalse(AreaList.Contains("12346"), "12346");
      }
      #endregion

      #region "StringToDigits Without Compact"
      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters")]
      public void AreaTransformer_StringToDigitWithoutCompact()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("1, 2 ,3-5", ',', '-', ParsingMode.EatChildReturnSingleDigits);

         Assert.IsTrue(AreaList.Contains("1"), "1");
         Assert.IsTrue(AreaList.Contains("2"), "2");
         Assert.IsTrue(AreaList.Contains("3"), "3");
         Assert.IsTrue(AreaList.Contains("4"), "4");
         Assert.IsTrue(AreaList.Contains("5"), "5");

         Assert.IsFalse(AreaList.Contains("6"), "6");
         Assert.IsFalse(AreaList.Contains("7"), "7");
         Assert.IsFalse(AreaList.Contains("23"), "23");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with provided delimiters"), ExpectedException(typeof(FormatException), "Invalid area format passed.")]
      public void AreaTransformer_StringToDigitWithoutCompactMoreThan2Interval()
      {
         AreaTransformer.GetAreasFromString<string>("1-2-3", ';', '-', ParsingMode.PreserveChildReturnRanges);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer throws exception when range is in the wrong order
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer throws exception when range is in the wrong order"), ExpectedException(typeof(ArgumentException), "Invalid area format passed.")]
      public void AreaTransformer_StringToDigitWithoutCompactWrongOrderInterval()
      {
         AreaTransformer.GetAreasFromString<string>("3-1", ';', '-', ParsingMode.EatChildReturnRanges);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer works with non-standard delimiters, in this case '+' as sperator and 'L' as range
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer works with non-standard delimiters, in this case '+' as sperator and 'L' as range")]
      public void AreaTransformer_StringToDigitWithProvidedNonstandardDelimiters()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("1+ 2 +3L5", '+', 'L');
         Assert.IsTrue(AreaList.Contains("1"), "1");
         Assert.IsTrue(AreaList.Contains("2"), "2");
         Assert.IsTrue(AreaList.Contains("3"), "3");
         Assert.IsTrue(AreaList.Contains("4"), "4");
         Assert.IsTrue(AreaList.Contains("5"), "5");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer throws exception if any digit is not numeric
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer throws exception if any digit is not numeric"), ExpectedException(typeof(FormatException), "Invalid area format passed.")]
      public void AreaTransformer_StringToDigitWithoutCompactNonNumericException()
      {
         AreaTransformer.GetAreasFromString<string>("F;2", ';', '-');
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer parses correctly the single digits coming from non-conventional ranges.
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer parses correctly the single digits coming from non-conventional ranges")]
      public void AreaTransformer_StringToDigitWithProvidedNonstandardRangesWithExplosion()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("4-62; 688-8", ';', '-', ParsingMode.EatChildReturnSingleDigits);
         string[] ExpectedSequence = { "4", "5", "60", "61", "62", "688", "689", "69", "7", "8" };
         Assert.IsTrue(ExpectedSequence.SequenceEqual(AreaList), "Sequence of compressed or exploded non conventional ranges not returned correctly");
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer parses correctly the conventional ranges coming from non-conventional ranges.
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer parses correctly the conventional ranges coming from non-conventional ranges")]
      public void AreaTransformer_StringToDigitWithProvidedNonStandardRangesWithoutExplosion()
      {
         List<string> AreaList = AreaTransformer.GetAreasFromString<string>("4-62; 688-8", ';', '-', ParsingMode.EatChildReturnRanges);
         string[] ExpectedSequence = {
		"4-5",
		"60-62",
		"688-689",
		"69",
		"7-8"
	};
         Assert.IsTrue(ExpectedSequence.SequenceEqual(AreaList), "Sequence of compressed or exploded non conventional ranges not returned correctly");
      }

      #endregion

      #region "DigitToString - List"
      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with provided delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts Digits to String correctly, with provided delimiters")]
      public void AreaTransformer_DigitsToStringWithDelimiters()
      {
         List<string> AreaList = new List<string>();
         AreaList.Add("1");
         AreaList.Add("2");
         AreaList.Add("3");
         AreaList.Add("5");
         AreaList.Add("7");

         string AreaString = AreaTransformer.TransformAreaDigitsToString(AreaList, ',', '+', Compression.Full);

         Assert.AreEqual<string>("1+3, 5, 7", AreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, starting with '0'
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts Digits to String correctly, starting with '0'")]
      public void AreaTransformer_DigitsToStringMakeCompactWithHeadingZeros()
      {
         List<string> AreaList = new List<string>();
         AreaList.Add("000000000001");
         AreaList.Add("2");
         AreaList.Add("3");
         AreaList.Add("0000000000000000005");

         string AreaString = AreaTransformer.TransformAreaDigitsToString(AreaList, ';', '-', Compression.Full);
         // MAST 2013-07-02: Right now it is not compressing the area string. When that is changed, we need to change this too
         Assert.AreEqual<string>("1-3; 5", AreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts String to Digits correctly, according to the compression method selected.
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, according to the compression method selected")]
      public void AreaTransformer_DigitsToStringCompressionMethod()
      {
         List<string> AreaList = new List<string>();
         AreaList.Add("1");
         AreaList.Add("2");
         AreaList.Add("22");
         AreaList.Add("2567");
         AreaList.Add("2568");
         AreaList.Add("2569");
         AreaList.Add("1234");

         string AreaFull = AreaTransformer.TransformAreaDigitsToString(AreaList, ';', '-', Compression.Full);
         string AreaPreserve = AreaTransformer.TransformAreaDigitsToString(AreaList, ';', '-', Compression.Preserve);

         Assert.AreEqual<string>("1-2", AreaFull);
         Assert.AreEqual<string>("1; 1234; 2; 22; 2567-2569", AreaPreserve);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_DigitsToString()
      {
         List<string> AreaList = new List<string>();
         AreaList.Add("1");
         AreaList.Add("2");
         AreaList.Add("3");
         AreaList.Add("5");
         AreaList.Add("7");

         string sAreaString = AreaTransformer.TransformAreaDigitsToString(AreaList, ';', '-', Compression.Full);

         Assert.AreEqual<string>("1-3; 5; 7", sAreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_DigitsToStringFullRange()
      {
         List<string> AreaList = new List<string>();
         AreaList.Add("1");
         AreaList.Add("2");
         AreaList.Add("3");
         AreaList.Add("4");
         AreaList.Add("5");
         AreaList.Add("6");
         AreaList.Add("7");
         AreaList.Add("8");
         AreaList.Add("9");

         string sAreaString = AreaTransformer.TransformAreaDigitsToString(AreaList, ';', '-', Compression.Full);

         Assert.AreEqual<string>("1-9", sAreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_DigitsToStringEmptyInput()
      {
         List<string> AreaList = new List<string>();
         string sAreaString = AreaTransformer.TransformAreaDigitsToString(AreaList, ';', '-', Compression.Full);
         Assert.AreEqual<string>("", sAreaString);

         sAreaString = AreaTransformer.TransformAreaDigitsToString(null, ';', '-', Compression.Full);
         Assert.AreEqual<string>("", sAreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts breakout digits to String correctly, with area code starts with 0
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts breakout digits to String correctly, with area code starts with 0")]

      public void AreaTransformer_BreakoutDigitsToStringWithAreaCodeStartsWith0()
      {
         List<string> BreakoutList = new List<string>();
         BreakoutList.Add("2");
         BreakoutList.Add("3");
         BreakoutList.Add("01");
         BreakoutList.Add("02");
         BreakoutList.Add("03");
         BreakoutList.Add("4");
         BreakoutList.Add("05");
         BreakoutList.Add("06");
         BreakoutList.Add("7");
         BreakoutList.Add("07");
         BreakoutList.Add("08");
         BreakoutList.Add("10");
         BreakoutList.Add("11");

         string sAreaString = AreaTransformer.TransformAreaDigitsToString(BreakoutList, ';', '-', Compression.Full, false);

         Assert.AreEqual<string>("01-03; 05-08; 10-11; 2-4; 7", sAreaString);

      }

      #endregion

      #region "DigitToString - Rows"
      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with provided delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts Digits to String correctly, with provided delimiters")]
      public void AreaTransformer_DigitsToStringRowsWithDelimiters()
      {
         DataTable Areas = new DataTable();
         DataColumn AreaColumn = new DataColumn("AREA", typeof(string));
         Areas.Columns.Add(AreaColumn);
         Areas.Rows.Add("1");
         Areas.Rows.Add("2");
         Areas.Rows.Add("3");
         Areas.Rows.Add("5");
         Areas.Rows.Add("7");

         string sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), "AREA", ',', '+', Compression.Full);

         Assert.AreEqual<string>("1+3, 5, 7", sAreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_DigitsToStringRows()
      {
         DataTable Areas = new DataTable();
         DataColumn AreaColumn = new DataColumn("AREA", typeof(string));
         Areas.Columns.Add(AreaColumn);
         Areas.Rows.Add("1");
         Areas.Rows.Add("2");
         Areas.Rows.Add("3");
         Areas.Rows.Add("5");
         Areas.Rows.Add("7");

         string sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), "AREA", ';', '-', Compression.Full);

         Assert.AreEqual<string>("1-3; 5; 7", sAreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_DigitsToStringRowsFullRange()
      {
         DataTable Areas = new DataTable();
         DataColumn AreaColumn = new DataColumn("AREA", typeof(string));
         Areas.Columns.Add(AreaColumn);
         Areas.Rows.Add("1");
         Areas.Rows.Add("2");
         Areas.Rows.Add("3");
         Areas.Rows.Add("4");
         Areas.Rows.Add("5");
         Areas.Rows.Add("6");
         Areas.Rows.Add("7");
         Areas.Rows.Add("8");
         Areas.Rows.Add("9");

         string sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), "AREA", ';', '-', Compression.Full);

         Assert.AreEqual<string>("1-9", sAreaString);
      }


      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_DigitsToStringRowsWrongType()
      {
         DataTable Areas = new DataTable();
         DataColumn AreaColumn = new DataColumn("DATE", typeof(System.DateTime));
         Areas.Columns.Add(AreaColumn);
         Areas.Rows.Add(System.DateTime.Now);

         string sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), "DATE", ';', '-', Compression.Full);

         Assert.AreEqual<string>("", sAreaString);
      }

      /// <summary>
      /// Ensures that AreaCodeTransformer converts Digits to String correctly, with default delimiters
      /// </summary>
      [TestMethod, Description("Ensures that AreaCodeTransformer converts String to Digits correctly, with default delimiters")]
      public void AreaTransformer_DigitsToStringRowsEmptyInput()
      {
         DataTable Areas = new DataTable();
         DataColumn AreaColumn = new DataColumn("AREA", typeof(string));
         Areas.Columns.Add(AreaColumn);

         // Rows are empty 
         string sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), "AREA", ';', '-', Compression.Full);
         Assert.AreEqual<string>("", sAreaString);

         // Rows are nothing
         sAreaString = AreaTransformer.TransformAreaDigitsToString(null, "AREA", ';', '-', Compression.Full);
         Assert.AreEqual<string>("", sAreaString);

         // Column string is empty
         sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), "", ';', '-', Compression.Full);
         Assert.AreEqual<string>("", sAreaString);

         // Column is nothing
         DataColumn Column = null;
         sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), Column, ';', '-', Compression.Full);
         Assert.AreEqual<string>("", sAreaString);

         // Column is not part of the table
         // Need to send in some rows, otherwise it will not reach the point to check this
         Areas.Rows.Add("1");
         sAreaString = AreaTransformer.TransformAreaDigitsToString(Areas.Select(), "COLUMN_NOT_PART_OF_ROWS", ';', '-', Compression.Full);
         Assert.AreEqual<string>("", sAreaString);
      }
      #endregion
   }
}
