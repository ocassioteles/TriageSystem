namespace TypedGuardrail.Domain.Records;

public sealed record IncomingPayload(
    string? DepartmentStr,
    string? PriorityStr,
    string Title,
    string Description,
    string? CustomerEmail = null,
    DateTime? SubmittedAt = null
);
