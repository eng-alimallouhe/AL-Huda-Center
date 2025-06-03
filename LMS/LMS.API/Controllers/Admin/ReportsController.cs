using LMS.API.DTOs.QueriesParameter;
using LMS.Application.Abstractions.Services.Helpers;
using LMS.Application.Features.Admin.Reports.Queries.DeadStockReport;
using LMS.Application.Features.Admin.Reports.Queries.FullStockInventoryReport;
using LMS.Application.Features.Admin.Sales.Queries.ExpensesReport;
using LMS.Application.Features.Admin.Sales.Queries.RevenuesReposrt;
using LMS.Domain.Enums.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        IExcelReportGeneratorHelper _excelReportGeneratorHelper;
        IMediator _mediator;
        
        public ReportsController(
            IExcelReportGeneratorHelper excelReportGeneratorHelper,
            IMediator mediator)
        {
            _excelReportGeneratorHelper = excelReportGeneratorHelper;
            _mediator = mediator;
        }


        [HttpGet("full-stock/{language}")]
        public async Task<ActionResult<byte[]>> GetStockReport(Language language)
        {
            var reportData = await _mediator.Send(new FullStockInventoryReportQuery(language));
            
            var report = _excelReportGeneratorHelper.GenerateStockReport(reportData);

            return File(
                report,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"report_full_stack_{new Random().Next(11111, 9999)}.xlsx");
        }


        [HttpGet("dead-stock")]
        public async Task<ActionResult<byte[]>> GetDeadStockReport([FromQuery] Language language, [FromQuery] DateTime from)
        {
            var reportData = await _mediator.Send(new DeadStockReportQuery(from, language));

            var report = _excelReportGeneratorHelper.GeneratDeadStockReport(reportData);

            return File(
                report,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"report_dead_stack_{new Random().Next(11111, 9999)}.xlsx");
        }


        [HttpGet("financial")]
        public async Task<ActionResult<byte[]>> GetFinancialReport([FromQuery] DateQueryParameter queryParameter)
        {
            var revReportData = await _mediator.Send(new RevenuesReposrtQuery(queryParameter.From, queryParameter.To));
            var expReportData = await _mediator.Send(new ExpensesReportQuery(queryParameter.From, queryParameter.To));

            var report = _excelReportGeneratorHelper.GenerateFinancialReport(expReportData, revReportData);

            return File(
                report,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"financial_report_{new Random().Next(11111, 9999)}.xlsx"
                );
        }
    }
}
