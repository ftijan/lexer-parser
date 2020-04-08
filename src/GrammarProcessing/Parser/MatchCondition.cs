using System.Collections.Generic;

namespace Parser
{
    public class MatchCondition
    {
        public DslObject Object { get; set; }
        public DslOperator Operator { get; set; }
        public string Value { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public DslLogicalOperator LogOpToNextCondition { get; set; }
    }
}
