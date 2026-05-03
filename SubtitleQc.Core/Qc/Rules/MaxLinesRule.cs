using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxLinesRule : IQcRule
{
    private readonly int _threshold;

    public MaxLinesRule(int threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MaxLines";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        return cue.Lines.Count > _threshold
            ? new QcIssue(Name, $"Cue has {cue.Lines.Count} lines, max is {_threshold}.")
            : null;
    }
}
