using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Qc.Abstractions;

public interface IQcRule
{
    string Name { get; }
    QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues);
}
