### Superspec

Implemented a working prototype from agents_super.md + superspec.md with a clean separation of concerns and all existing tests passing.

What I built
Added new core library project: SubtitleQc.Core
Added unified internal data model in SubtitleQc.Core/Models:
Cue
SubtitleDocument
ShotChangeData
Added isolated parsers in SubtitleQc.Core/Parsing:
SrtParser
WebVttParser
TtmlParser
ISubtitleParser abstraction
Added QC engine in SubtitleQc.Core/Qc:
RuleEngine
IQcRule, IShotChangeProvider
JSON-serializable result model: QcReport, QcResult, QcIssue, QcStatus
Implemented rules required by spec/tests in SubtitleQc.Core/Qc/Rules:
MaxLinesRule
MaxCplRule
MaxCpsRule
MinDurationRule
OverlapCheckRule
EmptyCueCheckRule
CrossShotBoundaryCheckRule
MinFramesFromShotChangeRule
Project wiring
Added SubtitleQc.Core to solution: GPT-5.3-Codex_Super_3.sln
Added project reference from tests to core:
SubtitleQc.Tests/SubtitleQc.Tests.csproj
Resolved missing test dependency versions in SubtitleQc.Tests.csproj using dotnet add package.
Validation + telemetry
Ran dotnet test:
Passed: 19, Failed: 0, Total: 19
Logged test attempts to test_metrics.jsonl:
Attempt 1: restore/build failed (missing package versions)
Attempt 2: full pass at 100%
Generated test_performance_report.md with Mermaid line chart after reaching 100%.
If you want, I can now do a cleanup pass to remove generated bin/obj artifacts from git tracking scope and prepare a clean commit.