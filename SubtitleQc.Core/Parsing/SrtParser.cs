using System.Globalization;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing;

public sealed class SrtParser : ISubtitleParser
{
    public string Format => "SRT";

    public SubtitleDocument Parse(string rawContent)
    {
        var cues = SplitBlocks(rawContent)
            .Select(ParseBlock)
            .Where(c => c is not null)
            .Cast<Cue>()
            .ToList();
        return new SubtitleDocument(cues);
    }

    private static IEnumerable<string> SplitBlocks(string rawContent)
    {
        return rawContent.Replace("\r\n", "\n")
            .Split("\n\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static Cue? ParseBlock(string block)
    {
        string[] lines = block.Split('\n', StringSplitOptions.TrimEntries);
        int timeIndex = GetTimeIndex(lines);
        if (timeIndex < 0 || timeIndex >= lines.Length - 1) return null;
        (TimeSpan start, TimeSpan end) = ParseTimes(lines[timeIndex]);
        var textLines = lines.Skip(timeIndex + 1).ToList();
        return new Cue(Guid.NewGuid().ToString("N"), start, end, textLines);
    }

    private static int GetTimeIndex(IReadOnlyList<string> lines)
    {
        return lines.Count > 1 && !lines[0].Contains("-->") ? 1 : 0;
    }

    private static (TimeSpan Start, TimeSpan End) ParseTimes(string timeLine)
    {
        string[] parts = timeLine.Split("-->", StringSplitOptions.TrimEntries);
        return (ParseTimestamp(parts[0]), ParseTimestamp(parts[1]));
    }

    private static TimeSpan ParseTimestamp(string value)
    {
        string normalized = value.Replace(',', '.');
        return TimeSpan.Parse(normalized, CultureInfo.InvariantCulture);
    }
}
