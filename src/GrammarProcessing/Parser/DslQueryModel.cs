using System.Collections.Generic;

namespace Parser
{
    public class DslQueryModel
    {
        public DateRange DateRange { get; set; }
        public int? Limit { get; set; }
        public IList<MatchCondition> MatchConditions { get; } = new List<MatchCondition>();
    }
}
