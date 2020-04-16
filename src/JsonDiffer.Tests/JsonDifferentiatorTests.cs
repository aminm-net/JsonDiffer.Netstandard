using static JsonDiffer.Tests.Data.SampleJsonData;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace JsonDiffer.Tests
{
    public class JsonDifferentiatorTests
    {
        [Fact]
        public void Modified_properties_should_prefixed_with_asterisk_symbol()
        {
            // setup
            var j1 = JToken.Parse("{'id':1, 'foo':'bar'}");
            var j2 = JToken.Parse("{'id':1, 'foo':'baz'}");

            // act
            var diff = JsonDifferentiator.Differentiate(j1,j2);

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
            var diff = JsonDifferentiator.Differentiate(j1,j2);

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
            var diff = JsonDifferentiator.Differentiate(j1,j2);

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
            var diff = JsonDifferentiator.Differentiate(j1,j2);

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
            var diff = JsonDifferentiator.Differentiate(j1,j2);

            // assert
            Assert.Null(diff);
        }

        [Fact]
        public void Result_should_be_the_other_when_one_of_operands_is_null()
        {
            // setup
            var j1 = default(JToken);
            var j2 = JToken.Parse("{'x':1}");

            // act
            var diff1 = JsonDifferentiator.Differentiate(j1,j2);
            var diff2 = JsonDifferentiator.Differentiate(j2,j1);

            // assert
            Assert.Equal(JToken.Parse("{'+x':1}"), diff1);
            Assert.Equal(JToken.Parse("{'-x':1}"),diff2);
        }

        [Fact]
        public void Differenc_should_contain_left_hand_side_operand_value_for_simple_key_value_objects()
        {
            // setup
            var j1 = JToken.Parse("{'id':1, 'foo':'bar'}");
            var j2 = JToken.Parse("{'id':1, 'foo':'baz'}");

            // act
            var diff12 = JsonDifferentiator.Differentiate(j1,j2);
            var diff21 = JsonDifferentiator.Differentiate(j2,j1);

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
                var diff = JsonDifferentiator.Differentiate(j1,j2);
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
            var diff12 = JsonDifferentiator.Differentiate(j1,j2);
            var diff21 = JsonDifferentiator.Differentiate(j2,j1);

            // assert
            Assert.Equal(j1, diff12);
            Assert.Equal(j2, diff21);
        }

        [Theory]
        [InlineData(Sample01, Sample02, Expected.Diff0102, Expected.Diff0201)]
        [InlineData(Sample03, Sample04, Expected.Diff0304, Expected.Diff0403)]
        [InlineData(Sample05, Sample06, Expected.Diff0506, Expected.Diff0605)]
        public void Differenc_should_capture_modifications_for_simple_key_value_objects_complex_json(string first, string second, string expected1Diff2, string expected2Diff1)
        {
            // setup
            var j1 = JToken.Parse(first);
            var j2 = JToken.Parse(second);

            // act
            var actual12 = JsonDifferentiator.Differentiate(j1,j2);
            var actual21 = JsonDifferentiator.Differentiate(j2,j1);

            var expected12 = JToken.Parse(expected1Diff2);
            var expected21 = JToken.Parse(expected2Diff1);

            // assert
            Assert.True(JToken.DeepEquals(expected12, actual12));
            Assert.True(JToken.DeepEquals(expected21, actual21));
        }

        [Fact]
        public void Differenc_should_show_differences_for_all_objects_inside_Array()
        {
            // setup
            var j1 = JToken.Parse("{'x':[{'foo':'bar'},{'baz':'qux'}]}");
            var j2 = JToken.Parse("{'x':[{'foo':'quux'},{'corge':'grault'}]}");

            // act
            var diff12 = JsonDifferentiator.Differentiate(j1, j2);
            var diff21 = JsonDifferentiator.Differentiate(j2, j1);
            var expected12 = JToken.Parse("{'*x':[{'*foo':'bar'},{'-baz':'qux','+corge':'grault'}]}");
            var expected21 = JToken.Parse("{'*x':[{'*foo':'quux'},{'+baz':'qux','-corge':'grault'}]}");

            // assert
            Assert.True(JToken.DeepEquals(expected12, diff12));
            Assert.True(JToken.DeepEquals(expected21, diff21));
        }

        [Fact]
        public void Differenc_should_show_differences_for_all_objects_inside_Array_2()
        {
            // setup
            var j1 = JToken.Parse("[{'foo':'bar'},{'baz':'qux'}]");
            var j2 = JToken.Parse("[{'foo':'quux'},{'corge':'grault'}]");

            // act
            var diff12 = JsonDifferentiator.Differentiate(j1, j2);
            var diff21 = JsonDifferentiator.Differentiate(j2, j1);
            var expected12 = JToken.Parse("[{'*foo':'bar'},{'-baz':'qux','+corge':'grault'}]");
            var expected21 = JToken.Parse("[{'*foo':'quux'},{'+baz':'qux','-corge':'grault'}]");

            // assert
            Assert.True(JToken.DeepEquals(expected12, diff12));
            Assert.True(JToken.DeepEquals(expected21, diff21));
        }
    }
}