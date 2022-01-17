
namespace JsonDiffer.Tests.Data
{
    public static class SampleJsonData
    {
        public const string Sample01 = @"{
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
        public const string Sample02 = @"{
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

        public const string Sample03 = @"[
	                            {
		                            ""id"": ""0001"",
		                            ""type"": ""donut"",
		                            ""name"": ""Cake"",
		                            ""ppu"": 0.55,
		                            ""batters"":
			                            {
				                            ""batter"":
					                            [
						                            { ""id"": ""1001"", ""type"": ""Regular"" },
						                            { ""id"": ""1002"", ""type"": ""Chocolate"" },
						                            { ""id"": ""1003"", ""type"": ""Blueberry"" },
						                            { ""id"": ""1004"", ""type"": ""Devil's Food"" }
					                            ]
			                            },
		                            ""topping"":
			                            [
				                            { ""id"": ""5001"", ""type"": ""None"" },
				                            { ""id"": ""5002"", ""type"": ""Glazed"" },
				                            { ""id"": ""5005"", ""type"": ""Sugar"" },
				                            { ""id"": ""5007"", ""type"": ""Powdered Sugar"" },
				                            { ""id"": ""5006"", ""type"": ""Chocolate with Sprinkles"" },
				                            { ""id"": ""5003"", ""type"": ""Chocolate"" },
				                            { ""id"": ""5004"", ""type"": ""Maple"" }
			                            ]
	                            },
	                            {
		                            ""id"": ""0002"",
		                            ""type"": ""donut"",
		                            ""name"": ""Raised"",
		                            ""ppu"": 0.55,
		                            ""batters"":
			                            {
				                            ""batter"":
					                            [
						                            { ""id"": ""1001"", ""type"": ""Regular"" }
					                            ]
			                            },
		                            ""topping"":
			                            [
				                            { ""id"": ""5001"", ""type"": ""None"" },
				                            { ""id"": ""5002"", ""type"": ""Glazed"" },
				                            { ""id"": ""5005"", ""type"": ""Sugar"" },
				                            { ""id"": ""5003"", ""type"": ""Chocolate"" },
				                            { ""id"": ""5004"", ""type"": ""Maple"" }
			                            ]
	                            },
	                            {
		                            ""id"": ""0003"",
		                            ""type"": ""donut"",
		                            ""name"": ""Old Fashioned"",
		                            ""ppu"": 0.55,
		                            ""batters"":
			                            {
				                            ""batter"":
					                            [
						                            { ""id"": ""1001"", ""type"": ""Regular"" },
						                            { ""id"": ""1002"", ""type"": ""Chocolate"" }
					                            ]
			                            },
		                            ""topping"":
			                            [
				                            { ""id"": ""5001"", ""type"": ""None"" },
				                            { ""id"": ""5002"", ""type"": ""Glazed"" },
				                            { ""id"": ""5003"", ""type"": ""Chocolate"" },
				                            { ""id"": ""5004"", ""type"": ""Maple"" }
			                            ]
	                            }
                            ]";
        public const string Sample04 = @"[
	                            {
		                            ""id"": ""0001"",
		                            ""type"": ""donut"",
		                            ""name"": ""Cake"",
		                            ""ppu"": 0.65,
                                    ""Calorie"": 257,
		                            ""batters"":
			                            {
				                            ""batter"":
					                            [
						                            { ""id"": ""1001"", ""type"": ""Regular"" },
						                            { ""id"": ""1002"", ""type"": ""Chocolate"" },
						                            { ""id"": ""1003"", ""type"": ""Blueberry"" },
						                            { ""id"": ""1004"", ""type"": ""Devil's Food"" }
					                            ]
			                            },
		                            ""topping"":
			                            [
				                            { ""id"": ""5001"", ""type"": ""None"" },
				                            { ""id"": ""5002"", ""type"": ""Glazed"" },
				                            { ""id"": ""5005"", ""type"": ""Sugar"" },
				                            { ""id"": ""5007"", ""type"": ""Powdered Sugar"" },
				                            { ""id"": ""5006"", ""type"": ""Chocolate with Sprinkles"" },
				                            { ""id"": ""5003"", ""type"": ""Chocolate"" },
				                            { ""id"": ""5004"", ""type"": ""Maple"" }
			                            ]
	                            },
	                            {
		                            ""id"": ""0002"",
		                            ""type"": ""donut"",
		                            ""name"": ""Raised"",
		                            ""ppu"": 0.55,
		                            ""batters"":
			                            {
				                            ""batter"":
					                            [
						                            { ""id"": ""1001"", ""type"": ""Regular"" }
					                            ]
			                            },
		                            ""topping"":
			                            [
				                            { ""id"": ""5001"", ""type"": ""None"" },
				                            { ""id"": ""5002"", ""type"": ""Glazed"" },
				                            { ""id"": ""5005"", ""type"": ""Sugar"" },
				                            { ""id"": ""5003"", ""type"": ""Chocolate"" },
				                            { ""id"": ""5004"", ""type"": ""Candy"" }
			                            ]
	                            },
	                            {
		                            ""id"": ""0003"",
		                            ""type"": ""donut"",
		                            ""name"": ""Old Fashioned"",
		                            ""ppu"": 0.55,
		                            ""batters"":
			                            {
				                            ""batter"":
					                            [
						                            { ""id"": ""1001"", ""type"": ""Regular"" },
						                            { ""id"": ""1002"", ""type"": ""Chocolate"" }
					                            ]
			                            },
		                            ""topping"":
			                            [
				                            { ""id"": ""5001"", ""type"": ""None"" },
				                            { ""id"": ""5002"", ""type"": ""Glazed"" },
				                            { ""id"": ""5003"", ""type"": ""Chocolate"" },
				                            { ""id"": ""5004"", ""type"": ""Maple"" }
			                            ]
	                            }
                            ]";

