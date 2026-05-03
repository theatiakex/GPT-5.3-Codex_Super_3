using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Qc;

public sealed class QcReport
{
    [JsonConstructor]
    public QcReport(DateTimeOffset generatedAt, IReadOnlyList<QcResult> results)
    {
        GeneratedAt = generatedAt;
        Results = results;
    }

    public DateTimeOffset GeneratedAt { get; }
    public IReadOnlyList<QcResult> Results { get; }
}
