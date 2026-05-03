using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Models;

public sealed class ShotChangeData
{
    [JsonConstructor]
    public ShotChangeData(IReadOnlyList<TimeSpan> timestamps, IReadOnlyList<int> frames)
    {
        Timestamps = timestamps;
        Frames = frames;
    }

    public IReadOnlyList<TimeSpan> Timestamps { get; }
    public IReadOnlyList<int> Frames { get; }
}
