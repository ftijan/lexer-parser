using System.Collections.Generic;
using System.Linq;

namespace Tokenizer
{
    public class RegexTokenizer : ITokenizer
    {
        private IList<TokenDefinition> _tokenDefinitions = new List<TokenDefinition>();

        public RegexTokenizer()
        {
            _tokenDefinitions.Add(new TokenDefinition(TokenType.And, "and", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Application, "app|application", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Between, "between", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.CloseParenthesis, "\\)", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Comma, ",", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Equals, "=", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.ExceptionType, "ex|exception", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Fingerprint, "fingerprint", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.NotIn, "not in", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.In, "in", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Like, "like", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Limit, "limit", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Match, "match", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Message, "msg|message", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.NotEquals, "!=", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.NotLike, "not like", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.OpenParenthesis, "\\(", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Or, "or", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.StackFrame, "sf|stackframe", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.DateTimeValue, "\\d\\d\\d\\d-\\d\\d-\\d\\d \\d\\d:\\d\\d:\\d\\d", 2));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.StringValue, "'([^']*)'", 1));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Number, "\\d+", 2));

        }

        public IEnumerable<DslToken> Tokenize(string queryText)
        {
            var tokenMatches = FindTokenMatches(queryText);

            var groupedByIndex = tokenMatches.GroupBy(x => x.StartIndex)
                .OrderBy(x => x.Key)
                .ToList();

            TokenMatch lastMatch = null;
            for (int i = 0; i < groupedByIndex.Count; i++)
            {
                var bestMatch = groupedByIndex[i].OrderBy(x => x.Precedence).First();
                if (lastMatch != null && bestMatch.StartIndex < lastMatch.EndIndex)
                {
                    continue;
                }

                yield return new DslToken(bestMatch.TokenType, bestMatch.Value);

                lastMatch = bestMatch;
            }

            yield return new DslToken(TokenType.SequenceTerminator);
        }
                
        private IList<TokenMatch> FindTokenMatches(string queryText) 
        {
            var tokenMatches = new List<TokenMatch>();

            foreach (var tokenDefinition in _tokenDefinitions)
            {
                tokenMatches.AddRange(tokenDefinition.FindMatches(queryText).ToList());
            }

            return tokenMatches;
        }
    }
}
