using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class EmptyCueCheckRule : IQcRule
{
    public string Name => "EmptyCueCheck";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        bool hasText = cue.Lines.Any(line => !string.IsNullOrWhiteSpace(line));
        return hasText ? null : new QcIssue(Name, "Cue does not contain visible text.");
    }
}
