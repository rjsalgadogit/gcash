using GCash.Models.Extensions;
using GCash.Models.StoredProcedure;
using GCash.Models.ViewModels;
using GCash.Services.Interfaces;
using Sequel.Service.Interfaces;

namespace GCash.Services
{
    public class RecordService : IRecordService
    {
        private readonly ISequelService<GetRecord> _getRecord;
        private readonly ISequelService<ReadRecord> _readRecord;
        private readonly ISequelService<UpdateRecord> _updateRecord;
        private readonly ISequelService<DeleteRecord> _deleteRecord;

        public RecordService(ISequelService<GetRecord> getRecord
            , ISequelService<ReadRecord> readRecord
            , ISequelService<UpdateRecord> updateRecord
            , ISequelService<DeleteRecord> deleteRecord)
        {
            _getRecord = getRecord;
            _readRecord = readRecord;
            _updateRecord = updateRecord;
            _deleteRecord = deleteRecord;
        }

        public async Task<List<RecordModel>> GetRecordAsync(TableModel viewModel)
        {
            var list = new List<RecordModel>();

            try
            {
                var result = await _getRecord.GetSPResultsFromSequelClientAsync(new GetRecord
                {
                    OffSet = viewModel.PageNumber,
                    PageSize = viewModel.PageSize,
                    Keyword = viewModel.Keyword,
                });

                if (result != null && result.Any())
                    list = result.Select(i => i.ToViewModel()).ToList();
            }
            catch (Exception ex) { throw; }

            return list;
        }

        public async Task<RecordModel> ReadRecordAsync(int Id)
        {
            var model = new RecordModel();

            try
            {
                var result = await _readRecord.GetSPResultsFromSequelClientAsync(new ReadRecord { Id = Id });

                if (result != null && result.Any())
                    model = result.Select(i => i.ToViewModel()).FirstOrDefault();
            }
            catch (Exception ex) { throw; }

            return model;
        }

        public async Task<bool> UpdateRecordAsync(RecordModel viewModel)
        {
            var isSuccess = false;

            try
            {
                await _updateRecord.PerformSPFromSequelClientAsync(new UpdateRecord
                {
                    Id = viewModel.Id,
                    ReferenceNumber = viewModel.ReferenceNumber,
                    Amount = viewModel.Amount,
                    IsClaimed = viewModel.IsClaimed
                });

                isSuccess = true;
            }
            catch (Exception ex) { throw; }

            return isSuccess;
        }

        public async Task<bool> DeleteRecordAsync(int Id)
        {
            var isSuccess = false;

            try
            {
                await _deleteRecord.PerformSPFromSequelClientAsync(new DeleteRecord { Id = Id });
                isSuccess = true;
            }
            catch (Exception ex) { throw; }

            return isSuccess;
        }
    }
}
