using TypedGuardrail.Domain.Enums;

namespace TypedGuardrail.Domain.Records;

public sealed record TriageResult(
    TriageRoutingStatus Status,
    string Reason,
    Department Department,
    TicketPriority Priority,
    DateTime ProcessedAt
);
