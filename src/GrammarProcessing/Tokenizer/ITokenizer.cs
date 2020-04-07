using System.Collections.Generic;

namespace Tokenizer
{
    public interface ITokenizer
    {
        IEnumerable<DslToken> Tokenize(string queryText);
    }
}
