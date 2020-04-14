using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace JsonDiff.Core
{
    public static class JsonHelper
    {
        public static JToken Difference(this JToken first, JToken second)
        {
            if (first == null || second == null || JToken.DeepEquals(first, second)) return null;

            if (second.GetType() != first.GetType()) throw new InvalidOperationException($"Operands type must match. '{first.GetType().Name}' vs '{second.GetType().Name}'");

            var propertyNames = (first?.Children() ?? default).Union((second?.Children() ?? default))?.Select(_ => ((JProperty)_).Name).Distinct();

            var difference = JToken.Parse("{}");

            foreach (var property in propertyNames)
            {
                if (first?[property] == null)
                {
                    var secondVal = second?[property]?.Parent as JProperty;

                    difference[$"+{property}"] = secondVal.Value;

                    continue;
                }

                if (second?[property] == null)
                {
                    var firstVal = first?[property]?.Parent as JProperty;

                    difference[$"-{property}"] = firstVal.Value;

                    continue;
                }

                if (first?[property] is JValue value)
                {
                    if (!JToken.DeepEquals(first?[property], second?[property]))
                    {
                        difference[$"*{property}"] = value;
                    }

                    continue;
                }

                if (first?[property] is JObject)
                {
                    var mode = second?[property] == null ? '-' : '*';
                    var firstsItem = first[property];
                    var secondsItem = second[property];

                    var diffrence = Difference(firstsItem, secondsItem);

                    if (diffrence.Count() > 0)
                    {
                        difference[$"{mode}{property}"] = diffrence;
                    }
                    continue;

                }

                if (first?[property] is JArray)
                {
                    var difrences = new JArray();
                    var mode = second?[property] == null ? '-' : '*';
                    var maximum = Math.Max(first?[property]?.Count() ?? 0, second?[property]?.Count() ?? 0);

                    for (int i = 0; i < maximum; i++)
                    {
                        var firstsItem = first[property]?.ElementAtOrDefault(i);
                        var secondsItem = second[property]?.ElementAtOrDefault(i);

                        var diff = Difference(firstsItem, secondsItem);

                        if (diff.Count() > 0)
                        {
                            difrences.Add(diff);
                        }
                    }

                    if (difrences.Count() > 0)
                    {
                        difference[$"{mode}{property}"] = difrences;
                    }

                    continue;
                }
            }

            return difference;
        }
    }
}