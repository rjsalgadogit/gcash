namespace GCash.Models.ViewModels
{
    public class RecordModel
    {
        public int Id { get; set; }

        public string ReferenceNumber { get; set; }

        public decimal Amount { get; set; }

        public bool IsClaimed { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string StatusText { get { return IsClaimed ? "Claimed" : "Unclaimed"; } }

        public string CreatedDateText { get { return CreatedDate.HasValue ? CreatedDate.Value.ToString("MM/dd/yyyy") : string.Empty; } }
    }
}