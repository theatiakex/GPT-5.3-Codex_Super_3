using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinDurationRule : IQcRule
{
    private readonly TimeSpan _threshold;

    public MinDurationRule(TimeSpan threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MinDuration";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        TimeSpan duration = cue.End - cue.Start;
        return duration < _threshold
            ? new QcIssue(Name, $"Cue duration {duration.TotalSeconds:F2}s is below {_threshold.TotalSeconds:F2}s.")
            : null;
    }
}
