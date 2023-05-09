using GCash.Models;
using GCash.Models.ViewModels;
using GCash.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GCash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecordService _recordService;

        public HomeController(ILogger<HomeController> logger, IRecordService recordService)
        {
            _logger = logger;
            _recordService = recordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _TableView(TableModel viewModel)
        {
            return PartialView(viewModel);
        }

        public async Task<string> GetData(TableModel viewModel)
        {
            var result = new JsonResultModel
            {
                Collection = await _recordService.GetRecordAsync(viewModel),
                IsSuccess = true
            };

            return JsonConvert.SerializeObject(await Task.FromResult(result));
        }

        public async Task<IActionResult> ReadData(int Id)
        {
            var result = await _recordService.ReadRecordAsync(Id);
            return Json(result);
        }

        public async Task<IActionResult> DeleteData(int Id)
        {
            var result = new JsonResultModel();

            try
            {
                result.IsSuccess = await _recordService.DeleteRecordAsync(Id);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return Json(result);
        }

        public async Task<IActionResult> UpdateData(RecordModel viewModel)
        {
            var result = new JsonResultModel();

            try
            {
                result.IsSuccess = await _recordService.UpdateRecordAsync(viewModel);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return Json(result);
        }

        #region Defaults
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}