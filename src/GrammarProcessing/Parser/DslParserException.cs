using System;

namespace Parser
{
    [Serializable]
    public class DslParserException : Exception
    {
        public DslParserException(string message) : base(message) { }
    }
}
