
using System;
using System.Collections.Generic;

namespace SrpLab;

public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();

    private readonly BusinessHoursPolicy _businessHoursPolicy = new();
    private readonly AppointmentSlotFinder _slotFinder = new();
    private readonly IcsCalendarFormatter _icsFormatter = new();
    private readonly SmsReminderFormatter _smsFormatter = new();

    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        return _businessHoursPolicy.IsWithinBusinessHours(
            when, Open, Close, SlotMinutes);
    }

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        return _slotFinder.FindNextSlot(
            from,
            searchHours,
            SlotMinutes,
            Open,
            Close,
            _booked);
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!IsWithinBusinessHours(slot) || _booked.Contains(slot))
            return false;

        _booked.Add(slot);
        return true;
    }

    public string ToIcs(
        DateTimeOffset slot,
        string patientName,
        string clinician)
    {
        return _icsFormatter.Format(
            slot, SlotMinutes, patientName, clinician);
    }

    public string SmsReminder(DateTimeOffset slot, string clinicPhone)
    {
        return _smsFormatter.Format(slot, clinicPhone);
    }
}