        public const string Sample05 = @"{
            ""quiz"": {
                ""sport"": {
                    ""q1"": {
                        ""question"": ""Which one is correct team name for NBA?"",
                        ""options"": [
                            ""New York Bulls"",
                            ""Los Angeles Kings"",
                            ""Huston Rocket""
                        ],
                        ""answer"": ""Huston Rocket""
                    }
                },
                ""maths"": {
                    ""q1"": {
                        ""question"": ""5 + 7 = ?"",
                        ""options"": [
                            ""10"",
                            ""11"",
                            ""12"",
                            ""13""
                        ],
                        ""answer"": ""12""
                    }
                }
            }
        }";
        public const string Sample06 = @"{
            ""quiz"": {
                ""sport"": {
                    ""q1"": {
                        ""question"": ""Which one is correct team name in NBA?"",
                        ""options"": [
                            ""New York Bulls"",
                            ""Los Angeles Kings"",
                            ""Juventus FC"",
                            ""Golden State Warriros"",
                            ""Huston Rocket""
                        ],
                        ""answer"": ""Huston Rocket""
                    }
                },
                ""maths"": {
                    ""q1"": {
                        ""question"": ""5 + 7 = ?"",
                        ""options"": [
                            ""10"",
                            ""11"",
                            ""12"",
                            ""13""
                        ],
                        ""answer"": ""12""
                    },
                    ""q2"": {
                        ""question"": ""12 - 8 = ?"",
                        ""options"": [
                            ""1"",
                            ""3"",
                            ""4""
                        ],
                        ""answer"": ""4""
                    }
                }
            }
        }";

        public static class Expected
        {
            public const string Diff0102 = @"{
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
            public const string Diff0201 = @"{
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

            public const string Diff0304 = @"[
                                                                  {
                                                                    ""*ppu"": 0.55,
                                                                    ""+Calorie"": 257
                                                                  },
                                                                  {
                                                                    ""*topping"": [
                                                                      {
                                                                        ""*type"": ""Maple""
                                                                      }
                                                                    ]
                                                                  }
                                                                ]";
            public const string Diff0403 = @"[
                                                                  {
                                                                    ""*ppu"": 0.65,
                                                                    ""-Calorie"": 257
                                                                  },
                                                                  {
                                                                    ""*topping"": [
                                                                      {
                                                                        ""*type"": ""Candy""
                                                                      }
                                                                    ]
                                                                  }
                                                                ]";

            public const string Diff0506 = @"{
                                                              ""*quiz"": {
                                                                ""*sport"": {
                                                                  ""*q1"": {
                                                                    ""*question"": ""Which one is correct team name for NBA?"",
                                                                    ""*options"": [
                                                                      ""Huston Rocket"",
                                                                      ""Golden State Warriros"",
                                                                      ""Huston Rocket""
                                                                    ]
                                                                  }
                                                                },
                                                                ""*maths"": {
                                                                  ""+q2"": {
                                                                    ""question"": ""12 - 8 = ?"",
                                                                    ""options"": [
                                                                      ""1"",
                                                                      ""3"",
                                                                      ""4""
                                                                    ],
                                                                    ""answer"": ""4""
                                                                  }
                                                                }
                                                              }
                                                            }";
            public const string Diff0605 = @"{
                                                                  ""*quiz"": {
                                                                    ""*sport"": {
                                                                      ""*q1"": {
                                                                        ""*question"": ""Which one is correct team name in NBA?"",
                                                                        ""*options"": [
                                                                          ""Juventus FC"",
                                                                          ""Golden State Warriros"",
                                                                          ""Huston Rocket""
                                                                        ]
                                                                      }
                                                                    },
                                                                    ""*maths"": {
                                                                      ""-q2"": {
                                                                        ""question"": ""12 - 8 = ?"",
                                                                        ""options"": [
                                                                          ""1"",
                                                                          ""3"",
                                                                          ""4""
                                                                        ],
                                                                        ""answer"": ""4""
                                                                      }
                                                                    }
                                                                  }
                                                                }";

            public static class OriginalAsDifference
            {
                public const string Diff0102 = @"{
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
                                       ""*status"":""NOK""
                                    },
                                    {
                                       ""*distance"":{
                                          ""-text"":""94.6 mi"",
                                          ""*value"":1521.93
                                       }
                                    }
                                 ],
                                 ""*status"":""NOK""
                              }
                           ]
                    }";

                public const string Diff0201 = @"{
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
                                       ""*status"":""OK""
                                    },
                                    {
                                       ""*distance"":{
                                          ""+text"":""94.6 mi"",
                                          ""*value"":152193
                                       }
                                    }
                                 ],
                                 ""*status"":""OK""
                              }
                           ]
                    }";

                public const string Diff0304 = @"[
                                                                  {
                                                                    ""*ppu"": 0.65,
                                                                    ""+Calorie"": 257
                                                                  },
                                                                  {
                                                                    ""*topping"": [
                                                                      {
                                                                        ""*type"": ""Candy""
                                                                      }
                                                                    ]
                                                                  }
                                                                ]";
                public const string Diff0403 = @"[
                                                                  {
                                                                    ""*ppu"": 0.55,
                                                                    ""-Calorie"": 257
                                                                  },
                                                                  {
                                                                    ""*topping"": [
                                                                      {
                                                                        ""*type"": ""Maple""
                                                                      }
                                                                    ]
                                                                  }
                                                                ]";

                public const string Diff0506 = @"{
                                                              ""*quiz"": {
                                                                ""*sport"": {
                                                                  ""*q1"": {
                                                                    ""*question"": ""Which one is correct team name in NBA?"",
                                                                    ""*options"": [
                                                                      ""Huston Rocket"",
                                                                      ""Golden State Warriros"",
                                                                      ""Huston Rocket""
                                                                    ]
                                                                  }
                                                                },
                                                                ""*maths"": {
                                                                  ""+q2"": {
                                                                    ""question"": ""12 - 8 = ?"",
                                                                    ""options"": [
                                                                      ""1"",
                                                                      ""3"",
                                                                      ""4""
                                                                    ],
                                                                    ""answer"": ""4""
                                                                  }
                                                                }
                                                              }
                                                            }";
                public const string Diff0605 = @"{
                                                                  ""*quiz"": {
                                                                    ""*sport"": {
                                                                      ""*q1"": {
                                                                        ""*question"": ""Which one is correct team name for NBA?"",
                                                                        ""*options"": [
                                                                          ""Juventus FC"",
                                                                          ""Golden State Warriros"",
                                                                          ""Huston Rocket""
                                                                        ]
                                                                      }
                                                                    },
                                                                    ""*maths"": {
                                                                      ""-q2"": {
                                                                        ""question"": ""12 - 8 = ?"",
                                                                        ""options"": [
                                                                          ""1"",
                                                                          ""3"",
                                                                          ""4""
                                                                        ],
                                                                        ""answer"": ""4""
                                                                      }
                                                                    }
                                                                  }
                                                                }";
            }
        }
    }
}
