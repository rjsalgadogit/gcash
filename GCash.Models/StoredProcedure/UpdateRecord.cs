using Sequel.Attributes;
using Sequel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GCash.Models.StoredProcedure
{
    public class UpdateRecord : ModelBaseSqlStoredProcedure
    {
        [Param]
        public int Id { get; set; }

        [Param]
        public string ReferenceNumber { get; set; }

        [Param]
        public decimal Amount { get; set; }

        [Param]
        public bool IsClaimed { get; set; }
    }
}
