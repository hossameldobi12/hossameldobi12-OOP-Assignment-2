# Responsibilities

## 1. WardBoard

**Responsibilities:**
- Manage patients and beds.
- Calculate patient acuity.
- Send pager alerts.
- Format handoff notes.
- Export data to CSV.

**Why is this a problem?**

The class does many different things. If we change the alert format or the CSV format, we need to change the same class.

## 2. CheckoutBasket

**Responsibilities:**
- Store basket items and calculate totals.
- Calculate coupon discounts.
- Format gift messages.
- Handle payment authorization.

**Why is this a problem?**

Basket calculations, gift messages, and payment authorization are different responsibilities. Changes in one part can affect the same class.

## 3. SupportTicket

**Responsibilities:**
- Calculate ticket priority.
- Calculate SLA deadlines.
- Check if the SLA is breached.
- Format public replies.
- Format internal escalation messages.

**Why is this a problem?**

Ticket rules and message formatting are different jobs. Changing the message text should not require changing the ticket logic.

## 4. LoanDesk

**Responsibilities:**
- Calculate loan risk.
- Check loan eligibility.
- Determine required documents.
- Format decision letters.
- Export loan data to CSV.

**Why is this a problem?**

Loan decisions and document formatting are different responsibilities. The class may change for different reasons.

## 5. CourseEnrollmentDesk

**Responsibilities:**
- Register students.
- Manage the waitlist.
- Calculate waitlist positions.
- Format welcome packets.
- Format tuition invoices.

**Why is this a problem?**

Student enrollment and formatting documents are different jobs. Changes to invoices should not affect enrollment logic.

## 6. KitchenTicket

**Responsibilities:**
- Detect allergens.
- Calculate preparation time.
- Format kitchen tickets.
- Select the kitchen lane.

**Why is this a problem?**

Food preparation rules, timing calculations, and ticket formatting are different responsibilities. Keeping them together makes changes harder.

## 7. SubscriptionBilling

**Responsibilities:**
- Calculate payment proration.
- Generate invoice numbers.
- Format dunning emails.
- Format ledger entries.

**Why is this a problem?**

Billing calculations, invoice numbering, and output formatting are different jobs. A change in one part can require modifying the same class.

## 8. WarehousePickList

**Responsibilities:**
- Allocate stock.
- Sort items by aisle and bin.
- Format picker instructions.
- Generate WMS XML.

**Why is this a problem?**

Stock allocation, route planning, and output formatting are different responsibilities. Each one can change for a different reason.

## 9. GradeBook

**Responsibilities:**
- Store student scores.
- Calculate student averages.
- Determine letter grades.
- Check Honor Roll eligibility.
- Format transcripts.
- Export data to CSV.

**Why is this a problem?**

Grade calculations, academic rules, and report formatting are different jobs. Changes to grading rules should not affect report formatting.

## 10. AppointmentDesk

**Responsibilities:**
- Check business hours.
- Find available appointment slots.
- Book appointments.
- Generate ICS calendar data.
- Format SMS reminders.

**Why is this a problem?**

Appointment booking, business-hour rules, and message formatting are different responsibilities. Changes to calendar or SMS formats should not affect booking logic.

## Conclusion

The main problem is that each class has more than one reason to change. Separating these responsibilities makes the code easier to understand, maintain, and test.