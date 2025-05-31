using LMS.Application.DTOs.Admin.Financial;
using LMS.Application.DTOs.Admin.Sales;
using LMS.Application.DTOs.Stock;

namespace LMS.Application.Abstractions.Services.Helpers
{
    public interface IExcelReportGeneratorHelper
    {
        byte[] GenerateFinancialReport(ICollection<PaymentDetailsDto> paymentsList, ICollection<FinancialDetailsDto> revenuesList);
        byte[] GenerateStockReport(ICollection<StockSnapshotDto> stockList);

        public byte[] GeneratDeadStockReport(ICollection<DeadStockDto> deadStocks);
    }
}