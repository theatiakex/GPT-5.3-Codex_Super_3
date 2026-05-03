using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class OverlapCheckRule : IQcRule
{
    public string Name => "OverlapCheck";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        if (index == 0) return null;
        Cue previousCue = cues[index - 1];
        return cue.Start < previousCue.End
            ? new QcIssue(Name, "Cue overlaps the previous cue.")
            : null;
    }
}
