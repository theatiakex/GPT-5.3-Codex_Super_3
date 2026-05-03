using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinFramesFromShotChangeRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;
    private readonly int _thresholdFrames;

    public MinFramesFromShotChangeRule(IShotChangeProvider shotChangeProvider, int thresholdFrames)
    {
        _shotChangeProvider = shotChangeProvider;
        _thresholdFrames = thresholdFrames;
    }

    public string Name => "MinFramesFromShotChange";

    public QcIssue? Evaluate(Cue cue, int index, IReadOnlyList<Cue> cues)
    {
        if (!cue.StartFrame.HasValue) return null;
        bool tooClose = _shotChangeProvider.GetShotChangeFrames()
            .Any(frame => Math.Abs(cue.StartFrame.Value - frame) < _thresholdFrames);
        return tooClose ? new QcIssue(Name, "Cue starts too close to a shot change.") : null;
    }
}
