using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace JsonDiffer.Tests
{
    public class JsonHelperTests
    {
        [Fact]
        public void JToken_difference_extention_method_should_work_as_it_should()
        {
            // setup
            var j1 = JToken.Parse("{'id':1, 'foo':'bar'}");
            var j2 = JToken.Parse("{'id':1, 'foo':'baz'}");

            // act
            var diff = j1.Difference(j2);
            var expected = JToken.Parse(@"{""*foo"":[""baz""]}");

            // assert
            Assert.StartsWith("*", (diff.First as JProperty).Name);
            Assert.Equal(expected, diff);
        }
    }
}



