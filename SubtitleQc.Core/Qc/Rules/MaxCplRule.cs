using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCplRule : IQcRule
{
    private readonly int _threshold;

    public MaxCplRule(int threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MaxCpl";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        int longestLine = cue.Lines.Count == 0 ? 0 : cue.Lines.Max(l => l.Length);
        return longestLine > _threshold
            ? new QcIssue(Name, $"Cue line length {longestLine} exceeds {_threshold}.")
            : null;
    }
}
