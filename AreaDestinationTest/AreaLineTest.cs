namespace AreaDestinationTest
{
   using System;
   using Microsoft.VisualStudio.TestTools.UnitTesting;
   using AreaDestination;
   using System.Collections.Generic;
   using System.Linq;

   [TestClass]
   public class AreaLineTest
   {
      /// <summary>
      /// Mock class calling protected method used for testing purposes.
      /// </summary>
      private class AreaLineMock : AreaLine<string>
      {
         /// <summary>
         /// Get explicit areas calling the base class method.
         /// </summary>
         /// <returns>Explicit areas</returns>
         public new IEnumerable<IArea<string>> GetExplicitAreas()
         {
            return base.GetExplicitAreas();
         }
      }

      #region "Add Area"

      [TestMethod, Description("Asserts that simple areas can be added")]
      public void AreaLine_AddSingleDistinctAreasWithSameDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Add(0.3m, 0.4m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(2, AreaList.Count);
         Assert.AreEqual<string>("1", AreaList[0].ToString());
         Assert.AreEqual<string>("3", AreaList[1].ToString());
      }

      [TestMethod, Description("Asserts that generic areas can be added")]
      public void AreaLine_AddSingleDistinctAreaRangesWithSameDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.3m);
         AreaLine.Add(0.4m, 0.6m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(2, AreaList.Count);
         Assert.AreEqual<string>("1-2", AreaList[0].ToString());
         Assert.AreEqual<string>("4-5", AreaList[1].ToString());
      }

      [TestMethod, Description("Asserts that generic areas can be added")]
      public void AreaLine_AddSingleDistinctAreasWithDifferentDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Add(0.4233m, 0.4234m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(2, AreaList.Count);
         Assert.AreEqual<string>("1", AreaList[0].ToString());
         Assert.AreEqual<string>("4233", AreaList[1].ToString());
      }

      [TestMethod, Description("Asserts that generic areas can be added")]
      public void AreaLine_AddSingleDistinctAreaRangesWithDifferentDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.3m);
         AreaLine.Add(0.433m, 0.438m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(2, AreaList.Count);
         Assert.AreEqual<string>("1-2", AreaList[0].ToString());
         Assert.AreEqual<string>("433-437", AreaList[1].ToString());
      }

      [TestMethod, Description("Asserts that generic areas can be added")]
      public void AreaLine_AddSingleConsecutiveAreasWithSameDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Add(0.2m, 0.3m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("1-2", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that generic consecutive areas with same precision can be added")]
      public void AreaLine_AddSingleConsecutiveAreaRangesWithSameDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.3m);
         AreaLine.Add(0.3m, 0.8m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("1-7", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding areas that are already included does not change the range")]
      public void AreaLine_AddSingleAlreadyIncludedAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Add(0.12m, 0.13m);
         AreaLine.Add(0.14m, 0.15m);
         AreaLine.Add(0.161234m, 0.161235m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("1", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding areas that are already included does not change the range")]
      public void AreaLine_AddMultipleAlreadyIncludedAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Add(0.12m, 0.13m);
         AreaLine.Add(0.14m, 0.15m);
         AreaLine.Add(0.161234m, 0.161235m);
         AreaLine.Add(0.3m, 0.4m);
         AreaLine.Add(0.361234m, 0.361235m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(2, AreaList.Count);
         Assert.AreEqual<string>("1", AreaList[0].ToString());
         Assert.AreEqual<string>("3", AreaList[1].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddSingleStartOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.28m);
         AreaLine.Add(0.24m, 0.26m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("24-27", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddMultipleStartOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.28m);
         AreaLine.Add(0.24m, 0.26m);
         AreaLine.Add(0.23m, 0.28m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("23-27", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddSingleEndOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.28m);
         AreaLine.Add(0.26m, 0.29m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("25-28", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddMultipleEndOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.27m);
         AreaLine.Add(0.26m, 0.28m);
         AreaLine.Add(0.27m, 0.29m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("25-28", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddMultipleMixedOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.27m);
         AreaLine.Add(0.26m, 0.28m);
         AreaLine.Add(0.23m, 0.26m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("23-27", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddTotallyOverlappingSingleArea()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.27m);
         AreaLine.Add(0.2m, 0.3m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("2", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddTotallyOverlappingMultipleArea()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.257m);
         AreaLine.Add(0.258m, 0.259m);
         AreaLine.Add(0.255m, 0.256m);
         AreaLine.Add(0.25m, 0.26m);
         AreaLine.Add(0.2m, 0.3m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("2", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddTotallyOverlappingMultipleAreaToBeMerge()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.3m, 0.4m);
         AreaLine.Add(0.256m, 0.257m);
         AreaLine.Add(0.25m, 0.26m);
         AreaLine.Add(0.2m, 0.3m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("2-3", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddSameSingleArea()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.257m);
         AreaLine.Add(0.256m, 0.257m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("256", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddSameAreaRange()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.258m);
         AreaLine.Add(0.256m, 0.258m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("256-257", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding overlapping areas produces the merge")]
      public void AreaLine_AddForConcatenation()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.3m, 0.4m);
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Add(0.2m, 0.3m);

         List<ZeroOneDecimalRange> AreaList = AreaLine.GetRanges().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<decimal>(0.1m, AreaList[0].Start);
         Assert.AreEqual<decimal>(0.4m, AreaList[0].End);
      }

      [TestMethod, Description("Asserts that adding areas including single zero is handled correctly")]
      public void AreaLine_AddWithEndIncludingZero()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.457m, 0.45701m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("45700", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding areas including zeros is handled correctly")]
      public void AreaLine_AddWithEndIncludingZeros()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.457m, 0.45700001m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("45700000", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding areas including zeros is handled correctly")]
      public void AreaLine_AddWithEndIncludingZerosMore()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.457m, 0.45700002m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("45700000-45700001", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that adding areas including zeros is handled correctly")]
      public void AreaLine_AddWithEndIncludingZerosExtended()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.457m, 0.45700004m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("45700000-45700003", AreaList[0].ToString());
      }

      #endregion

      #region "Remove Area"

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveSingleDistinctSameAreasWithSameDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Remove(0.1m, 0.2m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(0, AreaList.Count);
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_AddAndRemoveSingleDistinctSameAreasWithSameDecimalsEdge1()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);
         AreaLine.Remove(0.3m, 0.4m);
         AreaLine.Add(0.3m, 0.4m);

         List<ZeroOneDecimalRange> AreaList = AreaLine.GetRanges().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<decimal>(0.1m, AreaList[0].Start);
         Assert.AreEqual<decimal>(0.4m, AreaList[0].End);
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_AddAndRemoveSingleDistinctSameAreasWithSameDecimalsEdge2()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);
         AreaLine.Remove(0.1m, 0.2m);
         AreaLine.Add(0.1m, 0.2m);

         List<ZeroOneDecimalRange> AreaList = AreaLine.GetRanges().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<decimal>(0.1m, AreaList[0].Start);
         Assert.AreEqual<decimal>(0.4m, AreaList[0].End);
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_AddAndRemoveSingleDistinctSameAreasWithSameDecimalsMiddle()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);
         AreaLine.Remove(0.2m, 0.3m);
         AreaLine.Add(0.2m, 0.3m);

         List<ZeroOneDecimalRange> AreaList = AreaLine.GetRanges().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<decimal>(0.1m, AreaList[0].Start);
         Assert.AreEqual<decimal>(0.4m, AreaList[0].End);
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveSingleDistinctDifferentAreaWithSameDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.3m);
         AreaLine.Remove(0.4m, 0.6m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("1-2", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveSingleDistinctSameAreasWithDifferentDecimals()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);
         AreaLine.Remove(0.4233m, 0.4234m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("1", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveSingleStartOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.28m);
         AreaLine.Remove(0.24m, 0.26m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("26-27", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveMultipleStartOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.28m);
         AreaLine.Remove(0.24m, 0.26m);
         AreaLine.Remove(0.23m, 0.27m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("27", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveSingleEndOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.28m);
         AreaLine.Remove(0.26m, 0.29m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("25", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveMultipleEndOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.28m);
         AreaLine.Remove(0.27m, 0.28m);
         AreaLine.Remove(0.26m, 0.29m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("25", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveMultipleMixedOverlappingAreas()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.24m, 0.27m);
         AreaLine.Remove(0.22m, 0.25m);
         AreaLine.Remove(0.26m, 0.29m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("25", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveTotallyOverlappingSingleArea()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.25m, 0.27m);
         AreaLine.Remove(0.2m, 0.3m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(0, AreaList.Count);
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveTotallyOverlappingMultipleArea()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.257m);
         AreaLine.Add(0.258m, 0.259m);
         AreaLine.Add(0.255m, 0.256m);
         AreaLine.Remove(0.25m, 0.26m);
         AreaLine.Add(0.2m, 0.3m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("2", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveTotallyOverlappingMultipleAreaToBeSplit()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.2m, 0.8m);
         AreaLine.Remove(0.4m, 0.6m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(2, AreaList.Count);
         Assert.AreEqual<string>("2-3", AreaList[0].ToString());
         Assert.AreEqual<string>("6-7", AreaList[1].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveSameAreaRange()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.258m);
         AreaLine.Remove(0.256m, 0.258m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(0, AreaList.Count);
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveBoundariesAreaRange()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.259m);
         AreaLine.Remove(0.256m, 0.257m);
         AreaLine.Remove(0.258m, 0.259m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(1, AreaList.Count);
         Assert.AreEqual<string>("257", AreaList[0].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveCompleteAreaRangeInMultipleSteps()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.259m);
         AreaLine.Remove(0.256m, 0.257m);
         AreaLine.Remove(0.257m, 0.259m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(0, AreaList.Count);
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveInnerAreaRangeOneDigitDistance()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.256m, 0.259m);
         AreaLine.Remove(0.2565m, 0.2579m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual(3, AreaList.Count);
         Assert.AreEqual<string>("2560-2564", AreaList[0].ToString());
         Assert.AreEqual<string>("2579", AreaList[1].ToString());
         Assert.AreEqual<string>("258", AreaList[2].ToString());
      }

      [TestMethod, Description("Asserts that remove areas is handled correctly")]
      public void AreaLine_RemoveInnerMultipleAreaRangeMoreThanOneDigitDistance()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.3m);
         AreaLine.Add(0.266m, 0.269m);
         AreaLine.Remove(0.2565m, 0.2579m);
         AreaLine.Remove(0.276577m, 0.277579m);

         List<ZeroOneDecimalRange> RangeList = AreaLine.GetRanges().ToList();
         Assert.AreEqual<decimal>(0.1m, RangeList[0].Start);
         Assert.AreEqual<decimal>(0.2565m, RangeList[0].End);
         Assert.AreEqual<decimal>(0.2579m, RangeList[1].Start);
         Assert.AreEqual<decimal>(0.276577m, RangeList[1].End);
         Assert.AreEqual<decimal>(0.277579m, RangeList[2].Start);
         Assert.AreEqual<decimal>(0.3m, RangeList[2].End);
      }

      #endregion

      #region "Explicit area representation"

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple1()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.2m;
         decimal b = 0.4m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();


         Assert.AreEqual<int>(1, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.2m, 0.4m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple2()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.2m;
         decimal b = 0.23m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(1, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.2m, 0.23m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple3()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.2m;
         decimal b = 0.234m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(2, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.2m, 0.23m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.23m, 0.234m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple4()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.2m;
         decimal b = 0.34m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(2, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.2m, 0.3m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.3m, 0.34m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple5()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.21m;
         decimal b = 0.345m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(3, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.21m, 0.3m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.3m, 0.34m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.34m, 0.345m), new KeyValuePair<decimal, decimal>(list[2].Start, list[2].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple6()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.21m;
         decimal b = 0.3m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(1, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.21m, 0.3m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple7()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.212m;
         decimal b = 0.3m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(2, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.212m, 0.22m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.22m, 0.3m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple8()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.212m;
         decimal b = 0.4m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(3, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.212m, 0.22m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.22m, 0.3m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.3m, 0.4m), new KeyValuePair<decimal, decimal>(list[2].Start, list[2].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple9()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.212m;
         decimal b = 0.41m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(4, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.212m, 0.22m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.22m, 0.3m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.3m, 0.4m), new KeyValuePair<decimal, decimal>(list[2].Start, list[2].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.4m, 0.41m), new KeyValuePair<decimal, decimal>(list[3].Start, list[3].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple10()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.299m;
         decimal b = 0.31m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(2, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.299m, 0.3m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.3m, 0.31m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple11()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.2m;
         decimal b = 0.23456m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(4, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.2m, 0.23m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.23m, 0.234m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.234m, 0.2345m), new KeyValuePair<decimal, decimal>(list[2].Start, list[2].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.2345m, 0.23456m), new KeyValuePair<decimal, decimal>(list[3].Start, list[3].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_Explode_simple12()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         decimal a = 0.23456m;
         decimal b = 0.3m;
         AreaLine.Add(a, b);
         List<IArea<string>> list = AreaLine.GetExplicitAreas().ToList();

         Assert.AreEqual<int>(4, list.Count);
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.23456m, 0.2346m), new KeyValuePair<decimal, decimal>(list[0].Start, list[0].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.2346m, 0.235m), new KeyValuePair<decimal, decimal>(list[1].Start, list[1].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.235m, 0.24m), new KeyValuePair<decimal, decimal>(list[2].Start, list[2].End));
         Assert.AreEqual<KeyValuePair<decimal, decimal>>(new KeyValuePair<decimal, decimal>(0.24m, 0.3m), new KeyValuePair<decimal, decimal>(list[3].Start, list[3].End));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation")]
      public void AreaLine_GetExplicitAreaRepresentationCanExplodeAreasCorrectlyCaseA()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2345m);
         AreaLine.Add(0.345m, 0.3456789m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual<int>(8, AreaList.Count);
         Assert.IsTrue((new decimal[] { 0.1m, 0.2m, 0.2m, 0.23m, 0.23m, 0.234m, 0.234m, 0.2345m })
            .SequenceEqual(
            new decimal[] { AreaList[0].Start, AreaList[0].End, AreaList[1].Start, AreaList[1].End, AreaList[2].Start, AreaList[2].End, AreaList[3].Start, AreaList[3].End }));
         Assert.IsTrue((new decimal[] { 0.345m, 0.3456m, 0.3456m, 0.34567m, 0.34567m, 0.345678m, 0.345678m, 0.3456789m })
            .SequenceEqual(new decimal[] { AreaList[4].Start, AreaList[4].End, AreaList[5].Start, AreaList[5].End, AreaList[6].Start, AreaList[6].End, AreaList[7].Start, AreaList[7].End }));
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation with area removal")]
      public void AreaLine_GetExplicitAreaRepresentationCanExplodeAreasCorrectlyAfterRemove()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.3m);
         AreaLine.Add(0.266m, 0.269m);
         AreaLine.Remove(0.2565m, 0.2579m);
         AreaLine.Remove(0.276577m, 0.277579m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         string sExpectedAreas = "1,20-24,250-255,2560-2564,2579,258-259,26,270-275,2760-2764,27650-27656,276570-276576,277579,27758-27759,2776-2779,278-279,28-29";
         string sActualAreas = string.Join(",", AreaList.Select(x => x.ToString()).ToArray());

         Assert.AreEqual<string>(sExpectedAreas, sActualAreas);
      }

      [TestMethod, Description("Asserts that area line contains the correct explicit area representation with full range segment")]
      public void AreaLine_GetExplicitAreaRepresentationCanExplodeZeroOneSegment()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0m, 1m);

         List<IArea<string>> AreaList = AreaLine.GetExplicitAreas().ToList();
         Assert.AreEqual<string>("0-9", AreaList[0].ToString());
      }

      #endregion

      #region "Union"

      [TestMethod, Description("Asserts that area line union with same area line is performed correctly")]
      public void AreaLine_UnionOfTheSameAreaLine()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.1m, 0.2m);

         AreaLine<string> ResultingUnion = AreaLine.Union(AreaLineToUnion);
         Assert.AreEqual<string>("[0.1, 0.2)", ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line union with different area line is performed correctly")]
      public void AreaLine_UnionOfDifferentAreaLine1()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.2m, 0.6m);

         AreaLine<string> ResultingUnion = AreaLine.Union(AreaLineToUnion);
         Assert.AreEqual<string>("[0.1, 0.6)", ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line union with different area line is performed correctly")]
      public void AreaLine_UnionOfDifferentAreaLine2()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.3m, 0.6m);

         AreaLine<string> ResultingUnion = AreaLine.Union(AreaLineToUnion);
         Assert.AreEqual<string>("[0.1, 0.2); [0.3, 0.6)", ResultingUnion.ToString());
      }

      #endregion

      #region "Intersection"

      [TestMethod, Description("Asserts that area line intersection with same area line is performed correctly")]
      public void AreaLine_IntersectionOfTheSameAreaLine()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.1m, 0.2m);

         AreaLine<string> ResultingUnion = AreaLine.Intersect(AreaLineToUnion);
         Assert.AreEqual<string>("[0.1, 0.2)", ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line intersection with different area line is performed correctly")]
      public void AreaLine_IntersectionOfDifferentAreaLine1()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.2m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.2m, 0.6m);

         AreaLine<string> ResultingUnion = AreaLine.Intersect(AreaLineToUnion);
         Assert.AreEqual<string>(string.Empty, ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line intersection with different area line is performed correctly")]
      public void AreaLine_IntersectionOfDifferentAreaLine2()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.3m, 0.6m);

         AreaLine<string> ResultingUnion = AreaLine.Intersect(AreaLineToUnion);
         Assert.AreEqual<string>("[0.3, 0.4)", ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line intersection with different area line is performed correctly")]
      public void AreaLine_IntersectionOfDifferentAreaLine3()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.323m, 0.3245m);

         AreaLine<string> ResultingUnion = AreaLine.Intersect(AreaLineToUnion);
         Assert.AreEqual<string>("[0.323, 0.3245)", ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line intersection with different area line is performed correctly")]
      public void AreaLine_IntersectionOfDifferentAreaLine4()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.323m, 0.3245m);
         AreaLineToUnion.Add(0.399m, 0.3999m);

         AreaLine<string> ResultingUnion = AreaLine.Intersect(AreaLineToUnion);
         Assert.AreEqual<string>("[0.323, 0.3245); [0.399, 0.3999)", ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line intersection with different area line is performed correctly")]
      public void AreaLine_IntersectionOfDifferentAreaLine5()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);
         AreaLine.Add(0.7m, 0.8m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.323m, 0.3245m);
         AreaLineToUnion.Add(0.399m, 0.3999m);

         AreaLine<string> ResultingUnion = AreaLine.Intersect(AreaLineToUnion);
         Assert.AreEqual<string>("[0.323, 0.3245); [0.399, 0.3999)", ResultingUnion.ToString());
      }

      [TestMethod, Description("Asserts that area line intersection with different area line is performed correctly")]
      public void AreaLine_IntersectionOfDifferentAreaLine6()
      {
         AreaLineMock AreaLine = new AreaLineMock();
         AreaLine.Add(0.1m, 0.4m);
         AreaLine.Add(0.7m, 0.8m);

         AreaLineMock AreaLineToUnion = new AreaLineMock();
         AreaLineToUnion.Add(0.323m, 0.3245m);
         AreaLineToUnion.Add(0.399m, 0.3999m);
         AreaLineToUnion.Add(0.71m, 0.7234m);

         AreaLine<string> ResultingUnion = AreaLine.Intersect(AreaLineToUnion);
         Assert.AreEqual<string>("[0.323, 0.3245); [0.399, 0.3999); [0.71, 0.7234)", ResultingUnion.ToString());
      }

      #endregion
   }
}
