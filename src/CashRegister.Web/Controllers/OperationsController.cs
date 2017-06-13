using System;
using System.Linq;
using System.Threading.Tasks;
using CashRegister.Web.Models.DbContext;
using CashRegister.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/operations")]
    public class OperationsController : Controller
    {
        private readonly IOperationService _operationService;
        private readonly IProductService _productService;

        public OperationsController(IOperationService operationService, IProductService productService)
        {
            _operationService = operationService;
            _productService = productService;
        }

        [HttpGet("daily")]
        public IActionResult GetDailyOperations()
        {
            var operations = _operationService.GetDailyOperations();
            return Ok(operations);
        }

        [HttpGet("days/{startDate}/{endDate}")]
        public IActionResult GetOperationsByDay([FromRoute] DateTime startDate, [FromRoute] DateTime endDate)
        {
            var groups = _operationService.GetOperationsByDay(startDate, endDate).ToList();
            return Ok(groups);
        }

        //public IActionResult GetExcelFile(DateTime startDate, DateTime endDate)
        //{
        //    var groups = _operationService.GetOperationsByDay(startDate, endDate).ToList();
        //    var fileContent = ExcelFileHelper.CreateOperationsFile(groups);
        //    return File(fileContent,
        //        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //        $"AutreMonde_Export_{startDate.ToString("yyyy-MM-dd")}_{endDate.ToString("yyyy-MM-dd")}.xlsx");
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOperation([FromBody] Operation operation)
        {
            operation = await _operationService.CreateOrUpdate(operation);
            return Ok(operation);
        }


    }
}