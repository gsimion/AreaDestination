using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AreaDestination;
using System.Collections.Generic;
using System.Linq;

namespace AreaDestinationTest
{
   [TestClass]
   public class AreaRectangle
   {

      /// <summary>
      /// Asserts that default constructor of an area rectangle returns the right properties.
      /// </summary>
      [TestMethod, Description("Asserts that default constructor of an area rectangle returns the right properties")]
      public void AreaRectangle_Constructor()
      {
         Area<string> a = new Area<string>("test", 1234);
         AreaRectangle<string> ar = Painter.GetRepresentation<string>(a);

         Assert.AreEqual<int>(1, ar.Scale, "scale");
         Assert.AreEqual<int>(1, ar.Height, "height");
         Assert.AreEqual<int>(1, ar.Width, "width");
         Assert.AreEqual<string>("test", ar.Title, "title");
         Assert.AreEqual<string>(string.Empty, ar.Description, "description");

      }

      /// <summary>
      /// Asserts that default constructor with description of an area rectangle returns the right properties.
      /// </summary>
      [TestMethod, Description("Asserts that default constructor with description of an area rectangle returns the right properties")]
      public void AreaRectangle_ConstructorWithDescription()
      {
         Area<string> a = new Area<string>("test", 1234);
         AreaRectangle<string> ar = Painter.GetRepresentation<string>(a, "description");

         Assert.AreEqual<int>(1, ar.Scale, "scale");
         Assert.AreEqual<int>(1, ar.Height, "height");
         Assert.AreEqual<int>(1, ar.Width, "width");
         Assert.AreEqual<string>("test", ar.Title, "title");
         Assert.AreEqual<string>("description", ar.Description, "description");

      }

      /// <summary>
      /// Asserts that description can be changed properly.
      /// </summary>
      [TestMethod, Description("Asserts that description can be changed properly")]
      public void AreaRectangle_ChangeDescription()
      {
         Area<string> a = new Area<string>("test", 1234);
         AreaRectangle<string> ar = Painter.GetRepresentation<string>(a, "description");

         ar.Description = "not the first description";
         Assert.AreEqual<string>("not the first description", ar.Description, "description");

         ar.Description = null ;
         Assert.IsNull(ar.Description);
      }

      /// <summary>
      /// Asserts that area rectangle reflects the scale in its properties.
      /// </summary>
      [TestMethod, Description("Asserts that area rectangle reflects the scale in its properties")]
      public void AreaRectangle_SetScale()
      {
         Area<string> a = new Area<string>("test", 1234);
         AreaRectangle<string> ar = Painter.GetRepresentation<string>(a);
         int expectedScale = 10000;
         int expectedHeight = Convert.ToInt32(decimal.Truncate(((20m - 4) / 20m) * expectedScale));
         int expectedWidth = Convert.ToInt32(decimal.Truncate((0.1235m - 0.1234m) * expectedScale));

         ar.SetScale(expectedScale);
         Assert.AreEqual<int>(expectedScale, ar.Scale, "scale");
         Assert.AreEqual<int>(expectedHeight, ar.Height, "scaled height");
         Assert.AreEqual<int>(expectedWidth, ar.Width, "scaled width");
      }

      /// <summary>
      /// Asserts that area rectangle throws exception when scale is less than one.
      /// </summary>
      [TestMethod, ExpectedException(typeof(ArgumentException)), Description("Asserts that area rectangle throws exception when scale is less than one.")]
      public void AreaRectangle_SetScale_ArgumentException()
      {
         Area<string> a = new Area<string>("test", 1234);
         AreaRectangle<string> ar = Painter.GetRepresentation<string>(a);

         ar.SetScale(0);
      }

      /// <summary>
      /// Asserts that area rectangles list of a destination can be built.
      /// </summary>
      [TestMethod, Description("Asserts that area rectangles list of a destination can be built.")]
      public void AreaRectangle_SingleDestination_GetRepresentation_DefaultProperties()
      {
         string destName = "destination_test";
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination(destName, "1, 12, 123");
         List<AreaRectangle<string>> ars = Painter.GetRepresentation<string>(ds[destName]);

         Assert.AreEqual<int>(3, ars.Count, "expected rectangles created");
         Assert.AreEqual<int>(1, ars.Select(x=> x.Id).Distinct().Count(), "expected distinct descriptions");
         Assert.AreEqual<string>(destName, ars.First().Id, "id");
      }

      /// <summary>
      /// Asserts that area rectangles list of a destination returns correct rectangles.
      /// </summary>
      [TestMethod, Description("Asserts that area rectangles list of a destination returns correct rectangles.")]
      public void AreaRectangle_SingleDestination_GetRepresentation_CorrectRectangles()
      {
         int expectedScale = 10000; 
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("d", "1, 12, 123");
         List<AreaRectangle<string>> ars = Painter.GetRepresentation<string>(ds["d"], expectedScale);

         Assert.AreEqual<int>(3, ars.Count, "expected rectangles created");
         AreaRectangle<string> r1 = ars[0];
         AreaRectangle<string> r2 = ars[1];
         AreaRectangle<string> r3 = ars[2];

         Assert.AreEqual<int>(expectedScale, r1.Scale);
         Assert.AreEqual<int>(Convert.ToInt32(decimal.Truncate(((20m - 1) / 20m) * expectedScale)), r1.Height);
         Assert.AreEqual<int>(Convert.ToInt32(decimal.Truncate((0.2m - 0.1m) * expectedScale)), r1.Width);
         Assert.AreEqual<int>(expectedScale, r2.Scale);
         Assert.AreEqual<int>(Convert.ToInt32(decimal.Truncate(((20m - 2) / 20m) * expectedScale)), r2.Height);
         Assert.AreEqual<int>(Convert.ToInt32(decimal.Truncate((0.13m - 0.12m) * expectedScale)), r2.Width);
         Assert.AreEqual<int>(expectedScale, r3.Scale);
         Assert.AreEqual<int>(Convert.ToInt32(decimal.Truncate(((20m - 3) / 20m) * expectedScale)), r3.Height);
         Assert.AreEqual<int>(Convert.ToInt32(decimal.Truncate((0.124m - 0.123m) * expectedScale)), r3.Width);
      }

      /// <summary>
      /// Asserts that area rectangles list of a destination returns rectangles for a destination set.
      /// </summary>
      [TestMethod, Description("Asserts that area rectangles list of a destination returns rectangles for a destination set.")]
      public void AreaRectangle_DestinationSet_GetRepresentation_Default()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("d1", "2, 22, 223");
         ds.UpdateDestination("d2", "1, 1234, 3");
         List<AreaRectangle<string>> ars = Painter.GetRepresentation<Destination<string>, string>(ds);

         Assert.AreEqual<int>(6, ars.Count, "expected rectangles created");
         Assert.AreEqual<int>(2, ars.Select(x => x.Id).Distinct().Count(), "expected distinct descriptions");
         Assert.AreEqual<string>("d2", ars.First().Id, "id"); //order
      }
   }
}
