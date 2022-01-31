using Newtonsoft.Json.Linq;

namespace JsonDiffer
{
    public static class JsonHelper
    {
        public static JToken Difference(this JToken first, JToken second, bool showOriginalValues = false)
        {
            return JsonDifferentiator.Differentiate(first, second, showOriginalValues );
        }
    }
}