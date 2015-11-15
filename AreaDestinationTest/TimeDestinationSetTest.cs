using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AreaDestination;
using System.Collections.Generic;
using System.Linq;

namespace AreaDestinationTest
{
   [TestClass]
   public class TimeDestinationSetTest
   {

      /// <summary>
      /// Asserts that right areas for a destination are returned on the right date.
      /// </summary>
      [TestMethod, Description("Asserts that right areas for a destination are returned on the right date")]
      public void TimeDestinationSet_Basic()
      {
         TimeDestinationSet<string> ds = new TimeDestinationSet<string>();
         ds.AddArea("a", 331, new DateTime(2014, 8, 1), DateTime.MaxValue);
         ds.AddArea("a", 335, new DateTime(2014, 8, 8), DateTime.MaxValue);
         Destination<string> d = ds.GetDestinationAtDate("a", new DateTime(2014, 8, 2));
         Assert.AreEqual("331", d.ToString());
         d = ds.GetDestinationAtDate("a", new DateTime(2014, 8, 10));
         Assert.AreEqual("331,335", d.ToString());
      }

      /// <summary>
      /// Asserts that no area is returned for a missing validity period.
      /// </summary>
      [TestMethod, Description("Asserts that no area is returned for a missing validity period")]
      public void TimeDestinationSet_NoArea()
      {
         TimeDestinationSet<string> ds = new TimeDestinationSet<string>();
         ds.AddArea("a", 331, new DateTime(2014, 8, 1), new DateTime(2014, 8, 5));
         ds.AddArea("a", 335, new DateTime(2014, 8, 8), DateTime.MaxValue);
         Destination<string> d = ds.GetDestinationAtDate("a", new DateTime(2014, 8, 6));
         Assert.AreEqual(string.Empty, d.ToString(), "No areas expected for destination 'a' at date 06-08-2014");
      }

      /// <summary>
      /// Asserts that validity boundaries are returned correctly for time destination set.
      /// </summary>
      [TestMethod, Description("Asserts that validity boundaries are returned correctly for time destination set")]
      public void TimeDestinationSet_Boundary()
      {
         TimeDestinationSet<string> ds = new TimeDestinationSet<string>();
         ds.AddArea("a", 331, new DateTime(2014, 8, 1), new DateTime(2014, 8, 5));
         ds.AddArea("a", 335, new DateTime(2014, 8, 8), DateTime.MaxValue);
         Destination<string> d = default(Destination<string>);
         d = ds.GetDestinationAtDate("a", new DateTime(2014, 8, 5));
         Assert.AreEqual<string>(string.Empty, d.ToString());
         d = ds.GetDestinationAtDate("a", new DateTime(2014, 8, 8));
         Assert.AreEqual<string>("335", d.ToString());
         d = ds.GetDestinationAtDate("a", new DateTime(2014, 8, 7));
         Assert.AreEqual<string>(string.Empty, d.ToString());
         d = ds.GetDestinationAtDate("a", new DateTime(2014, 8, 4));
         Assert.AreEqual<string>("331", d.ToString());
      }

      /// <summary>
      /// Asserts that validity periods are successfully created for the same destination.
      /// </summary>
      [TestMethod, Description("Asserts that validity periods are successfully created for the same destination")]
      public void TimeDestinationSet_DestinationValidityPeriods_SameDestination()
      {
         TimeDestinationSet<string> TimeDestinationSet = new TimeDestinationSet<string>();
         KeyValuePair<DateTime, DateTime> ValidPeriod1 = new KeyValuePair<DateTime, DateTime>(new DateTime(2014, 8, 1), new DateTime(2014, 8, 5));
         KeyValuePair<DateTime, DateTime> ValidPeriod2 = new KeyValuePair<DateTime, DateTime>(new DateTime(2014, 8, 8), DateTime.MaxValue.Date);
         TimeDestinationSet.AddArea("a", 331, ValidPeriod1.Key, ValidPeriod1.Value);
         TimeDestinationSet.AddArea("a", 335, ValidPeriod2.Key, ValidPeriod2.Value);
         List<KeyValuePair<bool, DateRange>> ValidityPeriods = TimeDestinationSet.GetDestinationHistory("a");
         Assert.AreEqual<int>(4, ValidityPeriods.Count, "Total validity periods created");
         //first period: valid from & valid until
         Assert.IsFalse(ValidityPeriods[0].Key);
         Assert.AreEqual<DateTime>(DateTime.MinValue.Date, ValidityPeriods[0].Value.Start, "Start of the first period");
         Assert.AreEqual<DateTime>(ValidPeriod1.Key, ValidityPeriods[0].Value.End, "End of the first period");
         //second period: valid from & valid until
         Assert.IsTrue(ValidityPeriods[1].Key);
         Assert.AreEqual<DateTime>(ValidPeriod1.Key, ValidityPeriods[1].Value.Start, "Start of the second period");
         Assert.AreEqual<DateTime>(ValidPeriod1.Value, ValidityPeriods[1].Value.End, "End of the second period");
         //third period: valid from & valid until
         Assert.IsFalse(ValidityPeriods[2].Key);
         Assert.AreEqual<DateTime>(ValidPeriod1.Value, ValidityPeriods[2].Value.Start, "Start of the third period");
         Assert.AreEqual<DateTime>(ValidPeriod2.Key, ValidityPeriods[2].Value.End, "End of the third period");
         //fourth period: valid from & valid until
         Assert.IsTrue(ValidityPeriods[3].Key);
         Assert.AreEqual<DateTime>(ValidPeriod2.Key, ValidityPeriods[3].Value.Start, "Start of the fourth period");
         Assert.AreEqual<DateTime>(ValidPeriod2.Value, ValidityPeriods[3].Value.End, "End of the fourth period");
      }
   }
}
