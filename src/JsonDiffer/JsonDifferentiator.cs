using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace JsonDiffer
{
    public class JsonDifferentiator
    {
        public OutputMode OutputMode { get; private set; }
        public bool ShowOriginalValues { get; private set; }
        
        public JsonDifferentiator(OutputMode outputMode , bool showOriginalValues)
        {
            this.OutputMode = outputMode;
            this.ShowOriginalValues = showOriginalValues;
        }

        private static TargetNode PointTargetNode(JToken diff, string property, ChangeMode mode, OutputMode outMode)
        {
            string symbol = string.Empty;

            switch (mode)
            {
                case ChangeMode.Changed:
                    symbol = outMode == OutputMode.Symbol ? $"*{property}" : "changed";
                    break;

                case ChangeMode.Added:
                    symbol = outMode == OutputMode.Symbol ? $"+{property}" : "added";
                    break;

                case ChangeMode.Removed:
                    symbol = outMode == OutputMode.Symbol ? $"-{property}" : "removed";
                    break;
            }

            if (outMode == OutputMode.Detailed && diff[symbol] == null)
            {
                diff[symbol] = JToken.Parse("{}");
            }

            return new TargetNode(symbol, (outMode == OutputMode.Symbol) ? null : property);

        }

        public static JToken Differentiate(JToken first, JToken second, OutputMode outputMode = OutputMode.Symbol, bool showOriginalValues = false)
        {
            if (JToken.DeepEquals(first, second)) return null;

            if (first != null && second != null && first?.GetType() != second?.GetType())
                throw new InvalidOperationException($"Operands' types must match; '{first.GetType().Name}' <> '{second.GetType().Name}'");

            var propertyNames = (first?.Children() ?? default).Union(second?.Children() ?? default)?.Select(_ => (_ as JProperty)?.Name)?.Distinct();

            if (!propertyNames.Any() && (first is JValue || second is JValue))
            {
                return (first == null) ? second : first;
            }

            var difference = JToken.Parse("{}");

            foreach (var property in propertyNames)
            {
                if (property == null)
                {
                    if (first == null)
                    {
                        difference = second;
                    }
                    // array of object?
                    else if (first is JArray && first.Children().All(c => !(c is JValue)))
                    {
                        var difrences = new JArray();
                        //var mode = second == null ? '-' : '*';
                        var maximum = Math.Max(first?.Count() ?? 0, second?.Count() ?? 0);

                        for (int i = 0; i < maximum; i++)
                        {
                            var firstsItem = first?.ElementAtOrDefault(i);
                            var secondsItem = second?.ElementAtOrDefault(i);

                            var diff = Differentiate(firstsItem, secondsItem, outputMode, showOriginalValues);

                            if (diff != null)
                            {
                                difrences.Add(diff);
                            }
                        }

                        if (difrences.HasValues)
                        {
                            difference/*[$"{mode}{property}"] */= difrences;
                        }
                    }
                    else
                    {
                        difference = first;
                    }

                    continue;
                }

                if (first?[property] == null)
                {
                    var secondVal = second?[property]?.Parent as JProperty;

                    //if (difference["added"] == null)
                    //{
                    //    difference["added"] = JToken.Parse("{}");
                    //}
                    var targetNode = PointTargetNode(difference, property, ChangeMode.Added, outputMode);

                    //targetNode = secondVal.Value;

                    if (targetNode.Property != null)
                    {
                        difference[targetNode.Symbol][targetNode.Property] = secondVal.Value;
                    }
                    else
                        difference[targetNode.Symbol] = secondVal.Value;

                    //difference[$"+{property}"] = secondVal.Value;
                    //difference[$"+{property}"] = secondVal.Value;

                    continue;
                }

                if (second?[property] == null)
                {
                    var firstVal = first?[property]?.Parent as JProperty;

                    //if (difference["removed"] == null)
                    //{
                    //    difference["removed"] = JToken.Parse("{}");
                    //}

                    var targetNode = PointTargetNode(difference, property, ChangeMode.Removed, outputMode);

                    if (targetNode.Property != null)
                    {
                        difference[targetNode.Symbol][targetNode.Property] = firstVal.Value;
                    }
                    else
                        difference[targetNode.Symbol] = firstVal.Value;
                    //targetNode = firstVal.Value;


                    //difference["removed"][property] = firstVal.Value;

                    continue;
                }

                if (first?[property] is JValue value)
                {
                    if (!JToken.DeepEquals(first?[property], second?[property]))
                    {
                        var targetNode = PointTargetNode(difference, property, ChangeMode.Changed, outputMode);

                        //targetNode = showOriginalValues ? second?[property] : value;
                        //if (difference["changed"] == null)
                        //{
                        //    difference["changed"] = JToken.Parse("{}");
                        //}

                        if (targetNode.Property != null)
                        {
                            difference[targetNode.Symbol][targetNode.Property] = showOriginalValues ? second?[property] : value;
                        }
                        else
                            difference[targetNode.Symbol] = showOriginalValues ? second?[property] : value;
                        //difference["changed"][property] = showOriginalValues ? second?[property] : value;
                    }

                    continue;
                }

                if (first?[property] is JObject)
                {

                    var targetNode = second?[property] == null
                        ? PointTargetNode(difference, property, ChangeMode.Removed, outputMode)
                        : PointTargetNode(difference, property, ChangeMode.Changed, outputMode);


                    //var mode = second?[property] == null ? "removed" : "changed";

                    //if (difference[mode] == null)
                    //{
                    //    difference[mode] = JToken.Parse("{}");
                    //}

                    var firstsItem = first[property];
                    var secondsItem = second[property];

                    var diffrence = Differentiate(firstsItem, secondsItem, outputMode, showOriginalValues);

                    if (diffrence != null /*&& diffrence.Count() > 0*/)
                    {

                        if (targetNode.Property != null)
                        {
                            difference[targetNode.Symbol][targetNode.Property] = diffrence;
                        }
                        else
                            difference[targetNode.Symbol] = diffrence;

                    }

                    continue;
                }

                if (first?[property] is JArray)
                {
                    var difrences = new JArray();

                    var targetNode = second?[property] == null
                       ? PointTargetNode(difference, property, ChangeMode.Removed, outputMode)
                       : PointTargetNode(difference, property, ChangeMode.Changed, outputMode);

                    //var mode = second?[property] == null ? "removed" : "changed";

                    //if (difference[mode] == null)
                    //{
                    //    difference[mode] = JToken.Parse("{}");
                    //}

                    var maximum = Math.Max(first?[property]?.Count() ?? 0, second?[property]?.Count() ?? 0);

                    for (int i = 0; i < maximum; i++)
                    {
                        var firstsItem = first[property]?.ElementAtOrDefault(i);
                        var secondsItem = second[property]?.ElementAtOrDefault(i);

                        var diff = Differentiate(firstsItem, secondsItem, outputMode, showOriginalValues);

                        if (diff != null)
                        {
                            difrences.Add(diff);
                        }
                    }

                    if (difrences.HasValues)
                    {
                        //difference[mode][property] = difrences;
                        if (targetNode.Property != null)
                        {
                            difference[targetNode.Symbol][targetNode.Property] = difrences;
                        }
                        else
                            difference[targetNode.Symbol] = difrences;
                        //targetNode = difrences;
                    }

                    continue;
                }
            }

            return difference;
        }

        public JToken Differentiate(JToken first, JToken second)
        {
            return Differentiate(first, second, this.OutputMode, this.ShowOriginalValues);
        }
    }
}
