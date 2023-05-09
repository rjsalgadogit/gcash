using GCash.Models.StoredProcedure;
using GCash.Models.ViewModels;

namespace GCash.Models.Extensions
{
    public static class Extension
    {
        public static RecordModel ToViewModel(this GetRecord spModel)
            => new RecordModel
            {
                Id = spModel.Id,
                ReferenceNumber = spModel.ReferenceNumber,  
                Amount = spModel.Amount,
                IsClaimed = spModel.IsClaimed,
                CreatedDate = spModel.CreatedDate,
            };

        public static RecordModel ToViewModel(this ReadRecord spModel)
            => new RecordModel
            {
                Id = spModel.Id,
                ReferenceNumber = spModel.ReferenceNumber,
                Amount = spModel.Amount,
                IsClaimed = spModel.IsClaimed,
                CreatedDate = spModel.CreatedDate
            };
    }
}
