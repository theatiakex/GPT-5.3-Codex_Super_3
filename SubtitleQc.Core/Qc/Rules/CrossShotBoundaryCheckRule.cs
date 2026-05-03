using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class CrossShotBoundaryCheckRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;

    public CrossShotBoundaryCheckRule(IShotChangeProvider shotChangeProvider)
    {
        _shotChangeProvider = shotChangeProvider;
    }

    public string Name => "CrossShotBoundaryCheck";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        bool crossesCut = _shotChangeProvider.GetShotChangeTimestamps()
            .Any(cut => cut > cue.Start && cut < cue.End);
        return crossesCut ? new QcIssue(Name, "Cue crosses a shot boundary.") : null;
    }
}
