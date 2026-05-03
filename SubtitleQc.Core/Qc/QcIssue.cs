using System.Text.Json.Serialization;

namespace SubtitleQc.Core.Qc;

public sealed class QcIssue
{
    [JsonConstructor]
    public QcIssue(string ruleName, string message)
    {
        RuleName = ruleName;
        Message = message;
    }

    public string RuleName { get; }
    public string Message { get; }
}
