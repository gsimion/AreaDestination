using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AreaDestination;
using System.Collections.Generic;
using System.Linq;

namespace AreaDestinationTest
{
   [TestClass]
   public class RangeTest
   {

      #region "Implicit UInt64 to Decimal Conversion"

      /// <summary>
      /// Asserts that implicit conversion from long to decimal works just fine.
      /// </summary>
      [TestMethod, Description("Asserts that implicit conversion from long to decimal works just fine")]
      public void ZeroOneArea_DecimalImplicitConversion()
      {
         ulong l0 = 123456uL;
         ulong l1 = 1234567890uL;
         ulong l2 = ulong.MinValue;
         ulong l3 = 2uL;
         ulong l4 = 9999999999999999uL;
         ulong l5 = 1844674407370955161uL;
         ulong l6 = 1500uL;

         decimal dc0 = ZeroOneAreaRange.ConvertImplicitly(l0);
         decimal dc1 = ZeroOneAreaRange.ConvertImplicitly(l1);
         decimal dc2 = ZeroOneAreaRange.ConvertImplicitly(l2);
         decimal dc3 = ZeroOneAreaRange.ConvertImplicitly(l3);
         decimal dc4 = ZeroOneAreaRange.ConvertImplicitly(l4);
         decimal dc5 = ZeroOneAreaRange.ConvertImplicitly(l5);
         decimal dc6 = ZeroOneAreaRange.ConvertImplicitly(l6);

         Assert.AreEqual<decimal>(0.123456m, dc0);
         Assert.AreEqual<decimal>(0.123456789m, dc1);
         Assert.AreEqual<decimal>(0m, dc2);
         Assert.AreEqual<string>("0.0", dc2.ToString());
         Assert.AreEqual<decimal>(0.2m, dc3);
         Assert.AreEqual<decimal>(0.9999999999999999m, dc4);
         Assert.AreEqual<decimal>(0.1844674407370955161m, dc5);
         Assert.AreEqual<string>("0.1500", dc6.ToString());
      }

      #endregion

      #region "Implicit Decimal to UInt64 Conversion"

      /// <summary>
      /// Asserts that implicit conversion from decimal to long works just fine.
      /// </summary>
      [TestMethod, Description("Asserts that implicit conversion from decimal to long works just fine")]
      public void ZeroOneArea_ULongImplicitConversion()
      {
         decimal dc0 = 0.123456m;
         decimal dc1 = 0.123456789m;
         decimal dc2 = Convert.ToDecimal("0.0");
         decimal dc3 = 0.2m;
         decimal dc4 = 0.9999999999999999m;
         decimal dc5 = 0.1844674407370955161m;
         decimal dc6 = Convert.ToDecimal("0.1500");

         ulong l0 = ZeroOneAreaRange.ConvertImplicitly(dc0);
         ulong l1 = ZeroOneAreaRange.ConvertImplicitly(dc1);
         ulong l2 = ZeroOneAreaRange.ConvertImplicitly(dc2);
         ulong l3 = ZeroOneAreaRange.ConvertImplicitly(dc3);
         ulong l4 = ZeroOneAreaRange.ConvertImplicitly(dc4);
         ulong l5 = ZeroOneAreaRange.ConvertImplicitly(dc5);
         ulong l6 = ZeroOneAreaRange.ConvertImplicitly(dc6);

         Assert.AreEqual<ulong>(123456uL, l0);
         Assert.AreEqual<ulong>(123456789uL, l1);
         Assert.AreEqual<ulong>(0uL, l2);
         Assert.AreEqual<ulong>(0uL, l2);
         Assert.AreEqual<ulong>(2uL, l3);
         Assert.AreEqual<ulong>(9999999999999999uL, l4);
         Assert.AreEqual<ulong>(1844674407370955161uL, l5);
         Assert.AreEqual<ulong>(15uL, l6);
      }

      #endregion

      #region "ZeroOneAreaRange"

