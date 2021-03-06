using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Parser;
using System.Linq;
using Tokenizer;
using Xunit;

namespace Query.Tests
{
    public class QueryTests
    {
        private JsonSerializerSettings SerializerSettings { get; }

        public QueryTests()
        {
            SerializerSettings = new JsonSerializerSettings();
            SerializerSettings.Converters.Add(new StringEnumConverter());
        }

        [Fact]
        public void CanTokenizeAndParseQueryString()
        {
            string query = @"
MATCH app = 'MyTestApp'
AND ex IN ('System.NullReferenceException', 'System.FormatException')
BETWEEN 2016-01-01 00:00:00 AND 2016-02-01 00:00:00
LIMIT 100
";

            var tokenizer = new RegexTokenizer();
            var tokenSequence = tokenizer.Tokenize(query).ToList();

            var parser = new QueryParser();
            var dataRepresentation = parser.Parse(tokenSequence);

            var json = JsonConvert.SerializeObject(dataRepresentation, Formatting.Indented, SerializerSettings);
            Assert.NotNull(json);
        }
    }
}
