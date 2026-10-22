using System;
using System.Collections.Generic;

namespace SrpLab;

public sealed class AppointmentSlotFinder
{
    private readonly BusinessHoursPolicy _businessHoursPolicy = new();

    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours,
        int slotMinutes,
        TimeOnly open,
        TimeOnly close,
        HashSet<DateTimeOffset> booked)
    {
        var cursor = Align(from, slotMinutes);
        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (_businessHoursPolicy.IsWithinBusinessHours(
                    cursor, open, close, slotMinutes)
                && !booked.Contains(cursor))
            {
                return cursor;
            }

            cursor = cursor.AddMinutes(slotMinutes);
        }

        return null;
    }

    public DateTimeOffset Align(DateTimeOffset from, int slotMinutes)
    {
        var minutes = from.Minute - (from.Minute % slotMinutes);

        return new DateTimeOffset(
            from.Year,
            from.Month,
            from.Day,
            from.Hour,
            minutes,
            0,
            from.Offset);
    }
}