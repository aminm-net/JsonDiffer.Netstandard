using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDiffer.Tests.Data
{
    public static class SampleJsonData
    {
        public const string Sample1 = @"{
                           ""destination_addresses"":[
                              ""Napa County, CA, USA"",
                              ""Napa County2, CA, USA"",
                              ""Napa County3, CA, USA""
                           ],
                           ""origin_addresses"":[
                              ""New York, NY, USA""
                           ],
                           ""rows"":[
                              {
                                 ""elements"":[
                                    {
                                       ""distance"":{
                                          ""text"":""227 mi"",
                                          ""value"":36546.8
                                       },
                                       ""duration"":{
                                          ""text"":""3 hours 54 mins"",
                                          ""value"":14064
                                       },
                                       ""magical"":{
                                          ""text"":""227 mi"",
                                          ""value"":365468
                                       },
                                       ""status"":""OK""
                                    },
                                    {
                                       ""distance"":{
                                          ""text"":""94.6 mi"",
                                          ""value"":152193
                                       }
                                    }
                                 ],
                                 ""status"":""OK""
                              }
                           ]
                    }";
        public const string Sample2 = @"{
                           ""destination_addresses"":[
                              ""Washington, DC, USA"",
                              ""Napa County, CA, USA"",
                              ""Orange County, CA, USA"",
                           ],
                           ""origin_addresses"":[
                              ""New York, NY, USA""
                           ],
                           ""rows"":[
                              {
                                 ""elements"":[
                                    {
                                       ""distance"":{
                                          ""text"":""227 mi""
                                       },
                                       ""magical"":{
                                          ""text"":""227 mi"",
                                          ""value"":365468
                                       },
                                       ""status"":""NOK""
                                    },
                                    {
                                       ""distance"":{
                                          ""value"":1521.93
                                       }
                                    }
                                 ],
                                 ""status"":""NOK""
                              }
                           ]
                    }";

        public static class Expected
        {
            public const string Sample1Diffrencesample2 = @"{
                           ""*destination_addresses"":[
                              ""Napa County, CA, USA"",
                              ""Napa County2, CA, USA"",
                              ""Napa County3, CA, USA""
                           ],
                           ""*rows"":[
                              {
                                 ""*elements"":[
                                    {
                                       ""*distance"":{
                                          ""-value"":36546.8
                                       },
                                       ""-duration"":{
                                          ""text"":""3 hours 54 mins"",
                                          ""value"":14064
                                       },
                                       ""*status"":""OK""
                                    },
                                    {
                                       ""*distance"":{
                                          ""-text"":""94.6 mi"",
                                          ""*value"":152193
                                       }
                                    }
                                 ],
                                 ""*status"":""OK""
                              }
                           ]
                    }";
            public const string Sample2Diffrencesample1 = @"{
                           ""*destination_addresses"":[
                              ""Washington, DC, USA"",
                              ""Napa County, CA, USA"",
                              ""Orange County, CA, USA""
                           ],
                           ""*rows"":[
                              {
                                 ""*elements"":[
                                    {
                                       ""*distance"":{
                                          ""+value"":36546.8
                                       },
                                       ""+duration"":{
                                          ""text"":""3 hours 54 mins"",
                                          ""value"":14064
                                       },
                                       ""*status"":""NOK""
                                    },
                                    {
                                       ""*distance"":{
                                          ""+text"":""94.6 mi"",
                                          ""*value"":1521.93
                                       }
                                    }
                                 ],
                                 ""*status"":""NOK""
                              }
                           ]
                    }";
        }
    }
}
