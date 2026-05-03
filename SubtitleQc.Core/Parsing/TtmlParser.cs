using System.Globalization;
using System.Xml.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing;

public sealed class TtmlParser : ISubtitleParser
{
    public string Format => "TTML";

    public SubtitleDocument Parse(string rawContent)
    {
        XDocument document = XDocument.Parse(rawContent);
        var cues = document.Descendants()
            .Where(e => e.Name.LocalName == "p")
            .Select(ParseCue)
            .Where(c => c is not null)
            .Cast<Cue>()
            .ToList();
        return new SubtitleDocument(cues);
    }

    private static Cue? ParseCue(XElement paragraph)
    {
        string? begin = paragraph.Attribute("begin")?.Value;
        string? end = paragraph.Attribute("end")?.Value;
        if (begin is null || end is null) return null;
        TimeSpan start = ParseTimestamp(begin);
        TimeSpan finish = ParseTimestamp(end);
        var lines = ExtractLines(paragraph).ToList();
        return new Cue(Guid.NewGuid().ToString("N"), start, finish, lines);
    }

    private static IEnumerable<string> ExtractLines(XElement paragraph)
    {
        string normalized = paragraph.Value.Replace("\r\n", "\n");
        return normalized.Split('\n', StringSplitOptions.TrimEntries);
    }

    private static TimeSpan ParseTimestamp(string value)
    {
        string normalized = value.Replace(',', '.').TrimEnd('s');
        return TimeSpan.Parse(normalized, CultureInfo.InvariantCulture);
    }
}
