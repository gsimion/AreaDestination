namespace AreaDestinationVisualizer
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Text;

   /// <summary>
   /// Static reference data class containing generic vector with data.
   /// </summary>
   public static class ReferenceData
   {
      /// <summary>
      /// Vector containing country names.
      /// </summary>
      public static readonly string[] CountryNames = new string[] { 
         "Afghanistan", 
         "Albania", 
         "Algeria", 
         "American Samoa", 
         "Andorra",
         "Angola",
         "Anguilla",
         "Antarctica",
         "Antigua and Barbuda",
         "Argentina",
         "Armenia",
         "Aruba",
         "Australia",
         "Austria",
         "Azerbaijan",
         "Bahamas",
         "Bahrain",
         "Bangladesh",
         "Barbados",
         "Belarus",
         "Belgium",
         "Belize",
         "Benin",
         "Bermuda",
         "Bhutan",
         "Bolivia",
         "Bosnia and Herzegovina",
         "Botswana",
         "Brazil",
         "British Indian Ocean Territory",
         "British Virgin Islands",
         "Brunei",
         "Bulgaria",
         "Burkina Faso",
         "Burundi",
         "Cambodia",
         "Cameroon",
         "Canada",
         "Cape Verde",
         "Cayman Islands",
         "Central African Republic",
         "Chad",
         "Chile",
         "China",
         "Christmas Island",
         "Cocos Islands",
         "Colombia",
         "Comoros",
         "Cook Islands",
         "Costa Rica",
         "Croatia",
         "Cuba",
         "Curacao",
         "Cyprus",
         "Czech Republic",
         "Democratic Republic of the Congo",
         "Denmark",
         "Djibouti",
         "Dominica",
         "Dominican Republic",
         "East Timor",
         "Ecuador",
         "Egypt",
         "El Salvador",
         "Equatorial Guinea",
         "Eritrea",
         "Estonia",
         "Ethiopia",
         "Falkland Islands",
         "Faroe Islands",
         "Fiji",
         "Finland",
         "France",
         "French Polynesia",
         "Gabon",
         "Gambia",
         "Georgia",
         "Germany",
         "Ghana",
         "Gibraltar",
         "Greece",
         "Greenland",
         "Grenada",
         "Guam",
         "Guatemala",
         "Guernsey",
         "Guinea",
         "Guinea-Bissau",
         "Guyana",
         "Haiti",
         "Honduras",
         "Hong Kong",
         "Hungary",
         "Iceland",
         "India",
         "Indonesia",
         "Iran",
         "Iraq",
         "Ireland",
         "Isle of Man",
         "Israel",
         "Italy",
         "Ivory Coast",
         "Jamaica",
         "Japan",
         "Jersey",
         "Jordan",
         "Kazakhstan",
         "Kenya",
         "Kiribati",
         "Kosovo",
         "Kuwait",
         "Kyrgyzstan",
         "Laos",
         "Latvia",
         "Lebanon",
         "Lesotho",
         "Liberia",
         "Libya",
         "Liechtenstein",
         "Lithuania",
         "Luxembourg",
         "Macao",
         "Macedonia",
         "Madagascar",
         "Malawi",
         "Malaysia",
         "Maldives",
         "Mali",
         "Malta",
         "Marshall Islands",
         "Mauritania",
         "Mauritius",
         "Mayotte",
         "Mexico",
         "Micronesia",
         "Moldova",
         "Monaco",
         "Mongolia",
         "Montenegro",
         "Montserrat",
         "Morocco",
         "Mozambique",
         "Myanmar",
         "Namibia",
         "Nauru",
         "Nepal",
         "Netherlands",
         "Netherlands Antilles",
         "New Caledonia",
         "New Zealand",
         "Nicaragua",
         "Niger",
         "Nigeria",
         "Niue",
         "North Korea",
         "Northern Mariana Islands",
         "Norway",
         "Oman",
         "Pakistan",
         "Palau",
         "Palestine",
         "Panama",
         "Papua New Guinea",
         "Paraguay",
         "Peru",
         "Philippines",
         "Pitcairn",
         "Poland",
         "Portugal",
         "Puerto Rico",
         "Qatar",
         "Republic of the Congo",
         "Reunion",
         "Romania",
         "Russia",
         "Rwanda",
         "Saint Barthelemy",
         "Saint Helena",
         "Saint Kitts and Nevis",
         "Saint Lucia",
         "Saint Martin",
         "Saint Pierre and Miquelon",
         "Saint Vincent and the Grenadines",
         "Samoa",
         "San Marino",
         "Sao Tome and Principe",
         "Saudi Arabia",
         "Senegal",
         "Serbia",
         "Seychelles",
         "Sierra Leone",
         "Singapore",
         "Sint Maarten",
         "Slovakia",
         "Slovenia",
         "Solomon Islands",
         "Somalia",
         "South Africa",
         "South Korea",
         "South Sudan",
         "Spain",
         "Sri Lanka",
         "Sudan",
         "Suriname",
         "Svalbard and Jan Mayen",
         "Swaziland",
         "Sweden",
         "Switzerland",
         "Syria",
         "Taiwan",
         "Tajikistan",
         "Tanzania",
         "Thailand",
         "Togo",
         "Tokelau",
         "Tonga",
         "Trinidad and Tobago",
         "Tunisia",
         "Turkey",
         "Turkmenistan",
         "Turks and Caicos Islands",
         "Tuvalu",
         "U.S. Virgin Islands",
         "Uganda",
         "Ukraine",
         "United Arab Emirates",
         "United Kingdom",
         "United States",
         "Uruguay",
         "Uzbekistan",
         "Vanuatu",
         "Vatican",
         "Venezuela",
         "Vietnam",
         "Wallis and Futuna",
         "Western Sahara",
         "Yemen",
         "Zambia",
      "Zimbabwe"
      };

      /// <summary>
      /// Vector containing country dial codes.
      /// </summary>
      public static readonly ulong[] CountryDialCodes = new ulong[] { 
         93, 
         355, 
         213, 
         1684, 
         376 , 
         244,
         1264,
         672,
         1268,
         54,
         374,
         297,
         61,
         43,
         994,
         1242,
         973,
         880,
         1246,
         375,
         32,
         501,
         229,
         1441,
         975,
         591,
         387,
         267,
         55,
         246,
         1284,
         673,
         359,
         226,
         257,
         855,
         237,
         1,
         238,
         1345,
         236,
         235,
         56,
         86,
         61,
         61,
         57,
         269,
         682,
         506,
         385,
         53,
         599,
         357,
         420,
         243,
         45,
         253,
         1767,
         1809, //1-829, 1-849
         670,
         593,
         20,
         503,
         240,
         291,
         372,
         251,
         500,
         298,
         679,
         358,
         33,
         689,
         241,
         220,
         995,
         49,
         233,
         350,
         30,
         299,
         1473,
         1671,
         502,
         441481,
         224,
         245,
         592,
         509,
         504,
         852,
         36,
         354,
         91,
         62,
         98,
         964,
         353,
         441624,
         972,
         39,
         225,
         1876,
         81,
         441534,
         962,
         7,
         254,
         686,
         383,
         965,
         996,
         856,
         371,
         961,
         266,
         231,
         218,
         423,
         370,
         352,
         853,
         389,
         261,
         265,
         60,
         960,
         223,
         356,
         692,
         222,
         230,
         262,
         52,
         691,
         373,
         377,
         976,
         382,
         1664,
         212,
         258,
         95,
         264,
         674,
         977,
         31,
         599,
         687,
         64,
         505,
         227,
         234,
         683,
         850,
         1670,
         47,
         968,
         92,
         680,
         970,
         507,
         675,
         595,
         51,
         63,
         64,
         48,
         351,
         1787, //1-939
         974,
         242,
         262,
         40,
         7,
         250,
         590,
         290,
         1869,
         1758,
         590,
         508,
         1784,
         685,
         378,
         239,
         966,
         221,
         381,
         248,
         232,
         65,
         1721,
         421,
         386,
         677,
         252,
         27,
         82,
         211,
         34,
         94,
         249,
         597,
         47,
         268,
         46,
         41,
         963,
         886,
         992,
         255,
         66,
         228,
         690,
         676,
         1868,
         216,
         90,
         993,
         1649,
         688,
         1340,
         256,
         380,
         971,
         44,
         1,
         598,
         998,
         678,
         379,
         58,
         84,
         681,
         212,
         967,
         260,
         263
      };

      /// <summary>
      /// Vector containing country two-digits ISO codes.
      /// </summary>
      public static readonly string[] CountryISOCode = new string[] { 
         "AF", 
         "AL", 
         "DZ", 
         "AS", 
         "AD" ,
         "AO",
         "AI",
         "AQ",
         "AG",
         "AR",
         "AM",
         "AW",
         "AU",
         "AT",
         "AZ",
         "BS",
         "BH",
         "BD",
         "BB",
         "BY",
         "BE",
         "BZ",
         "BJ",
         "BM",
         "BT",
         "BO",
         "BA",
         "BW",
         "BR",
         "IO",
         "VG",
         "BN",
         "BG",
         "BF",
         "BI",
         "KH",
         "CM",
         "CA",
         "CV",
         "KY",
         "CF",
         "TD",
         "CL",
         "CN",
         "CX",
         "CC",
         "CO",
         "KM",
         "CK",
         "CR",
         "HR",
         "CU",
         "CW",
         "CY",
         "CZ",
         "CD",
         "DK",
         "DJ",
         "DM",
         "DO",
         "TL",
         "EC",
         "EG",
         "SV",
         "GQ",
         "ER",
         "EE",
         "ET",
         "FK",
         "FO",
         "FJ",
         "FI",
         "FR",
         "PF",
         "GA",
         "GM",
         "GE",
         "DE",
         "GH",
         "GI",
         "GR",
         "GL",
         "GD",
         "GU",
         "GT",
         "GG",
         "GN",
         "GW",
         "GY",
         "HT",
         "HN",
         "HK",
         "HU",
         "IS",
         "IN",
         "ID",
         "IR",
         "IQ",
         "IE",
         "IM",
         "IL",
         "IT",
         "CI",
         "JM",
         "JP",
         "JE",
         "JO",
         "KZ",
         "KE",
         "KI",
         "XK",
         "KW",
         "KG",
         "LA",
         "LV",
         "LB",
         "LS",
         "LR",
         "LY",
         "LI",
         "LT",
         "LU",
         "MO",
         "MK",
         "MG",
         "MW",
         "MY",
         "MV",
         "ML",
         "MT",
         "MH",
         "MR",
         "MU",
         "YT",
         "MX",
         "FM",
         "MD",
         "MC",
         "MN",
         "ME",
         "MS",
         "MA",
         "MZ",
         "MM",
         "NA",
         "NR",
         "NP",
         "NL",
         "AN",
         "NC",
         "NZ",
         "NI",
         "NE",
         "NG",
         "NU",
         "KP",
         "MP",
         "NO",
         "OM",
         "PK",
         "PW",
         "PS",
         "PA",
         "PG",
         "PY",
         "PE",
         "PH",
         "PN",
         "PL",
         "PT",
         "PR",
         "QA",
         "CG",
         "RE",
         "RO",
         "RU",
         "RW",
         "BL",
         "SH",
         "KN",
         "LC",
         "MF",
         "PM",
         "VC",
         "WS",
         "SM",
         "ST",
         "SA",
         "SN",
         "RS",
         "SC",
         "SL",
         "SG",
         "SX",
         "SK",
         "SI",
         "SB",
         "SO",
         "ZA",
         "KR",
         "SS",
         "ES",
         "LK",
         "SD",
         "SR",
         "SJ",
         "SZ",
         "SE",
         "CH",
         "SY",
         "TW",
         "TJ",
         "TZ",
         "TH",
         "TG",
         "TK",
         "TO",
         "TT",
         "TN",
         "TR",
         "TM",
         "TC",
         "TV",
         "VI",
         "UG",
         "UA",
         "AE",
         "GB",
         "US",
         "UY",
         "UZ",
         "VU",
         "VA",
         "VE",
         "VN",
         "WF",
         "EH",
         "YE",
         "ZM",
         "ZW"
      };

      /// <summary>
      /// Gets the  bitmap of a given country.
      /// </summary>
      /// <param name="countryCode">Two-digits ISO country code</param>
      /// <returns>Bitmap containing the copuntry, <value>null</value> if no country found</returns>
      public static Bitmap GetCountryImage(string countryCode)
      {
         if (String.IsNullOrEmpty(countryCode) || countryCode.Length != 2)
            return null;
         switch (countryCode)
         {
            case "AS":
               return Properties.Resources._as;
            case "DO":
               return Properties.Resources._do;
            case "IN":
               return Properties.Resources._in;
            case "IS":
               return Properties.Resources._is;
            case "AD":
               return Properties.Resources.ad;
            case "AE":
               return Properties.Resources.ae;
            case "AF":
               return Properties.Resources.af;
            case "AG":
               return Properties.Resources.ag;
            case "AI":
               return Properties.Resources.ai;
            case "AL":
               return Properties.Resources.al;
            case "AM":
               return Properties.Resources.am;
            case "AO":
               return Properties.Resources.ao;
            case "AQ":
               return Properties.Resources.aq;
            case "AR":
               return Properties.Resources.ar;
            case "AT":
               return Properties.Resources.at;
            case "AU":
               return Properties.Resources.au;
            case "AW":
               return Properties.Resources.aw;
            case "AX":
               return Properties.Resources.ax;
            case "AZ":
               return Properties.Resources.az;
            case "BA":
               return Properties.Resources.ba;
            case "BB":
               return Properties.Resources.bb;
            case "BD":
               return Properties.Resources.bd;
            case "BE":
               return Properties.Resources.be;
            case "BF":
               return Properties.Resources.bf;
            case "BG":
               return Properties.Resources.bg;
            case "BH":
               return Properties.Resources.bh;
            case "BI":
               return Properties.Resources.bi;
            case "BJ":
               return Properties.Resources.bj;
            case "BL":
               return Properties.Resources.bl;
            case "BM":
               return Properties.Resources.bm;
            case "BN":
               return Properties.Resources.bn;
            case "BO":
               return Properties.Resources.bo;
            case "BQ":
               return Properties.Resources.bq;
            case "BR":
               return Properties.Resources.br;
            case "BS":
               return Properties.Resources.bs;
            case "BT":
               return Properties.Resources.bt;
            case "BV":
               return Properties.Resources.bv;
            case "BW":
               return Properties.Resources.bw;
            case "BY":
               return Properties.Resources.by;
            case "BZ":
               return Properties.Resources.bz;
            case "CA":
               return Properties.Resources.ca;
            case "CC":
               return Properties.Resources.cc;
            case "CD":
               return Properties.Resources.cd;
            case "CF":
               return Properties.Resources.cf;
            case "CG":
               return Properties.Resources.cg;
            case "CH":
               return Properties.Resources.ch;
            case "CI":
               return Properties.Resources.ci;
            case "CK":
               return Properties.Resources.ck;
            case "CL":
               return Properties.Resources.cl;
            case "CM":
               return Properties.Resources.cm;
            case "CN":
               return Properties.Resources.cn;
            case "CO":
               return Properties.Resources.co;
            case "CR":
               return Properties.Resources.cr;
            case "CT":
               return Properties.Resources.ct;
            case "CU":
               return Properties.Resources.cu;
            case "CV":
               return Properties.Resources.cv;
            case "CW":
               return Properties.Resources.cw;
            case "CX":
               return Properties.Resources.cx;
            case "CZ":
               return Properties.Resources.cz;
            case "DE":
               return Properties.Resources.de;
            case "DJ":
               return Properties.Resources.dj;
            case "DK":
               return Properties.Resources.dk;
            case "DM":
               return Properties.Resources.dm;
            case "DZ":
               return Properties.Resources.dz;
            case "EC":
               return Properties.Resources.ec;
            case "EE":
               return Properties.Resources.ee;
            case "EG":
               return Properties.Resources.eg;
            case "EH":
               return Properties.Resources.eh;
            case "ER":
               return Properties.Resources.er;
            case "ES":
               return Properties.Resources.es;
            case "ET":
               return Properties.Resources.et;
            case "FI":
               return Properties.Resources.fi;
            case "FJ":
               return Properties.Resources.fj;
            case "FK":
               return Properties.Resources.fk;
            case "FO":
               return Properties.Resources.fo;
            case "FR":
               return Properties.Resources.fr;
            case "GA":
               return Properties.Resources.ga;
            case "GB":
               return Properties.Resources.gb;
            case "GD":
               return Properties.Resources.gd;
            case "GE":
               return Properties.Resources.ge;
            case "GF":
               return Properties.Resources.gf;
            case "GG":
               return Properties.Resources.gg;
            case "GH":
               return Properties.Resources.gh;
            case "GI":
               return Properties.Resources.gi;
            case "GL":
               return Properties.Resources.gl;
            case "GM":
               return Properties.Resources.gm;
            case "GN":
               return Properties.Resources.gn;
            case "GP":
               return Properties.Resources.gp;
            case "GQ":
               return Properties.Resources.gq;
            case "GR":
               return Properties.Resources.gr;
            case "GS":
               return Properties.Resources.gs;
            case "GT":
               return Properties.Resources.gt;
            case "GU":
               return Properties.Resources.gu;
            case "GW":
               return Properties.Resources.gw;
            case "GY":
               return Properties.Resources.gy;
            case "HK":
               return Properties.Resources.hk;
            case "HM":
               return Properties.Resources.hm;
            case "HN":
               return Properties.Resources.hn;
            case "HR":
               return Properties.Resources.hr;
            case "HT":
               return Properties.Resources.ht;
            case "HU":
               return Properties.Resources.hu;
            case "ID":
               return Properties.Resources.id;
            case "IE":
               return Properties.Resources.ie;
            case "IL":
               return Properties.Resources.il;
            case "IM":
               return Properties.Resources.im;
            case "IO":
               return Properties.Resources.io;
            case "IQ":
               return Properties.Resources.iq;
            case "IR":
               return Properties.Resources.ir;
            case "IT":
               return Properties.Resources.it;
            case "JM":
               return Properties.Resources.jm;
            case "JO":
               return Properties.Resources.jo;
            case "JP":
               return Properties.Resources.jp;
            case "KE":
               return Properties.Resources.ke;
            case "KG":
               return Properties.Resources.kg;
            case "KH":
               return Properties.Resources.kh;
            case "KI":
               return Properties.Resources.ki;
            case "KM":
               return Properties.Resources.km;
            case "KN":
               return Properties.Resources.kn;
            case "KP":
               return Properties.Resources.kp;
            case "KR":
               return Properties.Resources.kr;
            case "KW":
               return Properties.Resources.kw;
            case "KY":
               return Properties.Resources.ky;
            case "KZ":
               return Properties.Resources.kz;
            case "LA":
               return Properties.Resources.la;
            case "LB":
               return Properties.Resources.lb;
            case "LC":
               return Properties.Resources.lc;
            case "LI":
               return Properties.Resources.li;
            case "LK":
               return Properties.Resources.lk;
            case "LR":
               return Properties.Resources.lr;
            case "LS":
               return Properties.Resources.ls;
            case "LT":
               return Properties.Resources.lt;
            case "LU":
               return Properties.Resources.lu;
            case "LV":
               return Properties.Resources.lv;
            case "LY":
               return Properties.Resources.ly;
            case "MA":
               return Properties.Resources.ma;
            case "MC":
               return Properties.Resources.mc;
            case "MD":
               return Properties.Resources.md;
            case "ME":
               return Properties.Resources.me;
            case "MF":
               return Properties.Resources.mf;
            case "MG":
               return Properties.Resources.mg;
            case "MK":
               return Properties.Resources.mk;
            case "ML":
               return Properties.Resources.ml;
            case "MM":
               return Properties.Resources.mm;
            case "MN":
               return Properties.Resources.mn;
            case "MO":
               return Properties.Resources.mo;
            case "MQ":
               return Properties.Resources.mq;
            case "MR":
               return Properties.Resources.mr;
            case "MS":
               return Properties.Resources.ms;
            case "MT":
               return Properties.Resources.mt;
            case "MU":
               return Properties.Resources.mu;
            case "MV":
               return Properties.Resources.mv;
            case "MW":
               return Properties.Resources.mw;
            case "MX":
               return Properties.Resources.mx;
            case "MY":
               return Properties.Resources.my;
            case "MZ":
               return Properties.Resources.mz;
            case "NA":
               return Properties.Resources.na;
            case "NC":
               return Properties.Resources.nc;
            case "NE":
               return Properties.Resources.ne;
            case "NF":
               return Properties.Resources.nf;
            case "NG":
               return Properties.Resources.ng;
            case "NI":
               return Properties.Resources.ni;
            case "NL":
               return Properties.Resources.nl;
            case "NO":
               return Properties.Resources.no;
            case "NP":
               return Properties.Resources.np;
            case "NR":
               return Properties.Resources.nr;
            case "NU":
               return Properties.Resources.nu;
            case "NZ":
               return Properties.Resources.nz;
            case "OM":
               return Properties.Resources.om;
            case "PA":
               return Properties.Resources.pa;
            case "PE":
               return Properties.Resources.pe;
            case "PF":
               return Properties.Resources.pf;
            case "PG":
               return Properties.Resources.pg;
            case "PH":
               return Properties.Resources.ph;
            case "PK":
               return Properties.Resources.pk;
            case "PL":
               return Properties.Resources.pl;
            case "PM":
               return Properties.Resources.pm;
            case "PN":
               return Properties.Resources.pn;
            case "PR":
               return Properties.Resources.pr;
            case "PT":
               return Properties.Resources.pt;
            case "PW":
               return Properties.Resources.pw;
            case "PY":
               return Properties.Resources.py;
            case "QA":
               return Properties.Resources.qa;
            case "RE":
               return Properties.Resources.re;
            case "RO":
               return Properties.Resources.ro;
            case "RS":
               return Properties.Resources.rs;
            case "RU":
               return Properties.Resources.ru;
            case "RW":
               return Properties.Resources.rw;
            case "SA":
               return Properties.Resources.sa;
            case "SB":
               return Properties.Resources.sb;
            case "SC":
               return Properties.Resources.sc;
            case "SD":
               return Properties.Resources.sd;
            case "SE":
               return Properties.Resources.se;
            case "SG":
               return Properties.Resources.sg;
            case "SH":
               return Properties.Resources.sh;
            case "SI":
               return Properties.Resources.si;
            case "SJ":
               return Properties.Resources.sj;
            case "SK":
               return Properties.Resources.sk;
            case "SL":
               return Properties.Resources.sl;
            case "SM":
               return Properties.Resources.sm;
            case "SN":
               return Properties.Resources.sn;
            case "SO":
               return Properties.Resources.so;
            case "SR":
               return Properties.Resources.sr;
            case "SS":
               return Properties.Resources.ss;
            case "ST":
               return Properties.Resources.st;
            case "SV":
               return Properties.Resources.sv;
            case "SX":
               return Properties.Resources.sx;
            case "SY":
               return Properties.Resources.sy;
            case "SZ":
               return Properties.Resources.sz;
            case "TC":
               return Properties.Resources.tc;
            case "TD":
               return Properties.Resources.td;
            case "TF":
               return Properties.Resources.tf;
            case "TG":
               return Properties.Resources.tg;
            case "TH":
               return Properties.Resources.th;
            case "TJ":
               return Properties.Resources.tj;
            case "TK":
               return Properties.Resources.tk;
            case "TL":
               return Properties.Resources.tl;
            case "TM":
               return Properties.Resources.tm;
            case "TN":
               return Properties.Resources.tn;
            case "TO":
               return Properties.Resources.to;
            case "TR":
               return Properties.Resources.tr;
            case "TT":
               return Properties.Resources.tt;
            case "TW":
               return Properties.Resources.tw;
            case "TZ":
               return Properties.Resources.tz;
            case "UA":
               return Properties.Resources.ua;
            case "UG":
               return Properties.Resources.ug;
            case "US":
               return Properties.Resources.us;
            case "UY":
               return Properties.Resources.uy;
            case "UZ":
               return Properties.Resources.uz;
            case "VA":
               return Properties.Resources.va;
            case "VC":
               return Properties.Resources.vc;
            case "VE":
               return Properties.Resources.ve;
            case "VG":
               return Properties.Resources.vg;
            case "VI":
               return Properties.Resources.vi;
            case "VN":
               return Properties.Resources.vn;
            case "VU":
               return Properties.Resources.vu;
            case "WF":
               return Properties.Resources.wf;
            case "WS":
               return Properties.Resources.ws;
            case "YE":
               return Properties.Resources.ye;
            case "YT":
               return Properties.Resources.yt;
            case "ZA":
               return Properties.Resources.za;
            case "ZM":
               return Properties.Resources.zm;
            case "ZW":
               return Properties.Resources.zw;
         }
         return null;
      }
   }
}