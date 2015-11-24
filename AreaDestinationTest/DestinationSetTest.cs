using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AreaDestination;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AreaDestinationTest
{
   [TestClass]
   public class DestinationSetTest
   {

      private static DestinationSet<string> m_BigDestSetA = new DestinationSet<string>();
      private static DestinationSet<string> m_BigDestSetB = new DestinationSet<string>();
      private static DestinationSet<string> m_DestSetRouting = new DestinationSet<string>();

      private static DestinationSet<string> m_DestSetCarrier = new DestinationSet<string>();
      /// <summary>
      /// Populates big destination sets as part of reference data.
      /// </summary>
      private static void PopulateBigDestinationSets()
      {
         foreach (string sLine in Properties.Resources.destA.Split(Environment.NewLine.ToCharArray()))
         {
            string[] Area = sLine.Split(new char[] {
      ' ',
      '\t'
   }, StringSplitOptions.RemoveEmptyEntries);
            if (Area.Length == 2)
            {
               m_BigDestSetA.AddArea(Area[0], Convert.ToUInt64(Area[1]));
            }
         }

         foreach (string sLine in Properties.Resources.destB.Split(Environment.NewLine.ToCharArray()))
         {
            string[] Area = sLine.Split(new char[] {
      ' ',
      '\t'
   }, StringSplitOptions.RemoveEmptyEntries);
            if (Area.Length == 2)
            {
               m_BigDestSetB.AddArea(Area[0], Convert.ToUInt64(Area[1]));
            }
         }
      }

      [ClassInitialize()]
      public static void ClassInit(TestContext context)
      {
         PopulateBigDestinationSets();
         PopulateCarrierDest();
         PopulateRoutingDest();
      }

      /// <summary>
      /// Populates routing destination set.
      /// </summary>
      private static void PopulateRoutingDest()
      {
         m_DestSetRouting.UpdateDestination("DENMARK FIXED", "45");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-COPENHAGEN", "453");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-MØDETEL", "4590901,4590909090");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-NVPN", "4516140");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-PREMIUM", "45909018");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-PUBLIC PAY", "45909");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-SPECIAL", "459013");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-TDC", "45701");
         m_DestSetRouting.UpdateDestination("DENMARK FIXED-TRYG", "454420,459879");
         m_DestSetRouting.UpdateDestination("DENMARK FREEPHONE", "458017-458019");
         m_DestSetRouting.UpdateDestination("DENMARK MOBILE HI3G", "452597,4531,45426,454271-454274,454283,454291-454292,454294-454295,455211-455212,45535-45538,45605,45939");
         m_DestSetRouting.UpdateDestination("DENMARK MOBILE HI3G", "452597,4531,45426,454271-454274,454283,454291-454292,454294-454295,455211-455212,45535-45538,45605,45939");
         m_DestSetRouting.UpdateDestination("DENMARK MOBILE OTHERS", "45259,4531312,45318,4542,454290,45506,4552,455259,4553,455398,4560,456098-456099,4571,4581,4591-4593");
         m_DestSetRouting.UpdateDestination("DENMARK MOBILE TDC", "4520-4521,4523-4524,452595-452596,452598,4529-4530,4540,454275,455060-455061,4551,455110-455117,4551180-4551187,455220,455230,455240,455250,455254,455256-455258,4560990,4560992,4561,456140-456145,456147,45713-45716,457170,457172,457177,45718,458140-458145,458161,458171-458175,458177,458188,45911,459122-459125,45914,459152-459156,45916-45917,459189,45921,459221-459222,459240-459244,459290,459299,459310,459320,459330");
         m_DestSetRouting.UpdateDestination("DENMARK MOBILE TELENOR", "45206-45209,4522,4525,45319,45405-45409,4541,45424-45425,454260,454270,454280-454281,454284-454285,454287-454289,454293,4550,455064,455068,455210,455222,455225,455233,455242,455244,45525-45529,45531,455333,455390-455397,455399,456050,45606-45608,456090-456097,45618-45619,45711-45712,457171,457173-457176,457178-457179,457190-457195,457199,45811-45812,458130-458136,45815,458160,458162-458170,458176,458178-458187,458189,45819,459110-459112,459114,459119,459121,459131-459139,459150-459151,459159,459180-459188,45919,459218-459220,459224-459229,45923,459246-459248");
         m_DestSetRouting.UpdateDestination("DENMARK MOBILE TELIA", "452395,4526-4528,45371000,45420-45423,454276-454279,454282,454286,454296-454299,455188-455189,45521-45524,455319,45532-45534,45601-45604,456146");
         m_DestSetRouting.UpdateDestination("DENMARK UNASSIGNED", "450,459095");
      }

      /// <summary>
      /// Populates carrier destination set
      /// </summary>
      private static void PopulateCarrierDest()
      {
         m_DestSetCarrier.UpdateDestination("Denmark - Fixed", "45");
         m_DestSetCarrier.UpdateDestination("Denmark - Mobile Others", "452,4525981,4531312,45318,453235,4540,4542,4550,4552,455398,4560,456911,4571,4581,4591-4593");
         m_DestSetCarrier.UpdateDestination("Denmark - Paid800", "45802-45809");
         m_DestSetCarrier.UpdateDestination("Denmark - Mobile HI3G", "452597,45311-45312,453130,4531310-4531311,4531313-4531319,453132-453139,45314-45317,454261-454269,454271-454274,454283,454291-454292,454294-454295,455211-455212,45535-45538,456051-456059,45939");
         m_DestSetCarrier.UpdateDestination("Denmark - Mobile Sonofon", "45206-45209,45221-45229,45251-45258,452594,4525980,452599,45319,45405-45409,4541,45424-45425,454260,454270,454280-454281,454284-454285,454287-454289,454293,45501-45505,455062,455065-455068,45507-45509,455210,455222,455225,455233,455242,455244,455251-455253,455255,45526-45529,455310-455318,455333,455390-455397,455399,456050,45606-45608,456090-456097,45618-45619,45711-45712,457171,457173-457176,457178-457179,457190-457195,457199,45811-45812,458130-458136,45815,458160,458162-458170,458176,458178-458187,458189,45819,459110-459112,459114,459119,459121,459131-459139,459150-459151,459159,459180-459188,459191-459199,459218-459220,459224-459229,45923,459246-459248,459292-459295,45935");
         m_DestSetCarrier.UpdateDestination("Denmark - Mobile TDK", "45201-45205,4521,45231-45238,452390-452394,452396-452399,4524,452595-452596,4525982,4525988-4525989,4529-4530,45401-45404,454275,455060-455061,45510-45517,455180-455187,45519,455220,455230,455240,455250,455254,455256-455258,45611-45613,456140-456145,456147-456149,45615-45617,457170,457172,457177,458140-458145,458161,458171-458175,458177,458188,459113,459115-459118,459122-459125,45914,459152-459156,459189,459210-459216,459221-459222,459240-459244,459272,459290,459299,459310,459313,459320,459330,459340");
         m_DestSetCarrier.UpdateDestination("Denmark - Mobile Telia", "452395,4526-4528,45371000,45420-45423,454276-454279,454282,454286,454296-454299,455188-455189,455213-455219,455221,455223-455224,455226-455229,455231-455232,455234-455239,455241,455243,455245-455249,455319,45532,455330-455332,455334-455339,45534,45601-45604,456146");
      }


      #region "Populate"

      /// <summary>
      /// Asserts that destination set can be populated correctly via dictionary.
      /// </summary>
      [TestMethod, Description("Asserts that destination set can be populated correctly via dictionary")]
      public void Destination_PopulateByDictionary()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         Dictionary<string, string> Dict = new Dictionary<string, string>();
         Dict.Add("destination1", "123-125, 2");
         Dict.Add("destination2", "34567");

         ds.Populate(Dict);
         Assert.AreEqual<int>(3, ds.Destinations.Count);
         Assert.AreEqual<string>("123-125,2", ds.Destinations["destination1"].ToString());
         Assert.AreEqual<string>("34567", ds.Destinations["destination2"].ToString());
      }

      /// <summary>
      /// Asserts that destination set can be populated correctly via datatable.
      /// </summary>
      [TestMethod, Description("Asserts that destination set can be populated correctly via datatable")]
      public void Destination_PopulateByDatatable()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         DataTable dt = new DataTable();
         dt.Columns.Add("MOCK", typeof(System.Object));
         DataColumn DestinationColumn = dt.Columns.Add("D", typeof(System.String));
         DataColumn AreaColumn = dt.Columns.Add("A", typeof(System.String));
         dt.Rows.Add(new object[] {
      DBNull.Value,
      "destination1",
      "123-125"
   });
         dt.Rows.Add(new object[] {
      12,
      "destination2",
      "34567"
   });
         dt.Rows.Add(new object[] {
      12,
      "destination2",
      DBNull.Value
   });
         dt.Rows.Add(new object[] {
      12,
      "destination2",
      string.Empty
   });
         dt.Rows.Add(new object[] {
      12,
      DBNull.Value,
      DBNull.Value
   });
         dt.Rows.Add(new object[] {
      12,
      string.Empty,
      "1"
   });
         dt.Rows.Add(new object[] {
      12,
      "destination1",
      "2"
   });

         ds.Populate(dt.Select(), DestinationColumn, AreaColumn, false);
         Assert.AreEqual<int>(3, ds.Destinations.Count);
         Assert.AreEqual("123-125,2", ds.Destinations["destination1"].ToString());
         Assert.AreEqual("34567", ds.Destinations["destination2"].ToString());
      }

      #endregion

      [TestMethod, Description("Asserts that single areas can be added to a destination belonging to a destination set")]
      public void Destination_SingleArea()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "33");
         Assert.AreEqual<string>("33", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that multiple areas can be added to a destination belonging to a destination set")]
      public void Destination_SingleAreaWithMultipleCode()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "33");
         ds.AddArea("a", 34);
         Assert.AreEqual<string>("33-34", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that areas can be modified for a destination belonging to a destination set")]
      public void Destination_ModArea()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "33");
         Assert.AreEqual<string>("33", ds.Destinations["a"].ToString());
         ds.UpdateDestination("a", "330, 332");
         Assert.AreEqual<string>("330,332", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that overlapping areas are kept for a destination belonging to a destination set")]
      public void Destination_OverlapAreaOpt()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "33,332");
         Assert.AreEqual<string>("33,332", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that single area can be removed from a destination belonging to a destination set")]
      public void Destination_RemoveArea()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "330-332,338");
         Assert.AreEqual<string>("330-332,338", ds.Destinations["a"].ToString());
         ds.RemoveArea(331);
         Assert.AreEqual<string>("330,332,338", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that adding an already existing area to a destination throws an argument exception")]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Destination_AddAlreadyExistingArea()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "330-332,338");
         Assert.AreEqual<string>("330-332,338", ds.Destinations["a"].ToString());
         ds.AddArea("a", 331);
         Assert.AreEqual<string>("330-332,338", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that updating a different destination with an already existing area to a destination throws an argument exception")]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Destination_CreateDestinationWithExistingAreaInOtherDest()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "330-332,338");
         ds.UpdateDestination("b", "330");
      }

      [TestMethod, Description("Asserts that it is possible to update a destination moving around the same areas")]
      public void Destination_UpdateDestination()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "330-332,338");
         ds.UpdateDestination("a", "1,330");
         ds.AddArea("a", 332);
         Assert.AreEqual<string>("1,330,332", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that it is possible to update a destination moving around a covered area code which is not the same")]
      public void Destination_UpdateDestinationContainingCoveredAreaCode()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "33,3341");
         ds.UpdateDestination("b", "334");
         Assert.AreEqual<string>("330-333,3341,335-339", ds.ExplicitDestinations["a"].ToString());
         Assert.AreEqual<string>("3340,3342-3349", ds.ExplicitDestinations["b"].ToString());
      }

      [TestMethod, Description("Asserts that it is possible to update a destination moving around a covered area code which is not the same")]
      public void Destination_UpdateDestinationContainingCoveredAreaCodeRemoveCode()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "33,3341");
         ds.UpdateDestination("b", "334");
         ds.UpdateDestination("a", "33");
         Assert.AreEqual<string>("330-333,335-339", ds.ExplicitDestinations["a"].ToString());
         Assert.AreEqual<string>("334", ds.ExplicitDestinations["b"].ToString());
      }

      [TestMethod, Description("Asserts that it is possible add area after updating a destination that is already defined for another destinaiton")]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Destination_AddAreaExistingAreaInOtherDest()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "330-332,338");
         ds.UpdateDestination("b", "337");
         ds.AddArea("b", 331);
      }

      [TestMethod, Description("Asserts that it is not possible to remove a not existing explicit area, even if there are area overlapping with that")]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Destination_RemoveNotExistingArea()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.UpdateDestination("a", "330-332,338");
         Assert.AreEqual("330-332,338", ds.Destinations["a"].ToString());
         ds.RemoveArea(3312);
      }

      [TestMethod, Description("Asserts that an empty system destination set contains the right undefined destination properties")]
      public void DestinationSet_Empty()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         Assert.AreEqual<int>(1, ds.ExplicitDestinations.Count);
         Assert.IsTrue(ds.ExplicitDestinations.ContainsKey(ds.UndefinedDestinationId));
         Assert.IsTrue(ds.Destinations.ContainsKey(ds.UndefinedDestinationId));
         Assert.AreEqual<string>("0-9", ds.ExplicitDestinations[ds.UndefinedDestinationId].ToString());
         Assert.AreEqual<string>("0-9", ds.Destinations[ds.UndefinedDestinationId].ToString());
         Assert.AreEqual<int>(1, ds.Destinations.Count);
         Assert.IsTrue(ds.Destinations.ContainsKey(ds.UndefinedDestinationId));
         Assert.AreEqual<string>("0-9", ds.Destinations[ds.UndefinedDestinationId].ToString());
         Assert.AreEqual<string>("0-9", ds.Destinations[ds.UndefinedDestinationId].ToString());
      }

      [TestMethod, Description("Asserts that already covered range is not overridden if area is explicitely added")]
      public void DestinationSet_Dest_CoveredRange()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.AddArea("a", 33);
         ds.AddArea("a", 331);
         ds.AddArea("b", 1);
         Assert.AreEqual<int>(3, ds.Destinations.Count);
         Assert.IsTrue(ds.Destinations.ContainsKey("a"));
         Assert.AreEqual<string>("33,331", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that continuous range is merged correctly if area is explicitely added")]
      public void DestinationSet_Dest_ContiguousRange()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.AddArea("a", 330);
         ds.AddArea("a", 331);
         ds.AddArea("b", 332);
         Assert.AreEqual<int>(3, ds.Destinations.Count);
         Assert.AreEqual<string>("330-331", ds.Destinations["a"].ToString());
      }

      [TestMethod, Description("Asserts that same areas cannot be added to another destination if already explicitely defined")]
      [ExpectedException(typeof(InvalidOperationException))]
      public void DestinationSet_Dest_SplitRange()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.AddArea("a", 330);
         ds.AddArea("a", 332);
         ds.AddArea("b", 332);
      }

      [TestMethod, Description("Asserts that more than one disjoint destination can be stored correctly in the destination set")]
      public void DestinationSet_Dest_2_Dest_Disjoint()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.AddArea("a", 330);
         ds.AddArea("b", 332);
         Assert.AreEqual<int>(3, ds.Destinations.Count);
         Assert.IsTrue(ds.Destinations.ContainsKey("a"));
         Assert.IsTrue(ds.Destinations.ContainsKey("b"));
         Assert.AreEqual<string>("330", ds.Destinations["a"].ToString());
         Assert.AreEqual<string>("332", ds.Destinations["b"].ToString());
      }

      [TestMethod, Description("Asserts that more than one overlapping destination can be stored correctly in the destination set")]
      public void DestinationSet_Dest_2_Dest_Overlap()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.AddArea("a", 33);
         ds.AddArea("b", 332);
         Assert.AreEqual<int>(3, ds.Destinations.Count);
         Assert.AreEqual<string>("33", ds.Destinations["a"].ToString());
         Assert.AreEqual<string>("332", ds.Destinations["b"].ToString());
      }

      [TestMethod, Description("Asserts that more destination can be modified correctly when moving an area code belonging to a range")]
      public void DestinationSet_Dest_2_Mod_Dest()
      {
         DestinationSet<string> ds = new DestinationSet<string>();
         ds.AddArea("a", 33);
         ds.AddArea("b", 332);
         Assert.AreEqual(3, ds.Destinations.Count);
         Assert.AreEqual("33", ds.Destinations["a"].ToString());
         Assert.AreEqual("332", ds.Destinations["b"].ToString());
         ds.AddArea("b", 333);
         Assert.AreEqual(3, ds.Destinations.Count);
         Assert.AreEqual("33", ds.Destinations["a"].ToString());
         Assert.AreEqual("332-333", ds.Destinations["b"].ToString());
      }

      [TestMethod, Description("Asserts that exception is thrown if update destination is called without single area codes")]
      [ExpectedException(typeof(FormatException))]
      public void DestinationSet_Dest_CorrectHandlingAreaCodes()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         ds1.UpdateDestination("A", "33;334");
         ds1.UpdateDestination("B", "33");
      }

      #region "Mapping"

      [TestMethod, Description("Asserts that mapping is preserving the right destination identity")]
      public void DestinationSet_Dest_Map_Identity()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds1.AddArea("a", 33);
         ds1.AddArea("b", 332);
         ds2.AddArea("a", 33);
         ds2.AddArea("b", 332);
         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);

         Assert.AreEqual(3, maps.Count);
         Assert.AreEqual("00-32,34-99", maps.GetMappedAreas(ds1.UndefinedDestinationId, ds2.UndefinedDestinationId));
         Assert.AreEqual("330-331,333-339", maps.GetMappedAreas("a", "a"));
         Assert.AreEqual("332", maps.GetMappedAreas("b", "b"));
      }

      [TestMethod, Description("Asserts that mapping is capable of mapping correctly overlapping destinations")]
      public void DestinationSet_Dest_Map_Overlap()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds1.AddArea("a", 33);
         ds1.AddArea("b", 332);
         ds2.AddArea("a", 33);
         ds2.AddArea("b", 334);
         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);

         Assert.AreEqual(4, maps.Count);
         Assert.AreEqual("00-32,34-99", maps.GetMappedAreas(ds1.UndefinedDestinationId, ds2.UndefinedDestinationId));
         Assert.AreEqual("330-331,333,335-339", maps.GetMappedAreas("a", "a"));
         Assert.AreEqual("334", maps.GetMappedAreas("a", "b"));
         Assert.AreEqual("332", maps.GetMappedAreas("b", "a"));

         Assert.AreEqual(2, maps.LostAreaCodes.Count);
         Assert.AreEqual("332", maps.LostAreaCodes["b"].ToString());
         Assert.AreEqual("334", maps.LostAreaCodes["a"].ToString());

         Assert.AreEqual(2, maps.GainedAreaCodes.Count);
         Assert.AreEqual("332", maps.GainedAreaCodes["a"].ToString());
         Assert.AreEqual("334", maps.GainedAreaCodes["b"].ToString());
      }

      [TestMethod, Description("Asserts that mapping is capable of mapping correctly overlapping destinations")]
      public void DestinationSet_Diff_Dest_Map_Overlap()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds1.AddArea("ds1a", 33);
         ds1.AddArea("ds1b", 332);
         ds2.AddArea("ds2a", 33);
         ds2.AddArea("ds2b", 334);
         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);

         Assert.AreEqual(4, maps.Count);
         Assert.AreEqual("330-331,333,335-339", maps.GetMappedAreas("ds1a", "ds2a"));
         Assert.AreEqual("334", maps.GetMappedAreas("ds1a", "ds2b"));
         Assert.AreEqual("332", maps.GetMappedAreas("ds1b", "ds2a"));

         Assert.AreEqual(2, maps.LostAreaCodes.Count);
         Assert.AreEqual("332", maps.LostAreaCodes["ds1b"].ToString());
         Assert.AreEqual("330-331,333-339", maps.LostAreaCodes["ds1a"].ToString());

         Assert.AreEqual(2, maps.GainedAreaCodes.Count);
         Assert.AreEqual("330-333,335-339", maps.GainedAreaCodes["ds2a"].ToString());
         Assert.AreEqual("334", maps.GainedAreaCodes["ds2b"].ToString());
      }

      [TestMethod, Description("Asserts that mapping is capable of mapping correctly a single destination swap")]
      public void DestinationSet_Mapping_ChildDestinationSwap()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         ds1.AddArea("AFGHANISTAN-FIX", 93);
         ds1.AddArea("AFGHANISTAN-MOBILE OTHERS", 937);
         ds1.AddArea("AFGHANISTAN-MOBILE AREEBA", 9377);
         //do not include AFGHANISTAN-MOBILE ETISALAT
         ds1.AddArea("AFGHANISTAN-MOBILE ROSHAN", 9379);

         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds2.AddArea("AFGHANISTAN-FIX", 93);
         ds2.AddArea("AFGHANISTAN-MOBILE OTHERS", 937);
         ds2.AddArea("AFGHANISTAN-MOBILE AREEBA", 9377);
         ds2.AddArea("AFGHANISTAN-MOBILE ETISALAT", 9378);
         //do not include AFGHANISTAN-MOBILE ROSHAN

         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);

         Assert.AreEqual<int>(6, maps.Count);
         Assert.AreEqual<int>(2, maps.LostAreaCodes.Count);
         Assert.AreEqual<int>(2, maps.GainedAreaCodes.Count);
         Assert.AreEqual<string>("9379", maps.LostAreaCodes["AFGHANISTAN-MOBILE ROSHAN"].ToString());
         Assert.AreEqual<string>("9378", maps.LostAreaCodes["AFGHANISTAN-MOBILE OTHERS"].ToString());
         Assert.AreEqual<string>("9378", maps.GainedAreaCodes["AFGHANISTAN-MOBILE ETISALAT"].ToString());
         Assert.AreEqual<string>("9379", maps.GainedAreaCodes["AFGHANISTAN-MOBILE OTHERS"].ToString());
      }

      [TestMethod, Description("Asserts that mapping is fully mapping correctly the same destination set, specified with different order per destination")]
      public void DestinationSet_Mapping_SameDestSetWithoutChanges()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         ds1.AddArea("AFGHANISTAN-FIX", 93);
         ds1.AddArea("AFGHANISTAN-MOBILE OTHERS", 937);
         ds1.AddArea("AFGHANISTAN-MOBILE AREEBA", 9377);
         ds1.AddArea("AFGHANISTAN-MOBILE ETISALAT", 9378);
         ds1.AddArea("AFGHANISTAN-MOBILE ROSHAN", 9379);
         ds1.AddArea("ALBANIA", 355);
         ds1.UpdateDestination("ALBANIA-MOBILE", "35566 - 35569");
         ds1.AddArea("ALBANIA-TIRANA", 3554);
         ds1.AddArea("ALGERIA", 213);
         ds1.UpdateDestination("ALGERIA-MOBILE ORASCOM", "21377; 213780; 21379".Replace(';', ','));
         ds1.UpdateDestination("ALGERIA-MOBILE OTHERS", "213659; 21366; 213670; 21369; 21396190-21396192".Replace(';', ','));
         ds1.AddArea("ALGERIA-MOBILE WATANIYA", 2135);
         ds1.AddArea("AMERICAN SAMOA", 1684);
         ds1.AddArea("ANDORRA", 376);
         ds1.UpdateDestination("ANDORRA-MOBILE", "3763 - 3766");

         //add same set with different order of the elements, map should be 1:1
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds2.AddArea("AFGHANISTAN-FIX", 93);
         ds2.AddArea("AFGHANISTAN-MOBILE OTHERS", 937);
         ds2.AddArea("AFGHANISTAN-MOBILE ROSHAN", 9379);
         ds2.AddArea("AFGHANISTAN-MOBILE AREEBA", 9377);
         ds2.AddArea("AFGHANISTAN-MOBILE ETISALAT", 9378);
         ds2.AddArea("ANDORRA", 376);
         ds2.UpdateDestination("ANDORRA-MOBILE", "3763 - 3766");
         ds2.AddArea("ALBANIA", 355);
         ds2.AddArea("ALBANIA-TIRANA", 3554);
         ds2.UpdateDestination("ALBANIA-MOBILE", "35566 - 35569");
         ds2.AddArea("ALGERIA", 213);
         ds2.UpdateDestination("ALGERIA-MOBILE OTHERS", "213659; 21366; 213670; 21369; 21396190-21396192".Replace(';', ','));
         ds2.UpdateDestination("ALGERIA-MOBILE ORASCOM", "21377; 213780; 21379".Replace(';', ','));
         //
         ds2.AddArea("ALGERIA-MOBILE WATANIYA", 2135);
         ds2.AddArea("AMERICAN SAMOA", 1684);

         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);
         Assert.AreEqual<int>(16, maps.Maps.Count, "Fully mapped destinations");
         Assert.AreEqual<int>(0, maps.LostAreaCodes.Count, "Lost areas");
         Assert.AreEqual<int>(0, maps.GainedAreaCodes.Count, "Gained areas");
         //skip the default destination
         foreach (string sDestinationName in ds2.Destinations.Select(x => x.Key).Except(new string[] { string.Empty }))
         {
            Assert.IsTrue(maps[sDestinationName].HasMapping);
            Assert.IsTrue(maps[sDestinationName].IsFullyMapped);
            Assert.IsFalse(maps[sDestinationName].IsMultiMapped);
            Assert.IsFalse(maps[sDestinationName].IsPartiallyMapped);
         }
      }

      [TestMethod, Description("Asserts that mapping is mapping correctly a single scenario with one destination and its sub destinations")]
      public void DestinationSet_Mapping_SameRoutingDestination_Chile()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         ds1.UpdateDestination("CHILE", "56");
         ds1.UpdateDestination("CHILE-EASTER ISLAND", "5632100,563221,563225,563255");
         ds1.UpdateDestination("CHILE-MOBILE", "569,568");
         ds1.UpdateDestination("CHILE-SANTIAGO", "562");
         ds1.UpdateDestination("CHILE-SPECIAL SERVICES", "5615,5611,56322100,5632255,5645197");

         //add same routing destination, expected 1:1
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds2.UpdateDestination("CHILE", "56");
         ds2.UpdateDestination("CHILE-EASTER ISLAND", "5632100,563221,563225,563255");
         ds2.UpdateDestination("CHILE-MOBILE", "569,568");
         ds2.UpdateDestination("CHILE-SANTIAGO", "562");
         ds2.UpdateDestination("CHILE-SPECIAL SERVICES", "5615,5611,56322100,5632255,5645197");

         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);
         Assert.AreEqual<int>(6, maps.Maps.Count, "Fully mapped destinations");
         Assert.AreEqual<int>(0, maps.LostAreaCodes.Count, "Lost areas");
         Assert.AreEqual<int>(0, maps.GainedAreaCodes.Count, "Gained areas");
         Assert.IsTrue(maps["CHILE"].IsFullyMapped);
         Assert.IsTrue(maps["CHILE-EASTER ISLAND"].IsFullyMapped);
         Assert.IsTrue(maps["CHILE-MOBILE"].IsFullyMapped);
         Assert.IsTrue(maps["CHILE-SANTIAGO"].IsFullyMapped);
         Assert.IsTrue(maps["CHILE-SPECIAL SERVICES"].IsFullyMapped);
      }

      [TestMethod, Description("Asserts that mapping is mapping correctly a single scenario containing least significant zeros")]
      public void DestinationSet_Mapping_SameRoutingDestination_LeastSignificantZerosOrder()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         ds1.UpdateDestination("GERMANY-SERVICES", "4915");
         ds1.UpdateDestination("GERMANY-E-PLUS", "49150");

         //add same routing destination, expected 1:1
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds2.UpdateDestination("GERMANY-E-PLUS", "49150");
         ds2.UpdateDestination("GERMANY-SERVICES", "4915");

         DestinationSetBase.MappingResult<string> maps1to2 = ds1.MapToDestinationSet(ds2);
         Assert.AreEqual<int>(3, maps1to2.Maps.Count, "Fully mapped destinations");
         Assert.AreEqual<int>(0, maps1to2.LostAreaCodes.Count, "Lost areas");
         Assert.AreEqual<int>(0, maps1to2.GainedAreaCodes.Count, "Gained areas");
         Assert.IsTrue(maps1to2["GERMANY-E-PLUS"].IsFullyMapped);
         Assert.IsTrue(maps1to2["GERMANY-SERVICES"].IsFullyMapped);

         DestinationSetBase.MappingResult<string> maps2to1 = ds2.MapToDestinationSet(ds1);
         Assert.AreEqual<int>(3, maps2to1.Maps.Count, "Fully mapped destinations");
         Assert.AreEqual<int>(0, maps2to1.LostAreaCodes.Count, "Lost areas");
         Assert.AreEqual<int>(0, maps2to1.GainedAreaCodes.Count, "Gained areas");
         Assert.IsTrue(maps2to1["GERMANY-E-PLUS"].IsFullyMapped);
         Assert.IsTrue(maps2to1["GERMANY-SERVICES"].IsFullyMapped);
      }

      [TestMethod, Description("Asserts that the correct parent for the mapping is fetched by the mapper")]
      public void DestinationSet_Mapping_RSI_SwedenMobileExample()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         ds1.AddArea("SWEDEN", 46);
         ds1.AddArea("SWEDEN-MOBILE", 467);

         //define just main destinations
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds2.AddArea("SWEDEN", 46);
         ds2.UpdateDestination("SWEDEN-MOBILE 1", "4670-4675");
         ds2.UpdateDestination("SWEDEN-MOBILE 2", "4676-4678");

         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);
         Assert.AreEqual<int>(3, maps.GetMappedDestination("SWEDEN-MOBILE").Count());
         Assert.AreEqual<int>(1, maps.GetMappedDestination("SWEDEN").Count());
         Assert.IsTrue(maps["SWEDEN"].IsFullyMapped);
         Assert.IsTrue(maps["SWEDEN-MOBILE"].IsMultiMapped);
         Assert.AreEqual<string>("4670-4675", maps.Maps[new KeyValuePair<string, string>("SWEDEN-MOBILE", "SWEDEN-MOBILE 1")].ToString());
         Assert.AreEqual<string>("4676-4678", maps.Maps[new KeyValuePair<string, string>("SWEDEN-MOBILE", "SWEDEN-MOBILE 2")].ToString());
         Assert.AreEqual<string>("4679", maps.Maps[new KeyValuePair<string, string>("SWEDEN-MOBILE", "SWEDEN")].ToString());
      }

      [TestMethod, Description("Asserts that mapping is capable of mapping correctly a single destination swap, in the typical RSI scenario for CI")]
      public void DestinationSet_Mapping_RSI_ChildDestinationMovedToMain()
      {
         DestinationSet<string> ds1 = new DestinationSet<string>();
         ds1.AddArea("AFGHANISTAN-FIX", 93);
         ds1.AddArea("AFGHANISTAN-MOBILE OTHERS", 937);
         ds1.AddArea("AFGHANISTAN-MOBILE AREEBA", 9377);
         ds1.AddArea("AFGHANISTAN-MOBILE ETISALAT", 9378);
         ds1.AddArea("AFGHANISTAN-MOBILE ROSHAN", 9379);
         ds1.AddArea("ALBANIA", 355);
         ds1.UpdateDestination("ALBANIA-MOBILE", "35566 - 35569");
         ds1.AddArea("ALBANIA-TIRANA", 3554);

         //define just main destinations
         DestinationSet<string> ds2 = new DestinationSet<string>();
         ds2.AddArea("AFGHANISTAN-FIX", 93);
         ds2.AddArea("ALBANIA", 355);

         DestinationSetBase.MappingResult<string> maps = ds1.MapToDestinationSet(ds2);
         //maps
         Assert.AreEqual<int>(9, maps.Maps.Count, "Fully mapped destinations");
         Assert.AreEqual<string>("930-936,938-939", maps.Maps[new KeyValuePair<string, string>("AFGHANISTAN-FIX", "AFGHANISTAN-FIX")].ToString());
         Assert.AreEqual<string>("9370-9376", maps.Maps[new KeyValuePair<string, string>("AFGHANISTAN-MOBILE OTHERS", "AFGHANISTAN-FIX")].ToString());
         Assert.AreEqual<string>("9377", maps.Maps[new KeyValuePair<string, string>("AFGHANISTAN-MOBILE AREEBA", "AFGHANISTAN-FIX")].ToString());
         Assert.AreEqual<string>("9378", maps.Maps[new KeyValuePair<string, string>("AFGHANISTAN-MOBILE ETISALAT", "AFGHANISTAN-FIX")].ToString());
         Assert.AreEqual<string>("9379", maps.Maps[new KeyValuePair<string, string>("AFGHANISTAN-MOBILE ROSHAN", "AFGHANISTAN-FIX")].ToString());
         Assert.AreEqual<string>("3550-3553,35550-35565,3557-3559", maps.Maps[new KeyValuePair<string, string>("ALBANIA", "ALBANIA")].ToString());
         Assert.AreEqual<string>("35566-35569", maps.Maps[new KeyValuePair<string, string>("ALBANIA-MOBILE", "ALBANIA")].ToString());
         Assert.AreEqual<string>("3554", maps.Maps[new KeyValuePair<string, string>("ALBANIA-TIRANA", "ALBANIA")].ToString());
         //lost area codes
         Assert.AreEqual<int>(6, maps.LostAreaCodes.Count, "Lost areas");
         Assert.AreEqual<string>("9370-9376", maps.LostAreaCodes["AFGHANISTAN-MOBILE OTHERS"].ToString());
         Assert.AreEqual<string>("9377", maps.LostAreaCodes["AFGHANISTAN-MOBILE AREEBA"].ToString());
         Assert.AreEqual<string>("9378", maps.LostAreaCodes["AFGHANISTAN-MOBILE ETISALAT"].ToString());
         Assert.AreEqual<string>("9379", maps.LostAreaCodes["AFGHANISTAN-MOBILE ROSHAN"].ToString());
         Assert.AreEqual<string>("35566-35569", maps.LostAreaCodes["ALBANIA-MOBILE"].ToString());
         Assert.AreEqual<string>("3554", maps.LostAreaCodes["ALBANIA-TIRANA"].ToString());
         //gained area codes
         Assert.AreEqual<int>(2, maps.GainedAreaCodes.Count, "Gained areas");
         Assert.AreEqual<string>("937", maps.GainedAreaCodes["AFGHANISTAN-FIX"].ToString());
         Assert.AreEqual<string>("3554,35566-35569", maps.GainedAreaCodes["ALBANIA"].ToString());
      }

      #endregion

      [TestMethod, Description("Asserts find area/destination works correctly within a destination set")]
      public void DestinationSet_FindArea()
      {
         Assert.AreEqual<string>("Denmark - Paid800", m_DestSetCarrier.FindArea(458021));
         Assert.AreEqual<string>("Denmark - Mobile TDK", m_DestSetCarrier.FindArea("45201"));
         //In this case we are looking up a code which is too short, the first match is returned
         Assert.AreEqual<string>("Denmark - Mobile Others", m_DestSetCarrier.FindDestinations("452").First());
      }

      [TestMethod, Description("Asserts find area/destination works correctly within a destination set")]
      public void DestinationSet_FindAreaFullyCovered()
      {
         Assert.AreEqual<string>("DENMARK MOBILE TDC", m_DestSetRouting.FindArea("45200"));
      }

      [TestMethod, Description("Asserts find area/destination works correctly within a destination set")]
      public void DestinationSet_FindDestinationFullyCovered()
      {
         Assert.AreEqual<string>("DENMARK MOBILE TDC", m_DestSetRouting.FindFirstMatchingDestination(452));
      }

      [TestMethod, Description("Asserts find area/destination works correctly within a destination set")]
      public void DestinationSet_FindExactAreaFullyCoveredByOtherDests()
      {
         Assert.IsNull(m_DestSetRouting.FindArea("452"));
      }

      [TestMethod, Description("Asserts find area/destination works correctly within a destination set")]
      public void DestinationSet_FindExactAreaFullyCoveredEachCode()
      {
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457010"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457011"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457012"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457013"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457014"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457015"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457016"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457017"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457018"));
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("457019"));

      }

      [TestMethod, Description("Asserts find area/destination works correctly within a destination set")]
      public void DestinationSet_FindExactAreaExactCovered()
      {
         Assert.AreEqual<string>("DENMARK FIXED-TDC", m_DestSetRouting.FindArea("45701"));
      }

      [TestMethod, Description("Asserts find area/destination works correctly within a destination set")]
      public void DestinationSet_FindExactAreaMoreSpecific()
      {
         Assert.AreEqual<string>("DENMARK FIXED-PUBLIC PAY", m_DestSetRouting.FindArea("459091"));
      }
   }
}
