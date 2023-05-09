using Sequel.Attributes;
using Sequel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GCash.Models.StoredProcedure
{
    public class GetRecord : ModelBaseSqlStoredProcedure
    {
        [Read]
        public int Id { get; set; }

        [Read]
        public string ReferenceNumber { get; set; }

        [Read]
        public decimal Amount { get; set; }

        [Read]
        public bool IsClaimed { get; set; }

        [Read]
        public DateTime CreatedDate { get; set; }

        [Param]
        public int OffSet { get; set; }

        [Param]
        public int PageSize { get; set; }

        [Param]
        public string Keyword { get; set; }
    }
}
