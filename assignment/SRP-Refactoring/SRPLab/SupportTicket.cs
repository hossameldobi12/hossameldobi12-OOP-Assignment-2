
using System;

namespace SrpLab;

public sealed class SupportTicket
{
    private readonly TicketPriorityCalculator _priorityCalculator = new();
    private readonly TicketSlaCalculator _slaCalculator = new();
    private readonly PublicReplyFormatter _publicReplyFormatter = new();
    private readonly InternalEscalationFormatter _escalationFormatter = new();

    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(
        string id,
        string subject,
        string body,
        DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityCalculator.Calculate(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaCalculator.CalculateDeadline(OpenedAt, Priority);
    }

    public bool IsBreached(DateTimeOffset now)
    {
        return _slaCalculator.IsBreached(OpenedAt, Priority, now);
    }

    public string DraftPublicReply(string agentName)
    {
        return _publicReplyFormatter.Format(
            Id,
            Priority,
            agentName,
            SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _escalationFormatter.Format(
            Id,
            Priority,
            SlaDeadline());
    }
}
