using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCpsRule : IQcRule
{
    private readonly double _threshold;

    public MaxCpsRule(double threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MaxCps";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        double seconds = (cue.End - cue.Start).TotalSeconds;
        if (seconds <= 0) return new QcIssue(Name, "Cue duration must be positive.");
        int characters = cue.Lines.Sum(l => l.Length);
        double cps = characters / seconds;
        return cps > _threshold ? new QcIssue(Name, $"Cue speed {cps:F2} exceeds {_threshold:F2}.") : null;
    }
}
