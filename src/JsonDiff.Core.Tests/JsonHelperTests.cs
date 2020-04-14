using Newtonsoft.Json.Linq;
using Xunit;

namespace JsonDiff.Core.Tests
{
    public class JsonHelperTests
    {
        [Fact]
        public void Differenc_result_should_be_null_when_objects_are_empty()
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
        public void Differenc_result_should_be_null_when_objects_are_null()
        {
            // setup
            var j1 = JToken.Parse("{}");
            var j2 = JToken.Parse("{}");

            j1 = null;
            j2 = null;

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.Null(diff);
        }

        [Fact]
        public void Differenc_result_should_be_null_when_left_hand_object_is_null()
        {
            // setup
            var j1 = JToken.Parse("{}");
            var j2 = default(JToken);

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.Null(diff);
        }

        [Fact]
        public void Differenc_result_should_be_null_when_right_hand_object_is_null()
        {
            // setup
            var j1 = JToken.Parse("{}");
            var j2 = default(JToken);

            // act
            var diff = j1.Difference(j2);

            // assert
            Assert.Null(diff);
        }

        [Fact]
        public void Differenc_should_capture_original_valueon_the_left_for_simple_key_value_objects()
        {
            // setup
            var j1 = JToken.Parse(@"{'id':'1', 'foo':'bar'}");
            var j2 = JToken.Parse(@"{'id':'1', 'foo':'baz'}");

            // act
            var diff12 = j1.Difference(j2);
            var diff21 = j2.Difference(j1);

            var expected12 = JToken.Parse(@"{'*foo':'bar'}");
            var expected21 = JToken.Parse(@"{'*foo':'baz'}");


            // assert
            Assert.Equal(expected12, diff12);
            Assert.Equal(expected21, diff21);
        }
    }
}
