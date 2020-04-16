using Newtonsoft.Json.Linq;

namespace JsonDiffer
{
    public static class JsonHelper
    {
        public static JToken Difference(this JToken first, JToken second)
        {
            return JsonDifferentiator.Differentiate(first, second);
        }
    }
}