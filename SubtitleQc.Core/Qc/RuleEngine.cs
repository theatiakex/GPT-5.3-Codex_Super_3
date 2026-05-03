using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc;

public sealed class RuleEngine
{
    private readonly IReadOnlyList<IQcRule> _rules;

    public RuleEngine(IReadOnlyList<IQcRule> rules)
    {
        _rules = rules;
    }

    public QcReport Evaluate(IReadOnlyList<Cue> cues)
    {
        var results = new List<QcResult>(cues.Count);
        for (int i = 0; i < cues.Count; i++) results.Add(EvaluateCue(cues[i], i, cues));
        return new QcReport(DateTimeOffset.UtcNow, results);
    }

    private QcResult EvaluateCue(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        var issues = new List<QcIssue>();
        foreach (IQcRule rule in _rules) TryAddIssue(issues, rule.Evaluate(cue, index, cues));
        QcStatus status = issues.Count == 0 ? QcStatus.Passed : QcStatus.Failed;
        return new QcResult(cue.Id, status, issues);
    }

    private static void TryAddIssue(List<QcIssue> issues, QcIssue? issue)
    {
        if (issue is not null) issues.Add(issue);
    }
}
