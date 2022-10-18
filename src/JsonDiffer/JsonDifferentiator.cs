using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace JsonDiffer
{
    public class JsonDifferentiator
    {
        public OutputMode OutputMode { get; private set; }
        public ShowValuesOptions ShowValues { get; private set; }

        public JsonDifferentiator(OutputMode outputMode, ShowValuesOptions showValues)
        {
            this.OutputMode = outputMode;
            this.ShowValues = showValues;
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

        public static JToken Differentiate(JToken first, JToken second, OutputMode outputMode = OutputMode.Symbol, ShowValuesOptions showValues = ShowValuesOptions.New)
        {
            if (JToken.DeepEquals(first, second)) return null;

            if (first != null && second != null && first?.GetType() != second?.GetType())
                throw new InvalidOperationException($"Operands' types must match; '{first.GetType().Name}' <> '{second.GetType().Name}'");

            var propertyNames = (first?.Children() ?? default).Union(second?.Children() ?? default)?.Select(_ => (_ as JProperty)?.Name)?.Distinct();

            if (!propertyNames.Any() && (first is JValue || second is JValue))
            {
                if(first == second) return null;
                
                switch (showValues)
                {
                    case ShowValuesOptions.Original:
                        return (first != null) ? new JArray(first) : null;
                    case ShowValuesOptions.New:
                        return (second != null) ? new JArray(second) : null;
                    case ShowValuesOptions.OriginalAndNew:
                        return new JArray(first, second);
                }
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
                        var maximum = Math.Max(first?.Count() ?? 0, second?.Count() ?? 0);

                        for (int i = 0; i < maximum; i++)
                        {
                            var firstsItem = first?.ElementAtOrDefault(i);
                            var secondsItem = second?.ElementAtOrDefault(i);

                            var diff = Differentiate(firstsItem, secondsItem, outputMode, showValues);

                            if (diff != null)
                            {
                                difrences.Add(diff);
                            }
                        }

                        if (difrences.HasValues)
                        {
                            difference = difrences;
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

                    var targetNode = PointTargetNode(difference, property, ChangeMode.Added, outputMode);

                    if (targetNode.Property != null)
                    {
                        difference[targetNode.Symbol][targetNode.Property] = secondVal.Value;
                    }
                    else
                        difference[targetNode.Symbol] = secondVal.Value;

                    continue;
                }

                if (second?[property] == null)
                {
                    var firstVal = first?[property]?.Parent as JProperty;

                    var targetNode = PointTargetNode(difference, property, ChangeMode.Removed, outputMode);

                    if (targetNode.Property != null)
                    {
                        difference[targetNode.Symbol][targetNode.Property] = firstVal.Value;
                    }
                    else
                        difference[targetNode.Symbol] = firstVal.Value;

                    continue;
                }

                if (first?[property] is JValue value)
                {
                    if (!JToken.DeepEquals(first?[property], second?[property]))
                    {
                        var targetNode = PointTargetNode(difference, property, ChangeMode.Changed, outputMode);

                        if (targetNode.Property != null)
                        {
                            if (showValues == ShowValuesOptions.Original)
                            {
                                difference[targetNode.Symbol][targetNode.Property] = new JArray(value);
                            }
                            else if (showValues == ShowValuesOptions.New)
                            {
                                difference[targetNode.Symbol][targetNode.Property] = new JArray(second?[property]);
                            }
                            else
                            {
                                difference[targetNode.Symbol][targetNode.Property] = new JArray(value, second?[property]);
                            }

                        }
                        else
                        {
                            difference[targetNode.Symbol] = (showValues == ShowValuesOptions.Original) ? value : second?[property];

                            if (showValues == ShowValuesOptions.Original)
                            {
                                difference[targetNode.Symbol] = new JArray(value);
                            }
                            else if (showValues == ShowValuesOptions.New)
                            {
                                difference[targetNode.Symbol] = new JArray(second?[property]);
                            }
                            else
                            {
                                difference[targetNode.Symbol] = new JArray(value, second?[property]);
                            }
                        }
                    }

                    continue;
                }

                if (first?[property] is JObject)
                {

                    var targetNode = second?[property] == null
                        ? PointTargetNode(difference, property, ChangeMode.Removed, outputMode)
                        : PointTargetNode(difference, property, ChangeMode.Changed, outputMode);

                    var firstsItem = first[property];
                    var secondsItem = second[property];

                    var diffrence = Differentiate(firstsItem, secondsItem, outputMode, showValues);

                    if (diffrence != null)
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

                    var maximum = Math.Max(first?[property]?.Count() ?? 0, second?[property]?.Count() ?? 0);

                    for (int i = 0; i < maximum; i++)
                    {
                        var firstsItem = first[property]?.ElementAtOrDefault(i);
                        var secondsItem = second[property]?.ElementAtOrDefault(i);

                        var diff = Differentiate(firstsItem, secondsItem, outputMode, showValues);

                        if (diff != null)
                        {
                            difrences.Add(diff);
                        }
                    }

                    if (difrences.HasValues)
                    {
                        if (targetNode.Property != null)
                        {
                            difference[targetNode.Symbol][targetNode.Property] = difrences;
                        }
                        else
                            difference[targetNode.Symbol] = difrences;
                    }

                    continue;
                }
            }

            return difference;
        }

        public JToken Differentiate(JToken first, JToken second)
        {
            return Differentiate(first, second, this.OutputMode, this.ShowValues);
        }
    }
}
