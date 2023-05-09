using Sequel.Attributes;
using Sequel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GCash.Models.StoredProcedure
{
    public class ReadRecord : ModelBaseSqlStoredProcedure
    {
        public int Id { get; set; }

        [Read]
        public string ReferenceNumber { get; set; }

        [Read]
        public decimal Amount { get; set; }

        [Read]
        public bool IsClaimed { get; set; }

        [Read]
        public DateTime? CreatedDate { get; set; }
    }
}
