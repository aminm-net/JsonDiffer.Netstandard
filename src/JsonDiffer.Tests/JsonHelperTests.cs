using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Xunit;

namespace JsonDiff.Core.Tests
{
    public class JsonHelperTests
    {
        [Fact]
        public void Modified_properties_should_prefixed_with_asterisk_symbol()
        {
            // setup
            var j1 = JToken.Parse("{'id':1, 'foo':'bar'}");
            var j2 = JToken.Parse("{'id':1, 'foo':'baz'}");

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.StartsWith("*", (diff.First as JProperty).Name);
        }

        [Fact]
        public void Added_properties_should_prefixed_with_plus_symbol()
        {
            // setup
            var j1 = JToken.Parse("{'id':1 }");
            var j2 = JToken.Parse("{'id':1, 'foo':'baz'}");

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.StartsWith("+", (diff.First as JProperty).Name);
        }

        [Fact]
        public void Removed_properties_should_prefixed_with_dash_symbol()
        {
            // setup
            var j1 = JToken.Parse("{'id':1, 'foo':'bar'}");
            var j2 = JToken.Parse("{'id':1 }");

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.StartsWith("-", (diff.First as JProperty).Name);
        }

        [Fact]
        public void Result_should_be_null_when_both_operands_are_empty()
        {
            // setup
            var j1 = JToken.Parse("{}");
            var j2 = JToken.Parse("{}");

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.Null(diff);
        }

        [Fact]
        public void Result_should_be_null_when_both_operands_are_null()
        {
            // setup
            var j1 = default(JToken);
            var j2 = default(JToken);

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.Null(diff);
        }

        [Fact]
        public void Result_should_be_null_when_either_of_operands_is_null()
        {
            // setup
            var j1 = default(JToken);
            var j2 = JToken.Parse("{}");

            // act
            var diff1 = j1.Difference(j2);
            var diff2 = j2.Difference(j1);

            // assert
            Assert.Null(diff1);
            Assert.Null(diff2);
        }

        [Fact]
        public void Differenc_should_contain_left_hand_side_operand_value_for_simple_key_value_objects()
        {
            // setup
            var j1 = JToken.Parse("{'id':1, 'foo':'bar'}");
            var j2 = JToken.Parse("{'id':1, 'foo':'baz'}");

            // act
            var diff12 = j1.Difference(j2);
            var diff21 = j2.Difference(j1);

            var expected12 = JToken.Parse("{'*foo':'bar'}");
            var expected21 = JToken.Parse("{'*foo':'baz'}");


            // assert
            Assert.Equal(expected12, diff12);
            Assert.Equal(expected21, diff21);
        }

        [Fact]
        public void Result_should_throw_invalid_operation_exception_when_operands_types_do_not_match()
        {
            // setup
            var j1 = JToken.Parse("[{'areray':'foo'}]");
            var j2 = JToken.Parse("{'object':'bar'}");

            // act
            var exception = Record.Exception(() =>
            {
                var diff = j1.Difference(j2);
            });

            // assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Contains("JArray", exception.Message);
            Assert.Contains("JObject", exception.Message);
        }

        [Fact]
        public void Differenc_should_be_the_entire_left_hand_side_operand_value_for_simple_arrays()
        {
            // setup
            var j1 = JToken.Parse("['1','2','3']");
            var j2 = JToken.Parse("['1','3']");

            // act
            var diff12 = j1.Difference(j2);
            var diff21 = j2.Difference(j1);

            // assert
            Assert.Equal(j1, diff12);
            Assert.Equal(j2, diff21);
        }

        [Fact]
        public void Differenc_should_capture_modifications_for_simple_key_value_objects_complex_json_IIIIIIIIIII()
        {
            // setup
            var j1 = JToken.Parse(json1);
            var j2 = JToken.Parse(json2);

            // act
            var diff12 = j1.Difference(j2);
            var diff21 = j2.Difference(j1);

            var expected12 = JToken.Parse(j1DiffJ2);
            var expected21 = JToken.Parse(j2DiffJ1);

            // assert
            Assert.True(JToken.DeepEquals(expected12, diff12));
            Assert.True(JToken.DeepEquals(expected21, diff21));
        }

        readonly string json1 = @"{
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
        readonly string json2 = @"{
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
        readonly string j1DiffJ2 = @"{
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
        readonly string j2DiffJ1 = @"{
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



