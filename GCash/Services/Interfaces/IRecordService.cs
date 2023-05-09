using GCash.Models.StoredProcedure;
using GCash.Models.ViewModels;

namespace GCash.Services.Interfaces
{
    public interface IRecordService
    {
        Task<List<RecordModel>> GetRecordAsync(TableModel viewModel);

        Task<RecordModel> ReadRecordAsync(int Id);

        Task<bool> UpdateRecordAsync(RecordModel viewModel);

        Task<bool> DeleteRecordAsync(int Id);
    }
}
