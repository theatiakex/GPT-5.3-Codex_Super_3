using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Parsing.Abstractions;

public interface ISubtitleParser
{
    string Format { get; }
    SubtitleDocument Parse(string rawContent);
}