      /// <summary>
      /// Assers that in range method returns the correct output.
      /// </summary>
      [TestMethod, Description("Assers that in range method returns the correct output")]
      public void ZeroOneArea_InRangeMethodWithIntersections()
      {
         ZeroOneAreaRange Area = new ZeroOneAreaRange(0.1m, 0.3m);

         Assert.IsTrue(Area.InRange(0.2m, true));
         Assert.IsTrue(Area.InRange(2uL, true));
         Assert.IsTrue(Area.InRange(0.1m, true));
         Assert.IsTrue(Area.InRange(1uL, true));
         Assert.IsTrue(Area.InRange(0.1234m, true));
         Assert.IsTrue(Area.InRange(1234uL, true));
         Assert.IsTrue(Area.InRange(0.3m, true));
         Assert.IsTrue(Area.InRange(3uL, true));
         Assert.IsTrue(Area.InRange(new ZeroOneAreaRange(0.1m, 0.3m), true));
         Assert.IsTrue(Area.InRange(new ZeroOneAreaRange(0.2m, 0.4m), true));
         Assert.IsTrue(Area.InRange(new ZeroOneAreaRange(0m, 0.1m), true));
         Assert.IsTrue(Area.InRange(new ZeroOneAreaRange(0.3m, 0.4m), true));
         Assert.IsTrue(Area.InRange(new ZeroOneAreaRange(0m, 0.4m), true));
         Assert.IsFalse(Area.InRange(0.3234m, true));
         Assert.IsFalse(Area.InRange(3234uL, true));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0.0001m, 0.09999m), true));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0.4m, 0.5m), true));
      }

      /// <summary>
      /// Assers that in range method returns the correct output.
      /// </summary>
      [TestMethod, Description("Assers that in range method returns the correct output")]
      public void ZeroOneArea_InRangeMethodWithoutIntersections()
      {
         ZeroOneAreaRange Area = new ZeroOneAreaRange(0.1m, 0.3m);

         Assert.IsTrue(Area.InRange(0.2m, false));
         Assert.IsTrue(Area.InRange(2uL, false));
         Assert.IsTrue(Area.InRange(0.1m, false));
         Assert.IsTrue(Area.InRange(1uL, false));
         Assert.IsTrue(Area.InRange(0.1234m, false));
         Assert.IsTrue(Area.InRange(1234uL, false));
         Assert.IsFalse(Area.InRange(0.3m, false));
         Assert.IsFalse(Area.InRange(3uL, false));
         Assert.IsTrue(Area.InRange(new ZeroOneAreaRange(0.1m, 0.3m), false));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0.2m, 0.4m), false));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0m, 0.1m), false));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0.3m, 0.4m), false));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0m, 0.4m), false));
         Assert.IsFalse(Area.InRange(0.3234m, false));
         Assert.IsFalse(Area.InRange(3234uL, false));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0.0001m, 0.09999m), false));
         Assert.IsFalse(Area.InRange(new ZeroOneAreaRange(0.4m, 0.5m), false));
      }


      #endregion

      #region "Area - Constructor"
      /// <summary>
      /// Asserts that single area digit creates the right range.
      /// </summary>
      [TestMethod, Description("Asserts that single area digit creates the right range")]
      public void Area_WithSingleDigitCanBeCreatedCorrectly()
      {
         Area<string> Area = new Area<string>("A", 1234);
         Assert.AreEqual<string>("A", Area.Id);
         Assert.AreEqual<string>("1234", Area.ToString());
         Assert.AreEqual<decimal>(0.1234m, Area.Start);
         Assert.AreEqual<decimal>(0.1235m, Area.End);
      }

      /// <summary>
      /// Asserts that single area digit creates the right range.
      /// </summary>
      [TestMethod, Description("Asserts that single area digit creates the right range")]
      public void Area_WithSingleDigitAsStringCanBeCreatedCorrectly()
      {
         Area<string> Area = new Area<string>("A", "1234");
         Assert.AreEqual<string>("A", Area.Id);
         Assert.AreEqual<string>("1234", Area.ToString());
         Assert.AreEqual<decimal>(0.1234m, Area.Start);
         Assert.AreEqual<decimal>(0.1235m, Area.End);
      }

      /// <summary>
      /// Asserts that a range can be created given lower and upper band with leading zeros.
      /// </summary>
      [TestMethod, Description("Asserts that a range can be created given lower and upper band with leading zeros")]
      public void Area_WithSingleDigitStartingWithZeroCanBeCreatedCorrectly()
      {
         Area<string> Area = new Area<string>("A", "01234", "01235");
         Assert.AreEqual<string>("A", Area.Id);
         Assert.AreEqual<string>("01234-01235", Area.ToString());
         Assert.AreEqual<decimal>(0.01234m, Area.Start);
         Assert.AreEqual<decimal>(0.01236m, Area.End);
      }

      /// <summary>
      /// Asserts that a range can be created given lower and upper band.
      /// </summary>
      [TestMethod, Description("Asserts that a range can be created given lower and upper band")]
      public void Area_WithTwoDigitsCanBeCreatedCorrectly()
      {
         Area<string> Area = new Area<string>("A", "1234", "1236");
         Assert.AreEqual<string>("A", Area.Id);
         Assert.AreEqual<string>("1234-1236", Area.ToString());
         Assert.AreEqual<decimal>(0.1234m, Area.Start);
         Assert.AreEqual<decimal>(0.1237m, Area.End);
      }

      /// <summary>
      /// Asserts that a range can be created correctly for areas having one digit.
      /// </summary>
      [TestMethod, Description("Asserts that a range can be created correctly for areas having one digit")]
      public void AreaRange_UniqueRange()
      {
         Area<string> Area = new Area<string>("a", 1);
         Assert.AreEqual("a", Area.Id);
         Assert.AreEqual(0.1m, Area.Start);
         Assert.AreEqual(0.2m, Area.End);
      }

      /// <summary>
      /// Asserts that a full range can be created.
      /// </summary>
      [TestMethod, Description("Asserts that a full range can be created")]
      public void AreaRange_ZeroOne()
      {
         Area<string> Area = new Area<string>("a", decimal.Zero, decimal.One);
         Assert.AreEqual("a", Area.Id);
         Assert.AreEqual(0m, Area.Start);
         Assert.AreEqual(1m, Area.End);
         Assert.AreEqual<string>("0-9", Area.ToString());
      }

      /// <summary>
      /// Asserts that a range can be created correctly for areas having one digit equal to zero.
      /// </summary>
      [TestMethod, Description("Asserts that a range can be created correctly for areas having one digit equal to zero")]
      public void AreaRange_Range_Zero()
      {
         Area<string> Area = new Area<string>("a", 0);
         Assert.AreEqual("a", Area.Id);
         Assert.AreEqual(0m, Area.Start);
         Assert.AreEqual(0.1m, Area.End);
      }

      /// <summary>
      /// Asserts that a range can be created correctly for areas having one digit equal to nine.
      /// </summary>
      [TestMethod, Description("Asserts that a range can be created correctly for areas having one digit equal to nine")]
      public void AreaRange_Range_Nine()
      {
         Area<string> Area = new Area<string>("a", 9);
         Assert.AreEqual("a", Area.Id);
         Assert.AreEqual(0.9m, Area.Start);
         Assert.AreEqual(1m, Area.End);
      }

      /// <summary>
      /// Asserts that a range upper boundary is correct when having repeated nines.
      /// </summary>
      [TestMethod, Description("Asserts that a range upper boundary is correct when having repeated nines")]
      public void AreaRange_Range_NineRepeated()
      {
         Area<string> Area = new Area<string>("a", 99999999999L);
         Assert.AreEqual("a", Area.Id);
         Assert.AreEqual(0.99999999999m, Area.Start);
         Assert.AreEqual(1m, Area.End);
      }

      /// <summary>
      /// Asserts that it is not possible to create a range when lower band is greater than upper band.
      /// </summary>
      [TestMethod, Description("Asserts that it is not possible to create a range when lower band is greater than upper band"), ExpectedException(typeof(ArgumentException), "End value must be greater than start value")]
      public void AreaRange_Invalid_Range()
      {
         Area<string> Area = new Area<string>("a", 2, 1);
      }

      /// <summary>
      /// Asserts that it is not possible to create a range when lower band is greater than upper band.
      /// </summary>
      [TestMethod, Description("Asserts that it is not possible to create a range when lower band is greater than upper band"), ExpectedException(typeof(ArgumentException), "End value must be greater than start value")]
      public void AreaRangeString_Invalid_Range()
      {
         Area<string> Area = new Area<string>("a", "2", "1");
      }

      #endregion

      #region "Area - ToString"
      /// <summary>
      /// Asserts that single area is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that single area is printed correctly")]
      public void AreaRange_ToString_SingleArea()
      {
         Area<string> a = new Area<string>("a", 923);
         Assert.AreEqual(a.ToString(), "923");
      }

      /// <summary>
      /// Asserts that single area is ending with zeros is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that single area is ending with zeros is printed correctly")]
      public void AreaRange_ToString_SingleArea_EndingWithZeros()
      {
         Area<string> a = new Area<string>("a", "9232200000");
         Assert.AreEqual(a.ToString(), "9232200000");
      }

      /// <summary>
      /// Asserts that area range is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that area range is printed correctly")]
      public void AreaRange_ToString_AreaRange()
      {
         Area<string> a = new Area<string>("a", new Decimal(0.234), new Decimal(0.235));
         Assert.AreEqual(a.ToString(), "234");
      }

      /// <summary>
      /// Asserts that area full range is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that area full range is printed correctly")]
      public void AreaRange_ToString_AreaFullRange1()
      {
         Area<string> a = new Area<string>("a", "10", "19");
         Assert.AreEqual<decimal>(0.1m, a.Start);
         Assert.AreEqual<decimal>(0.2m, a.End);
         Assert.AreEqual(a.ToString(), "1");
      }

      /// <summary>
      /// Asserts that area full range is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that area full range is printed correctly")]
      public void AreaRange_ToString_AreaFullRange2()
      {
         Area<string> b = new Area<string>("a", "555");
         Assert.AreEqual<decimal>(0.555m, b.Start);
         Assert.AreEqual<decimal>(0.556m, b.End);
         Assert.AreEqual(b.ToString(), "555");

         Area<string> a = new Area<string>("a", "5550", "5559");
         Assert.AreEqual<decimal>(0.555m, a.Start);
         Assert.AreEqual<decimal>(0.556m, a.End);
         Assert.AreEqual(a.ToString(), "555");
      }

      /// <summary>
      /// Asserts that area full range is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that area full range is printed correctly")]
      public void AreaRange_ToString_AreaFullRange_EndingWithZeros()
      {
         Area<string> a = new Area<string>("a", "10000000000", "10000000003");
         Assert.AreEqual<decimal>(0.1m, a.Start);
         Assert.AreEqual<decimal>(0.10000000004m, a.End);
         Assert.AreEqual(a.ToString(), "10000000000-10000000003");
      }

      /// <summary>
      /// Asserts that area full range is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that area full range is printed correctly")]
      public void AreaRange_ToString_AreaRange_StartShorter()
      {
         Area<string> a = new Area<string>("a", new Decimal(0.23), new Decimal(0.235));
         Assert.AreEqual(a.ToString(), "230-234");
      }

      /// <summary>
      /// Asserts that area full range is printed correctly.
      /// </summary>
      [TestMethod, Description("Asserts that area full range is printed correctly")]
      public void AreaRange_ToString_AreaRange_EndShorter()
      {
         Area<string> a = new Area<string>("a", new Decimal(0.234), new Decimal(0.26));
         Assert.AreEqual(a.ToString(), "234-259");
      }
      #endregion
   }
}
