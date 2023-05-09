using Sequel.Attributes;
using Sequel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GCash.Models.StoredProcedure
{
    public class DeleteRecord : ModelBaseSqlStoredProcedure
    {
        [Param]
        public int Id { get; set; }
    }
}
