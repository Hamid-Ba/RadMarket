using Framework.Domain;
using System;

namespace DiscountManagement.Domain.DiscountCodeAgg
{
    public class DiscountCode : EntityBase
    {
        public string Code { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int Count { get; private set; }
        public string Reason { get; private set; }

        public DiscountCode(string code, DateTime startDate, DateTime endDate, int count, string reason)
        {
            Code = code;
            StartDate = startDate;
            EndDate = endDate;
            Count = count;
            Reason = reason;
        }

        public void Edit(string code, DateTime startDate, DateTime endDate, int count, string reason)
        {
            Code = code;
            StartDate = startDate;
            EndDate = endDate;
            Count = count;
            Reason = reason;
        }

        public void CodeUsed() => --Count;

        public bool IsCodeStillWork() => (StartDate <= DateTime.Now && DateTime.Now <= EndDate && Count > 0);

    }
}